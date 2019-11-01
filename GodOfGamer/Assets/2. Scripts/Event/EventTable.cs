using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodOfGamer.Event
{
    public abstract class EventTable : MonoBehaviour
    {
        public TextAsset csvFile;

        /// <summary>
        /// Awake에서 호출하는 순수가상 함수
        /// </summary>
        protected abstract void Initialize();

        protected virtual void Awake()
        {
            Initialize();
        }
    }
}