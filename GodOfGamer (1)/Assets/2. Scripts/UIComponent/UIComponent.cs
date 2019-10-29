using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodOfGamer
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class UIComponent : MonoBehaviour
    {
        protected GameMgr root;

        public RectTransform rectTransform { get; protected set; }

        protected virtual void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        protected virtual void Start()
        {
            root = GameMgr.s_Instance;
        }
    }
}