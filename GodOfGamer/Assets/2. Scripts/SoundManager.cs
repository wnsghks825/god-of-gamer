using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GodOfGamer
{
    public class SoundManager : MonoBehaviour
    {

        public AudioClip buttonSound;   //Audioclip이라는 데이터타입에 변수생성
        public AudioClip fireSound;
        public AudioClip shockSound;
        AudioSource myAudio; //컴퍼넌트에서 AudioSource가져오기
        public static SoundManager instance; //다른 스크립트에서 이스크립트에있는 함수를 호출할때 쓰임

        private void Awake()
        {
            if (SoundManager.instance == null)
                SoundManager.instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            myAudio = GetComponent<AudioSource>();  //myAudio에 컴퍼넌트에있는 AudioSource넣기

        }

        public void ButtonSound()
        {
            myAudio.PlayOneShot(buttonSound);
        }
        public void FireSound()
        {
            myAudio.PlayOneShot(fireSound);
        }
        public void ShockSound()
        {
            myAudio.PlayOneShot(shockSound);
        }
    }

}
