using UnityEngine;
using UnityEngine.Events;

namespace GodOfGamer.Event
{
    [System.Serializable] public class BindingEvent : UnityEvent { }

    /// <summary>
    /// 이벤트 클래스
    /// </summary>
    public class GOGEvent
    {
        [SerializeField]
        private string _key;

        public string key { get => _key; set => _key = value; } // 실행할 고유 이벤트 이름

        protected GOGEvent(string key)
        {
            this.key = key;
        }
    }

    /// <summary>
    /// 기본이벤트 정의하기 위한 클래스
    /// </summary>
    [System.Serializable]
    public sealed class BasicEvent : GOGEvent
    {
        [SerializeField]
        private string _descript;

        public string descript { get => _descript; set => _descript = value; } // 해당 이벤트의 설명을 기재한다.
        public BindingEvent onEvents;

        public BasicEvent(string key, string descript) : base(key)
        {
            this.descript = descript;
        }
    }


    /// <summary>
    /// 이벤트 정의하기 위한 클래스
    /// </summary>
    [System.Serializable]
    public sealed class DefineEvent : GOGEvent
    {
        // Note.20191024 대화이벤트가 추가시 변경해준다..
        //[SerializeField]
        //private string _eventType;              

        [SerializeField]
        private string _descript;

        public string descript { get => _descript; set => _descript = value; } // 해당 이벤트의 설명을 기재한다.
        public BindingEvent onEvents;
        
        public DefineEvent(string key, string descript) : base(key)
        {
            this.descript = descript;
        }
    }

    /// <summary>
    /// 조건 이벤트
    /// </summary>
    [System.Serializable]
    public sealed class ConditionEvent : GOGEvent
    {
        public bool evDone { get; private set; }        // 해당되는 이벤트가 실행되었는지 확인하는 함수
        public char var { get; private set; }           // 
        public uint num { get; private set; }           // Condition으로 연산할 various의 수량
        public string condition { get; private set; }   // {Over, Equal, Mod}  
        public uint time { get; private set; }          // 일단은 필요없어보임

        public ConditionEvent(string key, char var = '\0', bool evDone = false, uint num = 0, string condition = "", uint time = 0) : base(key)
        {
            this.evDone = evDone; this.var = var;
            this.num = num; this.condition = condition; this.time = time;
        }

        /// <summary>
        /// 사용시 호출해서 evDone값을 true로 변경해준다.
        /// </summary>
        public void UseEvent()
        {
            if (!evDone)
                evDone = true;
        }
    }
}
