using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodOfGamer
{
    /// <summary>
    /// 레벨 및 경험치에 관련된 클래스
    /// 외부에서 이 클래스에 접근한다
    /// </summary>
    public class LevelBanner : MonoBehaviour
    {
        private LevelDisplay _levelDisplay;
        private ExperienceBar _experienceBar;

        public Character character { get; private set; }
        public uint level { get; private set; }

        /// <summary>
        /// 레벨업시 호출하는 함수
        /// </summary>
        public void LevelUp()
        {
            _levelDisplay.RenewTextLevel(++level);
        }

        /// <summary>
        /// 성공했을 경우
        /// </summary>
        public void SuccessAnswer()
        {
            // 분기를 설정한 이유는 
            // 피버 혹은 탈진에서 오르는 경험치가 다를 경우를 처리하기 위해서
            switch (character.cntState)
            {
                case Character.State.Default:
                    _experienceBar.Increase(0.1f);
                    break;

                case Character.State.Fever:
                    _experienceBar.Increase(0.2f);
                    break;

                case Character.State.Exhaust:
                    break;
            }
        }

        /// <summary>
        /// 실패했을 경우
        /// </summary>
        //public void FailureAnswer()
        //{
        //    // 탈진상태일 경우 GaugeImage
        //    if (character.cntState == Character.State.Exhaust)
        //    {

        //    }
        //}

        private void Awake()
        {
            _levelDisplay = GetComponentInChildren<LevelDisplay>();
            _experienceBar = GetComponentInChildren<ExperienceBar>();
        }

        private void Start()
        {
            character = GameMgr.s_Instance.character;

            // 레벨 1로 초기화
            level = 1;

            _levelDisplay.RenewTextLevel(level);
        }
    }
}