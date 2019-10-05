using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailFace : MonoBehaviour
{
    public GameObject Face; //표정을 밖으로 빼자

    public float failTime;  // 실패 시 얼굴 지속시간

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator WaitFor()   //얼굴 지속시간
    {
        Face.gameObject.SetActive(true);
        if (Face.activeSelf == true)
        {
            failTime += failTime;
        }
        yield return new WaitForSeconds(failTime);

        Face.gameObject.SetActive(false);
    }

}
