﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager03 : MonoBehaviour
{
    public TouchManager touchManager;
    public PointerCtrl pointerCtrl;
    public AnswerManager answerManager;

    [HideInInspector]
    public GameboardCtrl gameboardCtrl;

    [Header("Create Mode")]
    public GamePanelCtrl gamePanelCtrl;
    public GameObject gamePanel_Create;
    public GameObject playSceneCanvas;

    [Header("게임 설정")]
    public GameObject settingPanel;
    public SettingCtrl settingCtrl;

    [Header("스크린샷")]
    public ScreenshotCtrl screenshotCtrl;

    [Header("Cube Info")]
    public CubeCtrl cubeCtrl;

    [HideInInspector]
    public GameObject cardboard;
    [HideInInspector]
    public InputField inputField;
    [HideInInspector]
    public GameObject oxPanel;

    // Create Mode - Game Panel 전환
    public void SetGamePanel_Create()
    {
        SoundManager.Instance.ClickButton();

        gamePanelCtrl.createModeGamePanel.SetActive(true);
        gamePanelCtrl.currGamePanel.SetActive(false);
    }

    // Create Mode - Grid 크기 조절
    public void SetGameboardGrid(int value)
    {
        GameboardCtrl gameBoardCtrl = touchManager.currGameboard.GetComponent<GameboardCtrl>();
        gameBoardCtrl.SetGameboardGrid(value);
        pointerCtrl.guideCube.transform.localScale = gameBoardCtrl.GetCubeScale();
    }

    // 카드 보기
    public void ShowCard()
    {
        Debug.Log("카드 보기");

        SoundManager.Instance.ClickButton();
        
        if (cardboard != null)
        {
            cardboard.SetActive(!cardboard.activeSelf);
        }
        else
        {
            Debug.Log("ButtonManager03 ::: 카드 없음");
        }
    }

    // 큐브 쌓기
    public void PlusCube()
    {
        SoundManager.Instance.ClickButton();

        if (pointerCtrl.guideCube.activeSelf == true)
        {
            if (GameManager.Instance.modeType == ModeType.Alone_Minus)
            {

            }
            else
            {
                cubeCtrl.PlusCube(pointerCtrl.guideCube);
            }
        }
    }

    // 큐브 빼기
    public void MinusCube()
    {
        SoundManager.Instance.ClickButton();

        if (pointerCtrl.currCube != null)
        {
            if (GameManager.Instance.modeType == ModeType.Alone_Minus)
            {
                pointerCtrl.currCube.SetActive(false);
            }
            else
            {
                cubeCtrl.MinusCube(pointerCtrl.currCube);
            }
        }
    }

    // 큐브 리셋
    public void ResetCubes()
    {
        SoundManager.Instance.ClickButton();

        if (GameManager.Instance.modeType == ModeType.Alone_Minus)
        {
            gameboardCtrl.ResetCubeData();
        }
        else
        {
            cubeCtrl.ResetCubeData();
        }
    }

    // Game Board 리셋
    public void ResetGameBoard()
    {
        SoundManager.Instance.ClickButton();

        touchManager.ResetTouchData();
        cubeCtrl.ResetCubeData();

        Debug.Log("ButtonManager03 ::: 보드 삭제");
    }

    // 정답 확인
    public void CheckAnswer()
    {
        Debug.Log("ButtonManager03 ::: 정답 확인 시작");

        SoundManager.Instance.ClickButton();

        // 혼자하기 유형01 - 큐브 개수 맞추기
        if (GameManager.Instance.modeType == ModeType.Alone_Count)
        {
            if (inputField != null)
            {
                answerManager.CheckAnwerAloneMode01(inputField);
                Debug.Log("ButtonManager03 ::: 정답 확인");
            }
            else
            {
                Debug.LogError("ButtonManager03 ::: inputField 없음");
            }
        }
        // 혼자하기 유형 02 - 카드 보고 큐브 빼기
        else if (GameManager.Instance.modeType == ModeType.Alone_Minus)
        {

        }
        // 혼자하기 유형 03 - 카드 보고 큐브 쌓기
        else if (GameManager.Instance.modeType == ModeType.Alone_Plus)
        {

        }

        Debug.Log("ButtonManager03 ::: 정답 확인 끝");
    }

    // 정답 확인 후 [다시하기 버튼] 클릭
    public void ClickRetryGameButton()
    {
        answerManager.ResetOXPanel();
        ResetGameBoard();
        Debug.Log("ButtonManager03 ::: 다시하기 버튼 클릭");
    }

    // 정답 확인 후 [이어하기 버튼] 클릭
    public void ClickContinueGameButton()
    {
        answerManager.ResetOXPanel();
        Debug.Log("ButtonManager03 ::: 이어하기 버튼 클릭");
    }

    // 정답 확인 후 [다음 단계로 버튼] 클릭
    public void ClickNextLevelButton()
    {
        answerManager.ResetOXPanel();
        answerManager.ClickNextLevelButton(playSceneCanvas);
        Debug.Log("ButtonManager03 ::: 다음 단계로 버튼 클릭");
    }


    // Game Board 크기 조절
    public void ChangeBoardSize(float value)
    {
        touchManager.ChangeBoardSize(value);
    }

    // [스크린샷 버튼] 클릭
    public void ClickScreenshotButton()
    {
        Debug.Log($"ButtonManager03 ::: 스크린샷 버튼 클릭");

        screenshotCtrl.Capture_Button();
    }

    // [설정 버튼] 클릭
    public void ClickSettingButton()
    {
        Debug.Log("ButtonManager03 ::: 설정 버튼 클릭");

        SoundManager.Instance.ClickButton();
        settingPanel.SetActive(!settingPanel.activeSelf);
        settingCtrl.SetSoundData(settingPanel.activeSelf);
    }

    // [재도전 버튼] 클릭
    public void ClickGameRetryButton()
    {
        Debug.Log("ButtonManager03 ::: [재도전 버튼] 클릭");

        SoundManager.Instance.ClickButton();
        ResetGameBoard();
        ClickSettingButton();
    }

    // [나가기 버튼] 클릭
    public void ClickGameExitButton()
    {
        Debug.Log("ButtonManager03 ::: [나가기 버튼] 클릭");

        SoundManager.Instance.ClickButton();
        SceneManager.LoadScene("01. Main Scene");
    }
}
