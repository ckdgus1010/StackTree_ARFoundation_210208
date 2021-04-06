using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager02 : MonoBehaviour
{
    private GameObject currCanvas;
    [HideInInspector]
    public bool isARPlayOn = false;

    [Header("방 목록 화면 & 대기 화면")]
    public GameObject cam;
    public GameObject roomListCanvas;
    public GameObject roomWaitingCanvas;

    [Header("같이하기 모드")]
    public GameObject arSession;
    public GameObject arSessionOrigin;
    public GameObject togetherPlayCanvas;

    void Start()
    {
        isARPlayOn = false;
        currCanvas = roomListCanvas;
    }

    // 방 목록 화면으로 전환하는 경우
    public void OpenRoomListCanvas()
    {
        // Play Mode 상태 전환
        GameManager.Instance.modeType = ModeType.Together;

        isARPlayOn = false;
        ChangeCanvas(isARPlayOn, roomListCanvas);
    }

    // 대기 화면으로 전환하는 경우
    public void OpenRoomWaitingCanvas()
    {
        isARPlayOn = false;
        ChangeCanvas(isARPlayOn, roomWaitingCanvas);
    }

    // 플레이를 시작하는 경우
    public void OpenTogetherPlayCanvas()
    {
        isARPlayOn = true;
        ChangeCanvas(isARPlayOn, togetherPlayCanvas);
    }

    void ChangeCanvas(bool _isARPlayOn, GameObject canvasObj)
    {
        // 카메라 전환
        if (_isARPlayOn == true)
        {
            arSession.SetActive(true);
            arSessionOrigin.SetActive(true);
            cam.SetActive(false);
        }
        else
        {
            cam.SetActive(true);
            arSession.SetActive(false);
            arSessionOrigin.SetActive(false);
        }

        // Canvas 전환
        if (currCanvas != null)
        {
            currCanvas.SetActive(false);
        }
        currCanvas = canvasObj;
        currCanvas.SetActive(true);
    }
}
