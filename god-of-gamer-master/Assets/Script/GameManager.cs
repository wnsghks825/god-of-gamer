using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class GameManager : Singleton<GameManager>
{
    //점수
    public int Count_S { get; set; }
    public int Count_F { get; set; }
    public int Count_C { get; set; }
    public int Count_C2 { get; set; }
    public int Count_M { get; set; }
    public int Count_M2 { get; set; }
    public int Count_P { get; set; }
}

