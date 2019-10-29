using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GodOfGamer
{
    /// <summary>
    /// 캐릭터 클래스
    /// </summary>
    [DisallowMultipleComponent]
    public class Character : MonoBehaviour
    {
        [SerializeField, Header("패턴 사이클 입력")]
        private int _defaultPatternStart = -1;

        [SerializeField]
        private int _defaultPatternEnd = -1;

        [SerializeField, Header("피버 오브젝트")]
        private Fever _fever = null;

        [SerializeField, Header("탈진 오브젝트")]
        private Exhaust _exhaust = null;

        public enum State { Default, Fever, Exhaust }
        public State cntState { get; private set; }

        public Fever fever { get { return _fever; } }
        public Exhaust exhaust { get { return _exhaust; } }
        public Face face { get; private set; }      // --> Character로 뺸다.

        private Image _image;
        private GameMgr _gameMgr;

        /// <summary>
        /// 이미지 크기 및 위치 셋팅
        /// </summary>
        public void SettingImage(float width)
        {
            var rectTransform = GetComponent<RectTransform>();

            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
            rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, width * 0.6768f);
        }

        /// <summary>
        /// 이미지 변경함수
        /// </summary>
        public void ChangeImage()
        {
            int randomValue = Random.Range(_defaultPatternStart, _defaultPatternEnd + 1);

            _image.sprite = _gameMgr.patternList[randomValue].sprite;
            _gameMgr.cntPatternIdx = (byte)randomValue;
        }

        public void ChangeImage(int idx)
        {
            _image.sprite = _gameMgr.patternList[idx].sprite;

            _gameMgr.cntPatternIdx = (byte)idx;
        }

        [ContextMenu("피버작동테스트")]
        private void FeverTest()
        {
            Event.EventMgr.s_Instance.BasicEventHandle(Event.EventMgr.BasicEvent.Fever);
        }

        [ContextMenu("탈진작동테스트")]
        private void ExhaustedTest()
        {
            Event.EventMgr.s_Instance.BasicEventHandle(Event.EventMgr.BasicEvent.Exhausted);
        }
        
        /// <summary>
        /// 피버상태활성화, 기본이벤트에 바인딩해준다.
        /// </summary>
        public void EnableFeverState()
        {
            ChangeState(State.Fever);
        }

        /// <summary>
        /// 기본상태활성화
        /// </summary>
        public void EnableDefaultState()
        {
            ChangeState(State.Default);
        }

        /// <summary>
        /// 탈진상태활성화
        /// </summary>
        public void EnableExhaustState()
        {
            ChangeState(State.Exhaust);
        }

        /// <summary>
        /// 상태전환 함수 (스테이트패턴의 개념을 착안)
        /// </summary>
        /// <param name="state">새로운상태</param>
        private void ChangeState(State newState)
        {
            _gameMgr.visualArea.ChangeBgImage(newState); //배경이미지 변경

            switch (newState)
            {
                case State.Fever:
                    fever.Active(10.0f);
                    Debug.Log("ChangeState(Fever)");
                    break;

                case State.Exhaust:
                    exhaust.Active();
                    Debug.Log("ChangeState(Exhaust)");
                    break;

            }

            cntState = newState;
        }

        //MonoBehaviour EventFunc Region
        private void Awake()
        {
            cntState = State.Default;

            face = GetComponentInChildren<Face>();
            _image = GetComponent<Image>();
            _gameMgr = GetComponentInParent<GameMgr>();
        }
    }
}