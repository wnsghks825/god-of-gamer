using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodOfGamer
{
    /// <summary>
    /// 점수 관련 클래스
    /// 수정하지않는다.
    /// </summary>
    public class Score
    {
        public uint success { get; private set; }
        public uint failure { get; private set; }
        public uint combo { get; private set; }
        public uint max { get; private set; }


        /// <summary>
        /// 기본생성자
        /// </summary>
        public Score() {
            success = uint.MinValue;
            failure = uint.MinValue;
            combo = uint.MinValue;
            max = uint.MinValue;
        }

        /// <summary>
        /// 정답일 경우 호출
        /// </summary>
        public void CorrectAnswer()
        {
            success++;

            combo++;
            // max의 값이 증가한 콤보값보다 작다면 갱신해준다.
            if (max < combo)
            {
                max = combo;
            }
        }
        
        /// <summary>
        /// 오답일 경우 호출
        /// </summary>
        public void IncorrectAnswer()
        {
            failure++;

            // 콤보값을 임시저장
            uint tempCombo = combo;

            // 임시저장한 콤보값이 max 값보다 크다면
            if (tempCombo > max)
            {
                // max의 값을 콤보값으로 갱신해준다.
                max = tempCombo;
            }

            // 콤보 0로 초기화
            combo = 0;
        }
    }
}