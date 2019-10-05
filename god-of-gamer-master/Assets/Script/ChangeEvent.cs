using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeEvent : MonoBehaviour
{

    public GameObject P4;
    public GameObject P5;
    public GameObject P6;

    Vector2 MousePosition;
    Camera Camera;

    private void Start()
    {
        Camera = Camera.main.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MousePosition = Input.mousePosition;
            MousePosition = Camera.ScreenToWorldPoint(MousePosition);

            Debug.Log(MousePosition);
        }
    }

    /*
    GraphicRaycaster graphicRaycaster;
    PointerEventData pointEventData;
    private void Start()
    {
        // 그래픽레이캐스트 컴퍼넌트를 가져옴
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        // 이벤트시스템의 포인트이벤트데이터
        pointEventData = new PointerEventData(null);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckObject();
        }
    }
    void CheckObject()
    {
        // 마우스 위치정보
        pointEventData.position = Input.mousePosition;
        List<RaycastResult> result = new List<RaycastResult>();
        // 레이캐스트의 결과를 리스트에 담아둔다
        graphicRaycaster.Raycast(pointEventData, result);
        if (result.Count != 0)
        {
            for (int i = 0; i < result.Count; i++)
            {
                Debug.Log("뭐가 들어있냐 = " + result[i]);
            }
            GameObject obj = result[0].gameObject;
            Debug.Log("누가 클릭되었냐 = " + obj.name);
        }
    }
    */



}
