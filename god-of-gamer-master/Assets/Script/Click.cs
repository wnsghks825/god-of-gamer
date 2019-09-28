using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour
{
    public static Click instance = null;

    public Text successText;
    public Text failText;
    public Text comboText;
    public Text maxText;

    bool T1=false;

    public GameObject Face;
    GameManager refGameMgr;

    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    private void Awake()
    {
        refGameMgr = GameManager.s_Instance;
    }
   
    void Active(GameObject P) {
        P.gameObject.SetActive(true);
    }
    void Deactive(GameObject P)
    {
        P.gameObject.SetActive(false);
    }

    void ChangePattern()
    {
        int random = Random.Range(0, 3); 

        switch(random){
            case 0:
                {
                    Active(P1);
                    Deactive(P2);
                    Deactive(P3);
                    break;
                }
            case 1:
                {
                    Active(P2);
                    Deactive(P1);
                    Deactive(P3);
                    break;
                }
            case 2:
                {
                    Active(P3);
                    Deactive(P1);
                    Deactive(P2);
                    break;
                }                  
        }
    }
    void Judge()
    {
        if (T1 == true)
        { 
            refGameMgr.Count_F++;
            if (refGameMgr.Count_M < refGameMgr.Count_C)
            {
                refGameMgr.Count_M = refGameMgr.Count_C;
            }
            refGameMgr.Count_C = 0;
        }
        else
        {
            refGameMgr.Count_S++;
            refGameMgr.Count_C++;
        }
    }
    void FaceCheck()
    {
        Face.gameObject.SetActive(false);
    }
    public void ClickEvents()
    {
        var refGameMgr = GameManager.s_Instance;
       
        if (P1.gameObject.activeSelf == true)   //틀린 경우(101)
        {

            if (gameObject.name == "Button2")
            {
                T1 = true;
            }
            else
            {
                T1 = false;
            }

        }
        if (P2.gameObject.activeSelf == true) //틀린 경우(011)
        {
            if (gameObject.name == "Button1")
            {
                T1 = true;
            }
            else
            {
                T1 = false;
            }

        }
        if (P3.gameObject.activeSelf == true)   //틀린 경우(110)
        {
            if (gameObject.name == "Button3")
            {
                T1 = true;
            }
            else
            {
                T1 = false;
            }

        }
        Judge();
        ChangePattern();

        successText.text = "Count_S : " + refGameMgr.Count_S.ToString();
        failText.text = "Count_F : " + refGameMgr.Count_F.ToString();
        comboText.text = "Count_C : " + refGameMgr.Count_C.ToString();
        maxText.text = "Count_M : " + refGameMgr.Count_M.ToString();
    }
}