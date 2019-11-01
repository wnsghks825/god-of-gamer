using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GodOfGamer.Event
{
    /// <summary>
    /// 조건 이벤트들을 갖는 클래스
    /// </summary>
    public class ConditionEventTable : EventTable
    {
        private List<ConditionEvent> _evList;

        public ConditionEvent Filter(char var, uint score)
        {
            // evDone값이 False이면서 ev.var과 일치한 것들을 먼저 찾는다.
            var evQuary = from ev in _evList
                          where ev.evDone == false && ev.var.Equals(var) 
                          orderby ev.num
                          select ev;

            if (evQuary.Count() != 0)
            {
                // 위에서 num기준으로 오름차순으로 정렬했기 때문에 First를 이용해준다.
                var cdEv = evQuary.First();

                // condition값을 토대로 진행
                if (cdEv.condition.Equals("Over"))
                {
                    if (score >= cdEv.num)
                        return cdEv;
                }
                else if (cdEv.condition.Equals("Equal"))
                {
                    if (score == cdEv.num)
                        return cdEv;
                }
                else if (cdEv.condition.Equals("Mod"))
                {
                    // 미구현
                }
            }
            return null;
        }

        protected override void Initialize()
        {
            _evList = new List<ConditionEvent>();

            var csvReadData = Core.CSVReader.Read(csvFile);

            foreach (var line in csvReadData)
            {
                var temp = new ConditionEvent(line["Key"].ToString(),
                                              char.Parse(line["Var"].ToString()),
                                              bool.Parse(line["EvDone"].ToString()),
                                              uint.Parse(line["Num"].ToString()),
                                              line["Condition"].ToString(),
                                              uint.Parse(line["Time"].ToString())
                                              );

                _evList.Add(temp);
            }
        }
    }
}