using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GodOfGamer.Event
{
    /// <summary>
    /// 이벤트 관리자
    /// </summary>
    [DisallowMultipleComponent]
    public class EventMgr : Core.Singleton<EventMgr>
    {
        public enum BasicEvent { Success, Failure, Fever, Exhausted }
        
        BasicEventTable _basicEventTable;           // 기본이벤트 테이블
        ConditionEventTable _conditionEventTable;   // 조건이벤트 테이블
        DefineEventTable _defineEventTable;         // 정의이벤트 테이블
        DialogEventTable _dialogEventTable;         // 대화이벤트 테이블

        ConditionEvent cntCondiEvent = null;

        /// <summary>
        /// 기본 이벤트 처리함수
        /// </summary>
        /// <param name="Key">키</param>
        public void BasicEventHandle(BasicEvent basicEvent)
        {
            var basicEv = _basicEventTable.GetBasicEvent(basicEvent.ToString());

            if (basicEv != null)
            {
                basicEv.onEvents.Invoke();
            }
        }

        /// <summary>
        /// 기본 이벤트 트리거
        /// </summary>
        /// <param name="basicEvent">기본 이벤트타입</param>
        /// <param name="score">현재 점수</param>
        public void BasicEventTrigger(BasicEvent basicEvent, Score score)
        {
            // basicEvent가 Success일 경우에
            if (basicEvent == BasicEvent.Success)
            {
                if (EventTrigger('s', score.success))
                {
                    Execute();
                }
                else if (EventTrigger('c', score.combo))
                {
                    Execute();
                }
            }
            // basicEvent가 Failure일 경우에
            else if (basicEvent == BasicEvent.Failure)
            {
                if (EventTrigger('f', score.failure))
                {
                    Execute();
                }
            }
        }

        /// <returns>True - temp의 값이 있을 때 / False - temp의 값이 없을 때 </returns>
        private bool EventTrigger(char var, uint score)
        {
            ConditionEvent temp = null;

            switch (var)
            {
                case 's':
                case 'S':
                    temp = _conditionEventTable.Filter('S', score);
                    break;

                case 'f':
                case 'F':
                    temp = _conditionEventTable.Filter('F', score);
                    break;

                case 'c':
                case 'C':
                    temp = _conditionEventTable.Filter('C', score);
                    break;

                case 'm':
                case 'M':
                    break;

                    //Note.1024 레벨추가된다네요 야발
                case 'L':
                case 'l':
                    break;
            }

            if(temp != null)
            {
                // 조건이벤트를 캐싱해둔다.
                cntCondiEvent = temp;

                return true;
            }

            return false;
        }
        
        public void EventTrigger(DefineEvent defineEvent, int score) { }

        /// <summary>
        /// 캐싱해둔 조건이벤트 실행
        /// </summary>
        public void Execute()
        {
            if(cntCondiEvent != null)
            {
                cntCondiEvent.UseEvent();

                // 이벤트 처리
                _defineEventTable.GetDefineEvent(cntCondiEvent.key).onEvents.Invoke();

                cntCondiEvent = null;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            _basicEventTable = GetComponentInChildren<BasicEventTable>();
            _conditionEventTable = GetComponentInChildren<ConditionEventTable>();
            _defineEventTable = GetComponentInChildren<DefineEventTable>();
        }
    }
}