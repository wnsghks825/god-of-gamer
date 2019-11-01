using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodOfGamer.Core
{
    /// <summary>
    /// 싱글톤 추상클래스 (C# Generic) 
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// 외부에서 인스턴스를 직접 접근해서 오염의 방지를 위해 한정자를 private or protected로 선언해 캡슐화.
        /// </summary>
        static protected T s_instance;

        /// <summary>
        /// 외부에서 접근하는 getter
        /// </summary>
        static public T s_Instance
        {
            get
            {
                if (s_instance == null)
                {
                    /// getter에 접근했을 때, 할당이 되어 있지 않다면 게임오브젝트로부터 검색(찾기)를 시도한다.
                    s_instance = FindObjectOfType<T>();
                }

                return s_instance;
            }
        }

        protected virtual void Awake()
        {
            if (s_instance == null)
            {
                /// s_instance의 값이 할당되어있지 않을 경우
                /// 자기자신을 s_instance에 할당해준다.
                s_instance = this as T; // == (T)this
            }
            else
            {
                if (s_instance != this)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}