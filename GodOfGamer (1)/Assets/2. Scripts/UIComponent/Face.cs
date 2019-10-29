using UnityEngine;
using UnityEngine.UI;

namespace GodOfGamer
{
    [RequireComponent(typeof(Image))]
    public class Face : MonoBehaviour
    {
        private Image _image;
        private float _durationTime;   // 작동해야할 시간
        private float _operatingTime;  // 작동시간

        /// <summary>
        /// 현재 컴포넌트를 활성화 혹은 비활성화 하는 함수
        /// </summary>
        /// <param name="enabled">활성여부</param>
        /// <param name="operationgtime">활성시간</param>
        public void Activate(float durationTime)
        {
            _operatingTime = 0.0f;
            _durationTime = durationTime;

            if (!enabled)
                enabled = true;
        }

        #region MonoBehaviour EventFunc Region

        private void Awake()
        {
            _image = GetComponent<Image>();

            // 시작시 비활성
            this.enabled = false;
        }

        private void OnEnable()
        {
            // 이미지 활성화
            _image.enabled = true;
        }

        private void OnDisable()
        {
            // 이미지 비활성
            _image.enabled = false;
        }

        private void Update()
        {
            // 흐른시간이 작동해야할 시간보다 적다면
            if(_operatingTime < _durationTime)
            {
                // flowTime에 이전프레임 ~ 현재프레임 완료된 시간을 더해준다.
                _operatingTime += Time.deltaTime;
            }
            else
            {
                // 비활성화
                enabled = false;
            }
        }

        #endregion
    }
}