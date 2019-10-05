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

    List<Sprite> phase = new List<Sprite>();

    [SerializeField]
    Image Phase;
     

    private void Awake()
    {
        refGameMgr = GameManager.s_Instance;
    }

    private void Start()
    {
        phase.Add(Resources.Load<Sprite>("P1"));
        phase.Add(Resources.Load<Sprite>("P2"));
        phase.Add(Resources.Load<Sprite>("P3"));
    }

    void ChangePattern()    //패턴 전환
    {
        int random = Random.Range(0, 3);    // 0 이상 3 미만

        Phase.sprite = phase[random];
    }

    public void ToggleFever()
    {

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

    public void ClickEvents()   //ButtonManager를 써볼까?
    {
        string name = Phase.sprite.name;
        int name2 = 0;
        string[] BT = { "준환트위치", "준환냄새", "준환바보" };
        int n = 0;
        if (gameObject.name == "준환트위치")
        {
            n = 0;
            if (gameObject.name == "준환냄새")
            {
                n = 1;

            }
            else {n = 2;}       
        }//버튼 누른다.

        if (name == "P1")   //틀린 경우(101) - 네임이 각각 P1,P2,P3인 경우를 체크하는 변수를 생성. 
        {
            if (gameObject.name == BT[0])
            {
                judge = true;

            }
            else
            {
                judge = false;
            }
            //Debug.Log("P" + n);
        }
        if (name == "P2")   //틀린 경우(101)
        {
            if (gameObject.name == BT[1])
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
            if (gameObject.name == BT[2])
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