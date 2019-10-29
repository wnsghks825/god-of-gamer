using UnityEngine;
using UnityEngine.UI;

namespace GodOfGamer
{
    /// <summary>
    /// 배경 이미지 관리
    /// </summary>
    [RequireComponent(typeof(Image))]
    [DisallowMultipleComponent]
    public class VisualArea : UIComponent
    {
        [SerializeField, Header("배경이미지"), Tooltip("기본배경")]
        private Sprite _defaultSprite = null;

        [SerializeField,Tooltip("피버배경")]
        private Sprite _feverSprite = null;

        [SerializeField,Tooltip("탈진배경")]
        private Sprite _exhaustSprite = null;
        
        public Image background { get; private set; }

        /// <summary>
        /// 배경이미지 변경
        /// </summary>
        /// <param name="state">현재 상태</param>
        public void ChangeBgImage(Character.State state)
        {
            switch (state)
            {
                case Character.State.Default:
                    if (background.sprite != _defaultSprite)
                        background.sprite = _defaultSprite;

                    break;

                case Character.State.Fever:
                    if(background.sprite != _feverSprite)
                        background.sprite = _feverSprite;
                    break;

                case Character.State.Exhaust:
                    if(background.sprite != _exhaustSprite)
                        background.sprite = _exhaustSprite;
                    break;
            }

        }
        
        protected override void Awake()
        {
            base.Awake();

            background = GetComponent<Image>();
        }
    }
}
