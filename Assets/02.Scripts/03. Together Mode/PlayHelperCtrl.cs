using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayHelperCtrl : MonoBehaviour
{
    [Header("Swipe Control")]
    public float sensitivity = 100;
    public float lerpSpeed = 15;
    private Vector2 direction;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 destination;

    [Header("Panel Info")]
    public RectTransform content;
    public RectTransform infoPanel;
    private float[] panelPoints;
    private int panelPointNum = 0;

    [Header("Panel UI")]
    public Scrollbar scrollbar;
    public Button playButton;
    public Button skipButton;

    [Header("Pagination Info")]
    public RectTransform paginations;
    public GameObject paginationPrefab;

    void Start()
    {
        int childCount = content.childCount;
        panelPoints = new float[childCount];

        float panelSize = infoPanel.sizeDelta.x;

        for (int i = 0; i < panelPoints.Length; i++)
        {
            if (i == 0)
            {
                panelPoints[i] = (childCount - 1) * 0.5f * panelSize;
            }
            else
            {
                panelPoints[i] = panelPoints[i - 1] - panelSize;
            }
        }

        ResetPlayHelperData();
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            startPos = touch.position;
        }

        if (touch.phase == TouchPhase.Ended)
        {
            endPos = touch.position;
            direction = endPos - startPos;
        }

        if (direction.magnitude < sensitivity)
        {
            return;
        }

        // 왼쪽으로 이동
        if (direction.x > 0)
        {
            if (panelPointNum != 0)
            {
                destination = new Vector2(panelPoints[panelPointNum - 1], 0);
            }
        }
        // 오른쪽으로 이동
        else
        {
            if (panelPointNum != panelPoints.Length - 1)
            {
                destination = new Vector2(panelPoints[panelPointNum + 1], 0);
            }
        }

        content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, destination, lerpSpeed * Time.deltaTime);
    }

    public void SetButtonData(ButtonManager04 buttonManager)
    {
        playButton.onClick.AddListener(() => buttonManager.ClickPlayHelperHideButton());
        skipButton.onClick.AddListener(() => buttonManager.ClickPlayHelperHideButton());
    }

    public void ResetPlayHelperData()
    {
        panelPointNum = 0;
        content.anchoredPosition = new Vector2(panelPoints[panelPointNum], 0);
        scrollbar.value = 0;
    }
}
