using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CanvasName
{
    Intro, Login, Main, Credit
}

public class CanvasManager : MonoBehaviour
{
    public GameObject[] canvasArray;
    public SwipeMenu swipeMenu;

    void Start()
    {
        ModeType playModeType = GameManager.Instance.modeType;

        switch (playModeType)
        {
            case ModeType.None:
                canvasArray[(int)CanvasName.Intro].SetActive(true);
                break;
            case ModeType.Create:
                //canvasArray[(int)CanvasName.Main].SetActive(true);
                //break;
            case ModeType.Alone_Count:
            case ModeType.Alone_Minus:
            case ModeType.Alone_Plus:
                //canvasArray[(int)CanvasName.Main].SetActive(true);
                //break;
            case ModeType.Together:
            case ModeType.Together_Two:
            case ModeType.Together_Three:
                canvasArray[(int)CanvasName.Main].SetActive(true);
                break;
        }

        GameManager.Instance.modeType = ModeType.None;
        GameManager.Instance.stageID = 0;
    }

    // 인트로 영상 >> 로그인 화면
    public void FinishIntroVideo()
    {
        // BGM

        canvasArray[(int)CanvasName.Login].SetActive(true);
        canvasArray[(int)CanvasName.Intro].SetActive(false);
    }

    // 로그인 화면 >> 메인 화면
    public void ClickLoginButton()
    {
        canvasArray[(int)CanvasName.Main].SetActive(true);
        canvasArray[(int)CanvasName.Login].SetActive(false);
    }

    // 메인 화면 >> 크레딧 화면
    public void ClickCreditButton()
    {
        canvasArray[(int)CanvasName.Credit].SetActive(true);
        canvasArray[(int)CanvasName.Main].SetActive(false);
    }

    // 크레딧 화면 >> 메인 화면
    public void ClickCreditSkipButton()
    {
        canvasArray[(int)CanvasName.Main].SetActive(true);
        canvasArray[(int)CanvasName.Credit].SetActive(false);
    }
}
