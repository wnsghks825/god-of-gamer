using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GodOfGamer
{
    /// <summary>
    /// 경험치바
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class ExperienceBar : MonoBehaviour
    {
        private Slider _slider;
        private LevelBanner _levelBanner;

        /// <summary>
        /// 경험치 증가시키기
        /// </summary>
        /// <param name="experience">경험치량</param>
        /// 매개변수는 0보다 크고 1보다 작은 값을 받는다.
        public void Increase(float experience)
        {
            _slider.value += experience;

            if(_slider.value >= 1.0f)
            {
                _levelBanner.LevelUp();
                _slider.value = 0.0f;
            }
        }

        private void Awake()
        {
            _levelBanner = GetComponentInParent<LevelBanner>();
            _slider = GetComponent<Slider>();
        }

        private void Start()
        {
            _slider.value = 0.0f;
        }
    }
}