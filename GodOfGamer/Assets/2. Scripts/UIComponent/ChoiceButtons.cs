using UnityEngine;
using UnityEngine.UI;

namespace GodOfGamer
{
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(CanvasGroup))]
    public class ChoiceButtons : UIComponent
    {
        private GridLayoutGroup _gridLayoutGroup;
        public CanvasGroup canvasGroup { get; private set; }

        private float _durationTime;   // 작동해야할 시간
        private float _operatingTime;  // 작동시간

        /// <summary>
        /// 클릭이벤트 처리
        /// Button 컴포넌트에 이벤트 등록을 해준다.
        /// </summary>
        /// <param name="idx">버튼 인덱스</param>
        public void ClickButton(int btnidx)
        {
            root.Handle(btnidx);
        }

        /// <summary>
        /// CanvasGroup 입력을 비활성하기 위한 함수
        /// </summary>
        /// <param name="durationTime">비활성시간</param>
        public void ButtonsInactive(float durationTime)
        {
            _durationTime = durationTime;
            _operatingTime = 0f;
            
            canvasGroup.interactable = false;
        }

        public void SettingGridLayoutGroup(float width, float height)
        {
            _gridLayoutGroup.cellSize = new Vector2(width / 3, height * 0.25f);
        }

        protected override void Awake()
        {
            base.Awake();

            canvasGroup = GetComponent<CanvasGroup>();
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
        }

        private void Update()
        {
            if(!canvasGroup.interactable)
            {
                _operatingTime += Time.deltaTime;

                if(_operatingTime > _durationTime)
                    canvasGroup.interactable = true;
            }
        }
    }
}