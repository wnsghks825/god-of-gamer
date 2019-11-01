using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GodOfGamer.Event
{
    /// <summary>
    /// 정의 된 이벤트들을 갖는 클래스
    /// </summary>
    public class DefineEventTable : EventTable
    {
        public List<BindingEvent> tempEventsList = new List<BindingEvent>();

        private Dictionary<string, DefineEvent> _evDefineMap;
        private List<DefineEvent> _evList;              // Note. 필요없어보임

        /// <summary>
        /// 키값을 통해 DefineEvent 참조값을 가져온다.
        /// </summary>
        /// <param name="Key">Key</param>
        /// <returns>해당키를 찾을 수 없다면 null을 반환</returns>
        public DefineEvent GetDefineEvent(string Key)
        {
            if (_evDefineMap[Key] == null)
                return null;

            return _evDefineMap[Key];
        }

        protected override void Initialize()
        {
            var csvReadData = Core.CSVReader.Read(csvFile);

            _evDefineMap = new Dictionary<string, DefineEvent>();
            _evList = new List<DefineEvent>();

            for(int i = 0; i < csvReadData.Count; i++)
            {
                var temp = new DefineEvent(csvReadData[i]["Key"].ToString(), csvReadData[i]["Descript"].ToString());
                temp.onEvents = tempEventsList[i];
                _evDefineMap.Add(csvReadData[i]["Key"].ToString(), temp);
                _evList.Add(temp);
            }
        }
    }
}