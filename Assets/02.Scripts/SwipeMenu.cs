using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public Scrollbar horizontalScrollbar;
    public float lerpSpeed = 0.1f;
    public float sensitivity = 150.0f;

    public float value;
    private float intialValue;
    private Vector2 startPos;
    private Vector2 endPos;

    [SerializeField]
    private Toggle[] paginations = new Toggle[3];

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            intialValue = horizontalScrollbar.value;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;

            // 방향 확인
            Vector2 dir = endPos - startPos;
            //Debug.Log($"dir.x ::: {dir.x} // scrollbar.value = {horizontalScrollbar.value} \n startPos: {startPos} // endPos: {endPos}");

            // startPos & endPos 초기화
            startPos = Vector2.zero;
            endPos = Vector2.zero;

            // 단순 터치인 경우
            if (dir.x == 0)
            {
                return;
            }

            if (dir.x > sensitivity)
            {
                //Debug.Log("왼쪽으로 이동");

                if (intialValue < 0.75f)
                {
                    value = 0.0f;
                    paginations[0].isOn = true;
                }
                else if (intialValue > 0.75f)
                {
                    value = 0.5f;
                    paginations[1].isOn = true;
                }
            }
            else if (dir.x < -sensitivity)
            {
                //Debug.Log("오른쪽으로 이동");

                if (intialValue < 0.25f)
                {
                    value = 0.5f;
                    paginations[1].isOn = true;
                }
                else if (intialValue > 0.25f)
                {
                    value = 1.0f;
                    paginations[2].isOn = true;
                }
            }
            else
            {
                //Debug.Log($"SwipeMenu ::: {dir.x} 단순 터치");
                return;
            }
        }

        horizontalScrollbar.value = Mathf.Lerp(horizontalScrollbar.value, value, lerpSpeed * Time.deltaTime);
    }

    public void ClickAloneModeButton()
    {
        value = 0.5f;
        paginations[1].isOn = true;
    }
}
