using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodOfGamer
{
    /// <summary>
    /// 탈진 컴포넌트
    /// </summary>
    public class Exhaust : MonoBehaviour
    {
        [SerializeField]
        private SpriteAnimation[] _exhAnimation = null;

        [SerializeField, Header("탈진 게이지 증가(노멀 상태)")]
        private int _exhEnter = 1;      // 노멀 상태에서 탈진 게이지 증가 변수

        [SerializeField, Header("탈진 게이지 감소(노멀 상태)")]
        private int _exhExit = 1;       // 노멀 상태에서 탈진 게이지 감소 변수

        [SerializeField, Header("탈진 게이지 증가(탈진 상태)")]
        private int _exhFail = 1;       // 탈진 상태에서 탈진 게이지 증가

        [SerializeField, Header("연속 실패 시 가중치")]
        private float _exhWeight = 1;   // 연속 실패 시 가중치

        public float gauge { get; private set; }
        public float maxGauge { get; private set; }

        private SpriteAnimation _spriteAnimation;

        private Fever _fever { get; set; }
        
        public void Active()
        {
            enabled = true;
        }
        public void GaugeReset()
        {
            gauge = 0;
        }
        /// <summary>
        ///  실패 판정 누적
        /// </summary>
        public void NormalFail()
        {
            gauge = _exhWeight * gauge + _exhEnter;
            _fever.ResetGauge();
            if (gauge >= maxGauge)
            {
                var refEvMgr = GameMgr.s_Instance.eventMgr;

                if (refEvMgr != null)
                {
                    //for (int i = 0; i < _exhAnimation.Length; i++)
                    //{
                    //    _exhAnimation[i].enabled = false;
                    //}

                    // 캐릭터 상태를 탈진으로 변경
                    refEvMgr.BasicEventHandle(Event.EventMgr.BasicEvent.Exhausted);
                    SoundManager.instance.ShockSound();
                    return;
                }
            }

            if (gauge >= 33.0f)
            {
                _exhAnimation[0].enabled = true;
            }
            if (gauge >= 66.0f)
            {
                _exhAnimation[1].enabled = true;
            }
            Debug.Log(gauge);
        }

        /// <summary>
        /// 탈진 상태에서 성공 판정을 받았을 시
        /// </summary>
        public void ExhSuccess()
        {
            var refGameMgr = GameMgr.s_Instance;
            gauge = gauge - refGameMgr.score.combo;
                Debug.Log(gauge);
            if (gauge <= 0)
            {
                //for (int i = 0; i < _exhAnimation.Length; i++)
                //{
                //    _exhAnimation[i].enabled = false;
                //}

                // 기본상태로 변경하기
                refGameMgr.character.EnableDefaultState();

                // 컴포넌트 비활성
                enabled = false;
            }
        }

        /// <summary>
        /// 탈진 상태에서 실패 판정을 받았을 때
        /// </summary>
        public void ExhFail()
        {
            gauge += _exhFail;
            if (gauge >= maxGauge)
                gauge = maxGauge;

        }

        /// <summary>
        /// 성공 판정 시 탈진 게이지
        /// </summary>
        public void NormalSuccess()
        {
            gauge -= _exhExit;
            if (gauge <= 33.0f)
            {
                _exhAnimation[0].enabled = false;
            }
            if (gauge <= 66.0f)
            {
                _exhAnimation[1].enabled = false;
            }
            if (gauge <= 0)
                gauge = 0;

        }

        private void Awake()
        {
            _spriteAnimation = GetComponentInChildren<SpriteAnimation>();
            _fever = GetComponentInParent<GameMgr>().GetComponentInChildren<Fever>();

            // 최대 게이지 설정
            maxGauge = 100.0f;

            enabled = false;
        }

        private void OnEnable()
        {
            _spriteAnimation.enabled = true;
            for(int i = 0; i < _exhAnimation.Length; i++)
            {
                _exhAnimation[i].enabled = false;
            }
            gauge = maxGauge;
        }

        private void OnDisable()
        {
            _spriteAnimation.enabled = false;

            gauge = 0.0f;
        }
    }
}
