using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GodOfGamer.Event;

namespace GodOfGamer
{
    /// <summary>
    /// GameManager 역할을 하게 된다.
    /// </summary>
    [DisallowMultipleComponent]
    public class GameMgr : Core.Singleton<GameMgr>
    {
        [SerializeField, Header("이벤트매니저")]
        private Event.EventMgr _eventMgr = null;

        [SerializeField, Header("Width/Height"), Tooltip("현재 작업중인 환경의 width, height 작성한다.")]
        private Vector2 _sizeDelta = Vector2.zero;

        [SerializeField, Header("패턴리스트")]
        private List<Pattern> _patternList = null;

        #region Properties Region
        
        public Score score { get; private set; }

        // 현재 패턴 인덱스
        public byte cntPatternIdx { get; set; }

        public Event.EventMgr eventMgr { get { return _eventMgr; } }
        public List<Pattern> patternList { get { return _patternList; } }

        public RectTransform rectTransform { get; private set; }
        public VisualArea visualArea { get; private set; }
        public ChoiceButtons choiceButtons { get; private set; }
        public Character character { get; private set; }
        public LevelBanner levelBanner { get; private set; }

        #endregion

        /// <summary>
        /// 버튼클릭이벤트 함수
        /// </summary>
        /// <param name="btnIdx">버튼 인덱스</param>
        public void Handle(int btnIdx)
        {
            // 성공시
            if (Judgment(btnIdx))
            {
                // 성공처리
                eventMgr.BasicEventHandle(EventMgr.BasicEvent.Success);

                //Note.1018 피버 카운팅을 GameMgr CheckAnswer vs EventMgr Handle
                //탈진카운팅도 마찬가지

                // 이벤트트리거 작동
                eventMgr.BasicEventTrigger(EventMgr.BasicEvent.Success, score);
            }
            // 실패시
            else
            {
                // 실패처리
                eventMgr.BasicEventHandle(EventMgr.BasicEvent.Failure);

                // 기본 이벤트 트리거 실패시 작동
                eventMgr.BasicEventTrigger(EventMgr.BasicEvent.Failure, score);
            }
        }
         
        private bool Judgment(int btnidx)
        {
            if (patternList[cntPatternIdx].button[btnidx])
                return true;
            else
                return false;
        }

        /// <summary>
        /// 질문의 대한 체크 함수 기본이벤트에 등록
        /// </summary>
        /// <param name="isSuccessful">성공판정인지?</param>
        public void CheckAnswer(bool isSuccessful)
        {
            if (isSuccessful)
            {
                score.CorrectAnswer();

                // 기본상태이면서 성공을 하면 피버에 가까워진다.
                if (character.cntState == Character.State.Default)
                {
                    character.fever.GaugeIncrease();
                    character.exhaust.NormalSuccess();
                }
                else if(character.cntState == Character.State.Exhaust)
                {
                    character.exhaust.ExhSuccess(); 

                    // 10.30 - 탈진 객체에서 처리하게 변경한다.
                    character.ChangeImage();
                }
            }
            else
            {
                score.IncorrectAnswer();

                // 기본상태이면서 실패를 하면 좌절에 가까워진다.
                if (character.cntState == Character.State.Default)
                {
                    character.fever.GaugeReset();
                    character.exhaust.NormalFail();
                }
                else if (character.cntState == Character.State.Exhaust)
                {
                    character.exhaust.ExhFail();

                    // 10.30 - 탈진 객체에서 처리하게 변경한다.
                    character.ChangeImage();
                }
            }

            // 캐릭터의 상태가 기본상태이면 이미지를 변경시켜준다.
            if (character.cntState == Character.State.Default)
            { 
                character.ChangeImage();
            }
        }

        #region MonoBehaviour EventFunc Region
        protected override void Awake()
        {
            base.Awake();

            score = new Score();

            rectTransform = GetComponent<RectTransform>();
            visualArea = GetComponentInChildren<VisualArea>();
            choiceButtons = GetComponentInChildren<ChoiceButtons>();
            character = GetComponentInChildren<Character>();
            levelBanner = GetComponentInChildren<LevelBanner>();
        }

        private void Start()
        {
            if (eventMgr == null)
                _eventMgr = Event.EventMgr.s_instance;

            StartCoroutine(SetScaler());
        }
        
        /// <summary>
        /// Canvas Scaler scalerFactor 변경
        /// 변경한 값을 토대로 ChoiceButtons의 GridLayoutGroup 필드값 변경
        /// </summary>
        private IEnumerator SetScaler()
        {
            yield return new WaitForSeconds(0.01f);

            // 현재 Canvas의 Width, Height 얻어오기
            Vector2 sizeDelta = rectTransform.sizeDelta;

            // width의 값을 토대로 CanvasScaler Scale Factor 값 설정하기
            GetComponent<CanvasScaler>().scaleFactor = sizeDelta.x / _sizeDelta.x;

            yield return new WaitForSeconds(0.01f);

            // scaleFactor 변경된 값은 RectTransform width, hieght값이 변경된다.
            // 변경된 값을 제대로 가져오기 위해 코루틴를 이용
            _sizeDelta = sizeDelta = rectTransform.sizeDelta;
         
            // ChoiceButtons의 gridLayoutGroup 필드 설정 함수 호출
            choiceButtons.SettingGridLayoutGroup(_sizeDelta.x, _sizeDelta.y);

            character.SettingImage(sizeDelta.x);
        }


        #endregion
    }

    /// <summary>
    /// 이미지에 따른 버튼 패턴
    /// </summary>
    [System.Serializable]
    public struct Pattern
    {
        public Sprite sprite;
        public bool[] button;
    }
}