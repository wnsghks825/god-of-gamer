using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GodOfGamer
{
    /// <summary>
    /// 피버 컴포넌트
    /// </summary>
    public class Fever : MonoBehaviour
    {
        /// <summary>
        /// GameUI Pattern의 Fever에 해당하는 시작 인덱스와 종료 인덱스를 작성해준다.
        /// </summary>
        [SerializeField, Header("피버패턴 사이클 입력")] 
        private int _FeverPatterStart = -1;

        [SerializeField]
        private int _FeverPatterEnd = -1;

        [SerializeField, Header("피버 작동시간"), Range(0,50f)]
        private float _durationTime = 0f;

        [SerializeField, Header("전환시간"), Range(0,5)]
        private float _waitTime = 0f;

        [SerializeField, Header("피버발동조건(콤보)")]
        private int _feverCondition = 10;

        [SerializeField]
        private SpriteAnimation[] _wispAnimation = null;

        public float gauge { get; private set; }
        public float maxGuage { get; private set; }

        private Coroutine _coroutine;   // 업데이트 함수 대신 코루튼을 사용
        private WaitForSeconds _wait;
        private SpriteAnimation _fireAnimation;

        private float _consumptionSec;  // 초당소비량

        /// <summary>
        /// 이 클래스 컴포넌트를 활성화 시켜주기 위한 함수
        /// </summary>
        /// <param name="durationTime">작동시간</param>
        public void Active(float durationTime)
        {
            _durationTime = durationTime;
            
            // 위습비활성
            WispInActive();

            // 피버게이지 최대값(100)으로 고정
            gauge = maxGuage;

            // 초당 감소하는 양을 캐싱.
            // Update에서 이용
            _consumptionSec = gauge / durationTime;

            // Fever 컴포넌트 활성화
            enabled = true;
        }

        /// <summary>
        /// 피버게이지 증가 함수
        /// </summary>
        public void GaugeIncrease()
        {
            gauge += maxGuage / _feverCondition;

            if (gauge >= maxGuage)
            {
                var refEventMgr = Event.EventMgr.s_Instance;

                if (refEventMgr != null)
                {
                    // 캐릭터 상태를 피버로 변경
                    refEventMgr.BasicEventHandle(Event.EventMgr.BasicEvent.Fever);

                    return;
                }
            }

            // 위습 활성
            if (gauge % 20 == 0)
            {
                _wispAnimation[(int)gauge / 20 - 1].enabled = true;
            }
        }

        public void GaugeReset()
        {
            WispInActive();
            gauge = 0;
        }

        /// <summary>
        /// 위습비활성 로직
        /// </summary>
        public void WispInActive()
        {
            // 모든 위습 비활성
            for (int i = 0; i < _wispAnimation.Length; i++)
            {
                _wispAnimation[i].enabled = false;
            }
        }


        #region MonoBehaviour EventFunc Region

        private void Awake()
        {
            _fireAnimation = GetComponentInChildren<SpriteAnimation>();

            _wait = new WaitForSeconds(_waitTime);

            // 최대게이지 설정
            maxGuage = 100.0f;

            enabled = false;
        }

        private void OnEnable()
        {
            _fireAnimation.enabled = true;
            _coroutine = StartCoroutine(FeverActivate());
        }

        private void OnDisable()
        {
            gauge = 0.0f;
            _fireAnimation.enabled = false;
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private void Update()
        {
            if(gauge > 0f)
            {
                gauge -= _consumptionSec * Time.deltaTime;
            }
        }

        #endregion

        private IEnumerator FeverActivate()
        {
            var refgameMgr = GameMgr.s_Instance;
            var refCharacter = refgameMgr.character;
            
            // 진행한 시간
            float _operatingtime = 0f; 

            int cntFeverIdx = _FeverPatterStart;

            // 피버발동할 때 이전의 패턴 인덱스를 저장해둔다.
            int tempIdx = refgameMgr.cntPatternIdx;  

            while (_operatingtime < _durationTime)
            {
                refCharacter.ChangeImage(cntFeverIdx);

                yield return _wait;

                _operatingtime += _waitTime;
                cntFeverIdx++;

                // 현재피버인덱스가 특정값을 초과할경우
                if (cntFeverIdx > _FeverPatterEnd)
                    cntFeverIdx = _FeverPatterStart;
            }

            // 저장해둔 인덱스로 다시 설정해준다.
            refCharacter.ChangeImage(tempIdx);

            // 기본상태로 변경하기
            refCharacter.EnableDefaultState();

            enabled = false;
        }
    }
}