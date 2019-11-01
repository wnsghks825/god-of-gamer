using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GodOfGamer
{
    [RequireComponent(typeof(Image))]
    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField, Header("스프라이트 애니메이션")]
        private Sprite[] _sprites = null;

        [SerializeField, Header("전환시간")]
        private float _waitTime = 0.05f;

        private Image _image;
        private Coroutine _coroutine;
        private WaitForSeconds _wait;

        private int cntidx;

        #region MonoBehaviour EventFunc Region

        private void Awake()
        {
            _wait = new WaitForSeconds(_waitTime);
            _image = GetComponent<Image>();
            _image.enabled = false;
            this.enabled = false;
        }

        // 이 객체가 활성화되면
        private void OnEnable()
        {
            // 이미지 컴포넌트도 활성화해준다.
            _image.enabled = true;

            _coroutine = StartCoroutine(SpriteAnim());
        }

        // 이 객체가 비활성화되면
        private void OnDisable()
        {
            // 이미지 컴포넌트도 비활성화해준다.
            _image.enabled = false;

            if(_coroutine != null)
                StopCoroutine(_coroutine);
        }

        #endregion

        private IEnumerator SpriteAnim()
        {
            while(true)
            {
                yield return _wait;

                cntidx++;

                // 인덱스를 초과했을 경우 0으로 변경
                if (cntidx == _sprites.Length)
                {
                    cntidx = 0;
                }

                // 이미지 변경
                _image.sprite = _sprites[cntidx];
            }
        }
    }
}