using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public Text successText;
    public Text failText;
    public Text comboText;
    public Text maxText;

    // Update is called once per frame
    void Update()
    {
        var refGameMgr = GameManager.s_Instance;

        successText.text = "Count_S : " + refGameMgr.Count_S.ToString();
        failText.text = "Count_F : " + refGameMgr.Count_F.ToString();
        comboText.text = "Count_C : " + refGameMgr.Count_C.ToString();
        maxText.text = "Count_M : " + refGameMgr.Count_M.ToString();
    }
}
