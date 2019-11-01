using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GodOfGamer
{
    /// <summary>
    /// 레벨 및 상태에 따른 처리를 보여주기 위한 클래스
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class LevelDisplay : MonoBehaviour
    {
        [SerializeField] // 게이지 이미지
        private Image _gaugeImage = null;

        private Text _levelText = null;
        private LevelBanner _levelBanner;

        private bool _isActivatedGauge = false;
        
        /// <summary>
        /// 레벨텍스트를 갱신한다.
        /// </summary>
        public void RenewTextLevel(uint level)
        {
            _levelText.text = level.ToString();
        }

        /// <summary>
        /// 상태 시스템 사용 --> EventMgr BasicTable 이벤트바인딩해둔다.
        /// </summary>
        /// <param name="state">상태이름을 입력받는다</param>
        public void ActivateStatusSystem(string stateName)
        {
            GaugeImageInitialize(stateName);
        }

        // 게이지 이미지 초기화
        private void GaugeImageInitialize(string stateName)
        {
            _gaugeImage.fillAmount = 1.0f;

            if (stateName == "Fever")
            {
                _gaugeImage.color = Color.red;
            }
            else if (stateName == "Exhausted")
            {
                _gaugeImage.color = Color.magenta;
            }
            else
            {
                Debug.LogWarning("잘못입력했습니다");
                return;
            }

            _isActivatedGauge = true;
        }

        private void Awake()
        {
            _levelBanner = GetComponentInParent<LevelBanner>();
            _levelText = GetComponentInChildren<Text>();

            // 게이지 이미지 컴포넌트 필드값 변경
            _gaugeImage.fillAmount = 0.0f;
            _gaugeImage.type = Image.Type.Filled;
            _gaugeImage.fillMethod = Image.FillMethod.Vertical;
            _gaugeImage.fillOrigin = (int)Image.OriginVertical.Bottom;
        }

        private void Update()
        {
            if(_isActivatedGauge)
            {
                float ratio = 0.0f;

                if (_levelBanner.character.cntState == Character.State.Fever)
                {
                    ratio = _levelBanner.character.fever.gauge / _levelBanner.character.fever.maxGuage;
                }
                else if(_levelBanner.character.cntState == Character.State.Exhaust)
                {
                    ratio = _levelBanner.character.exhaust.gauge / _levelBanner.character.exhaust.maxGauge;
                }

                if (ratio != _gaugeImage.fillAmount)
                {
                    _gaugeImage.fillAmount = ratio;

                    if (ratio <= 0.0f)
                        _isActivatedGauge = false;
                }
            }
        }
    }
}