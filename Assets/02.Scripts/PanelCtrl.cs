using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelCtrl : MonoBehaviour
{
    [Header("Panel 도착 지점 설정")]
    public Vector2 destination;
    private RectTransform rectTr;
    private Vector2 startPos;
    private Vector2 movingPoint;

    [HideInInspector]
    public bool isButtonClicked = false;
    public float lerpSpeed = 7.5f;

    void Start()
    {
        rectTr = GetComponent<RectTransform>();
        startPos = rectTr.anchoredPosition;
        isButtonClicked = false;
    }

    void Update()
    {
        movingPoint = isButtonClicked ? destination : startPos;
        rectTr.anchoredPosition = Vector2.Lerp(rectTr.anchoredPosition, movingPoint, lerpSpeed * Time.deltaTime);
    }

    public void ResetPanelData()
    {
        rectTr.anchoredPosition = startPos;
        isButtonClicked = false;
    }
}
