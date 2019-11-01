using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodOfGamer.Event
{
    /// <summary>
    /// 기본이벤트 테이블
    /// </summary>
    public class BasicEventTable : EventTable
    {
        public List<BasicEvent> tempBasicList = new List<BasicEvent>();       // Editor에서 이용

        private Dictionary<string, BasicEvent> _basicEvMap;

        /// <summary>
        /// 키값을 통해 BasicEvent 참조값을 가져온다.
        /// </summary>
        /// <param name="Key">Key</param>
        /// <returns>해당키를 찾을 수 없다면 null을 반환</returns>
        public BasicEvent GetBasicEvent(string Key)
        {
            if (_basicEvMap[Key] == null)
                return null;

            return _basicEvMap[Key];
        }

        protected override void Initialize()
        {
            // Map 초기화
            _basicEvMap = new Dictionary<string, BasicEvent>();

            // Map 요소 추가
            for(int i = 0; i < tempBasicList.Count; i++)
            {
                _basicEvMap.Add(tempBasicList[i].key, tempBasicList[i]);
            }
        }
    }
}