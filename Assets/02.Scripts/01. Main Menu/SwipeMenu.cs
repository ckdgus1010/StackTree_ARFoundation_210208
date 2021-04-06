using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    private RectTransform rectTr;
    private HorizontalLayoutGroup horizontalLayoutGroup;

    [Header("Swipe Control")]
    public Scrollbar horizontalScrollbar;
    public float lerpSpeed = 0.1f;
    public float sensitivity = 150.0f;
    private float[] points;
    private int currPointNum = 0;

    private Vector2 startPos;
    private Vector2 endPos;

    [SerializeField]
    private Toggle[] paginations = new Toggle[3];

    void Start()
    {
        rectTr = GetComponent<RectTransform>();
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();

        SetPointsData();

        // 기본 위치 세팅
        currPointNum = 0;
        rectTr.anchoredPosition = new Vector2(points[currPointNum], 0);
    }

    // 이동할 수 있는 위치 설정
    void SetPointsData()
    {
        int childCount = transform.childCount;
        points = new float[childCount];

        float panelSize = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta.x;
        float leftPadding = horizontalLayoutGroup.padding.left;
        float spacePadding = horizontalLayoutGroup.spacing;

        for (int i = 0; i < childCount; i++)
        {
            if (i == 0)
            {
                points[i] = panelSize + leftPadding;
            }
            else
            {
                float num = panelSize + spacePadding;
                points[i] = points[i - 1] - num;
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
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

            // startPos & endPos 초기화
            startPos = Vector2.zero;
            endPos = Vector2.zero;

            // 단순 터치인 경우
            if (dir.x == 0)
            {
                return;
            }

            // 왼쪽으로 이동
            if (dir.x > sensitivity)
            {
                if (currPointNum != 0)
                {
                    currPointNum -= 1;
                    paginations[currPointNum].isOn = true;
                }
            }
            // 오른쪽으로 이동
            else if (dir.x < -sensitivity)
            {
                if (currPointNum != points.Length - 1)
                {
                    currPointNum += 1;
                    paginations[currPointNum].isOn = true;
                }
            }
            else
            {
                return;
            }
        }

        Vector2 destination = new Vector2(points[currPointNum], 0);
        rectTr.anchoredPosition = Vector2.Lerp(rectTr.anchoredPosition, destination, lerpSpeed * Time.deltaTime);
    }

    public void ClickAloneModeButton()
    {
        currPointNum = 1;
        rectTr.anchoredPosition = new Vector2(points[currPointNum], 0);
        paginations[currPointNum].isOn = true;
    }
}
