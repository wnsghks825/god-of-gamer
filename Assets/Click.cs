using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utilities;

public class Click : MonoBehaviour
{
    public static Click instance = null;

    public Text successText;
    public Text failText;
    public Text comboText;
    public Text maxText;

    GameManager refGameMgr;
    public GameObject Face;

    private void Awake()
    {
        refGameMgr = GameManager.s_Instance;
        successText.text = "Count_S : " + refGameMgr.Count_S.ToString();
        failText.text = "Count_F : " + refGameMgr.Count_F.ToString();
        comboText.text = "Count_C : " + refGameMgr.Count_C.ToString();
        maxText.text = "Count_M : " + refGameMgr.Count_M2.ToString();
    }

    void ChangeScene()
    {
        int random = Random.Range(0, 3);  
        SceneManager.LoadScene(random);

    }
    void FaceCheck()
    {
        Face.gameObject.SetActive(false);
    }
    public void ClickEvents()
    {



        if (SceneManager.GetActiveScene().name == "P1")
        {
            if (gameObject.name == "Button1" || gameObject.name == "Button3")
            {
                refGameMgr.Count_S++;
                refGameMgr.Count_P++;
                refGameMgr.Count_C = refGameMgr.Count_P;
                refGameMgr.Count_M2 = refGameMgr.Count_C;
                //Invoke("ChangeScene", 1); // 2초뒤 LaunchProjectile함수 호출
                Face.gameObject.SetActive(false);
                ChangeScene();

            }
            else
            {
                refGameMgr.Count_M2 = refGameMgr.Count_C;
                refGameMgr.Count_P = 0;
                Face.gameObject.SetActive(true);
                //Invoke("ChangeScene", 1); // 2초뒤 LaunchProjectile함수 호출
                ChangeScene();
            }
        }

        if (SceneManager.GetActiveScene().name == "P2")
        {
            if (gameObject.name == "Button2" || gameObject.name == "Button3")
            {
                refGameMgr.Count_S++;
                refGameMgr.Count_P++;
                refGameMgr.Count_C = refGameMgr.Count_P;
                refGameMgr.Count_M2 = refGameMgr.Count_C;
                //Invoke("ChangeScene", 1); // 2초뒤 LaunchProjectile함수 호출
                Face.gameObject.SetActive(false);
                ChangeScene();
            }
            else
            {
                refGameMgr.Count_F++;
                refGameMgr.Count_M2 = refGameMgr.Count_C;
                refGameMgr.Count_P = 0;
                Face.gameObject.SetActive(true);
                //Invoke("ChangeScene", 1); // 2초뒤 LaunchProjectile함수 호출
                ChangeScene();
            }

        }

        if (SceneManager.GetActiveScene().name == "P3")
        {
            if (gameObject.name == "Button1" || gameObject.name == "Button2")
            {
                refGameMgr.Count_S++;
                refGameMgr.Count_P++;
                refGameMgr.Count_C = refGameMgr.Count_P;
                refGameMgr.Count_M2 = refGameMgr.Count_C;
                //Invoke("ChangeScene", 1); // 2초뒤 LaunchProjectile함수 호출
                Face.gameObject.SetActive(false);
                ChangeScene();
            }
            else
            {
                refGameMgr.Count_F++;
                refGameMgr.Count_P = 0;
                refGameMgr.Count_M2 = refGameMgr.Count_C;
                Face.gameObject.SetActive(true);
                //Invoke("ChangeScene", 1); // 2초뒤 LaunchProjectile함수 호출
                ChangeScene();
            }
        }

        successText.text = "Count_S : " + refGameMgr.Count_S.ToString();
        failText.text = "Count_F : " + refGameMgr.Count_F.ToString();
        comboText.text = "Count_C : " + refGameMgr.Count_C.ToString();
        maxText.text = "Count_M : " + refGameMgr.Count_M.ToString();

        DontDestroyOnLoad(refGameMgr);
    }

}