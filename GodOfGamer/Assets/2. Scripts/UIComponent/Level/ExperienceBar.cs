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
        [SerializeField, Header("경험치 가중치")]
        private float _weight;

   

        private Slider _slider;
        private LevelBanner _levelBanner;
        private float value { get; set; }
        /// <summary>
        /// 경험치 증가시키기
        /// </summary>
        /// <param name="experience">경험치량</param>
        /// 매개변수는 0보다 크고 1보다 작은 값을 받는다.
        public void Increase(float experience)
        {

            _slider.value += experience;
            value += experience;
            int maxValue = (int)(Mathf.Pow(_weight, (_levelBanner.level-1)) * 100);
            _slider.maxValue = maxValue;
            if (value >= _slider.maxValue)
            {
                _levelBanner.LevelUp();

                _slider.value = value - maxValue;
                Debug.Log(value - maxValue);
                value = _slider.value;
            }

        }

        public void PassingValue(float experience)
        {

            value += experience;
            if (value >= _slider.maxValue)
            {
                _slider.value = value - _slider.maxValue;
                Debug.Log(_slider.value);
                value = _slider.value;
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