using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileImageScrollCtrl : MonoBehaviour
{
    private RectTransform rectTr;

    [Header("Swipe Control")]
    public float sensitivity = 100;
    public float lerpSpeed = 15;
    private Vector2 startPos;
    private Vector2 endPos;
    private float[] points;

    [Header("생성할 프로필 이미지 프리팹")]
    public GameObject profileImageIconPrefab;

    // 프로필 이미지 아이콘 관리
    private List<GameObject> profileImageIcons = new List<GameObject>();

    [HideInInspector]
    public int currPointNum = 0;
    private int profileImageNum = 0;
    private float imageSize;

    void Start()
    {
        rectTr = GetComponent<RectTransform>();

        SetProfileImageIcons();
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
                }
            }
            // 오른쪽으로 이동
            else if (dir.x < -sensitivity)
            {
                if (currPointNum != points.Length - 1)
                {
                    currPointNum += 1;
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

    public void SetProfileImageIcons()
    {
        // Profile Image Icon 생성
        if (profileImageIcons.Count > 0)
        {
            return;
        }

        int count = GameManager.Instance.profileImages.Length;

        for (int i = 0; i < count; i++)
        {
            GameObject icon = Instantiate(profileImageIconPrefab, transform);
            icon.GetComponent<Image>().sprite = GameManager.Instance.profileImages[i];

            if (i == 0)
            {
                imageSize = icon.GetComponent<RectTransform>().sizeDelta.x;
            }

            profileImageIcons.Add(icon);
        }

        // 이동할 수 있는 지점 생성
        points = new float[count];

        HorizontalLayoutGroup layoutGroup = GetComponent<HorizontalLayoutGroup>();
        float spacing = layoutGroup.spacing;

        for (int i = 0; i < count; i++)
        {
            if (i == 0)
            {
                points[i] = 0;
            }
            else
            {
                float gap = imageSize + spacing;
                points[i] = points[i - 1] - gap;
            }
        }
    }

    public void ClickProfileImageIcon(int num)
    {
        profileImageNum = num;
    }

    public void ChangeProfileImageNum()
    {
        GameManager.Instance.profileImageNum = profileImageNum;
        profileImageNum = 0;
    }
}
