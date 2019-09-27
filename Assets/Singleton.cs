using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        static private T s_instance;

        static public T s_Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = FindObjectOfType<T>();

                    if (s_instance == null)
                        Debug.Log("Singleton s_Instance를 찾을 수 없습니다.");

                }

                return s_instance;
            }
        }

        protected virtual void Awake()
        {
            if (!s_instance)
                s_instance = (T)this;
            else
            {
                if (s_instance != (T)this)
                    Destroy(this.gameObject);
            }
        }
    }
}