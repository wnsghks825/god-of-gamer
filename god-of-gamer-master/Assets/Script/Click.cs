using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 어떤의도?
/// 클릭 시 점수 증가 & 표정 피드백
/// 클릭했을 때 화면 전환
/// </summary>
public class Click : MonoBehaviour
{
    public float failTime;  // 실패 시 얼굴 지속시간
    public float delayTime; //fever시 화면 전환 딜레이

    bool judge = false;   //false 가 success 반환
    bool isFever = false;

    public GameObject Face; //표정을 밖으로 빼자
    GameManager refGameMgr;

    public GameObject[] P;

    [SerializeField]
    Image Phase;
     
    Sprite Phase1, Phase2, Phase3;

    private void Awake()
    {
        refGameMgr = GameManager.s_Instance;
    }

    private void Start()
    {
        Phase1 = Resources.Load<Sprite>("P1");  //배열 말고 리스트!
        Phase2 = Resources.Load<Sprite>("P2");  
        Phase3 = Resources.Load<Sprite>("P3"); 

    }
    void Active(GameObject P)
    {
        P.gameObject.SetActive(true);
    }
    void Deactive(GameObject P)
    {
        P.gameObject.SetActive(false);
    }

    void ChangePattern()    //패턴 전환
    {
        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                {
                    /*
                    Active(P[0]);
                    Deactive(P[1]);
                    Deactive(P[2]);
                    */
                    Phase.sprite = Phase1;
                    break;
                }
            case 1:
                {
                    /*
                    Active(P[1]);
                    Deactive(P[0]);
                    Deactive(P[2]);
                    */
                    Phase.sprite = Phase2;
                    break;
                }
            case 2:
                {
                    /*
                    Active(P[2]);
                    Deactive(P[0]);
                    Deactive(P[1]);
                    */
                    Phase.sprite = Phase3;
                    break;
                }
        }
    }

    public void ToggleFever()
    {

    }

    IEnumerator Fever() //피버 시 애니메이션 변경
    {
        P[0].gameObject.SetActive(false);
        P[1].gameObject.SetActive(false);
        P[2].gameObject.SetActive(false);
        for (int i = 3; i < 6; i++)
        {
            P[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(delayTime);
            P[i].gameObject.SetActive(false);

            if (i == 5) { i = 2; }
        }

    }

    void Judge()    //증가시킬 Count 변수 판별
    {
        if (judge == true)
        {
            refGameMgr.Count_F++;
            if (refGameMgr.Count_M < refGameMgr.Count_C)
            {
                refGameMgr.Count_M = refGameMgr.Count_C;
            }
            refGameMgr.Count_C = 0;

            StartCoroutine(WaitFor());
        }
        else
        {
            refGameMgr.Count_S++;
            refGameMgr.Count_C++;
        }
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

    public void FeverEvent()
    {
        isFever = true;
        judge = false;
        Judge();
        StartCoroutine(Fever());
    }

    public void ClickEvents()   //ButtonManager를 써볼까?
    {
        string name = Phase.sprite.name;
        string[] BT = { "준환트위치", "준환냄새", "준환바보" };
        int n = 0;
        
        if (name == "P1")   //틀린 경우(101)
        {
            if (gameObject.name == BT[n])
            {
                judge = true;

            }
            else
            {
                judge = false;
            }
        }
        if (name == "P2")   //틀린 경우(101)
        {
            if (gameObject.name == "준환냄새")
            {
                judge = true;

            }
            else
            {
                judge = false;
            }
        }
        if (name == "P3")   //틀린 경우(101)
            {
            if (gameObject.name == "준환바보")
            {
                judge = true;
            }
            else
            {
                judge = false;
            }
        }
        Judge();
        ChangePattern();
    }
}