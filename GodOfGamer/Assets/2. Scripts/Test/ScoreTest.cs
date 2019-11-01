using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GodOfGamer;

// 말그대로 테스트용
public class ScoreTest : MonoBehaviour
{
    private Score _score;

    public Text[] textArr;

    // Start is called before the first frame update
    void Start()
    {
        _score = GameMgr.s_Instance.score;
    }

    // Update is called once per frame
    void Update()
    {
        if (textArr.Length == 4)
        {
            textArr[0].text = _score.success.ToString();
            textArr[1].text = _score.failure.ToString();
            textArr[2].text = _score.max.ToString();
            textArr[3].text = _score.combo.ToString();
        }
    }
}
