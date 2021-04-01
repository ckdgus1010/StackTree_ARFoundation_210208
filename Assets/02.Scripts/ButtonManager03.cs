using System.Collections;
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
            cubeCtrl.PlusCube(pointerCtrl.guideCube);
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
        
        cubeCtrl.ResetCubeData();
    }

    // Game Board 리셋
    public void ResetGameBoard()
    {
        SoundManager.Instance.ClickButton();

        

        touchManager.ResetTouchData();
        cubeCtrl.ResetCubeData();
    }

    // 정답 확인
    public void CheckAnswer()
    {
        SoundManager.Instance.ClickButton();

        ModeType modeType = GameManager.Instance.modeType;

        // 혼자하기 유형01 - 큐브 개수 맞추기
        if (modeType == ModeType.Alone_Count)
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
        else if (modeType == ModeType.None || modeType == ModeType.Create)
        {
            Debug.LogError($"ButtonManager03 ::: 정답 확인 할 수 없음 // modeType = {modeType}");
        }
        else
        {
            if (cubeCtrl.list.Count > 0)
            {
                answerManager.CheckAnswerOthers();
            }

            cubeCtrl.list.Clear();
        }

        Debug.Log("ButtonManager03 ::: 정답 확인 끝");
    }

    // 정답 확인 후 [다시하기 버튼] 클릭
    public void ClickRetryGameButton()
    {
        SoundManager.Instance.ClickButton();

        answerManager.ResetOXPanel();
        ResetGameBoard();
        Debug.Log("ButtonManager03 ::: 다시하기 버튼 클릭");
    }

    // 정답 확인 후 [이어하기 버튼] 클릭
    public void ClickContinueGameButton()
    {
        SoundManager.Instance.ClickButton();

        answerManager.ResetOXPanel();
        Debug.Log("ButtonManager03 ::: 이어하기 버튼 클릭");
    }

    // 정답 확인 후 [다음 단계로 버튼] 클릭
    public void ClickNextLevelButton()
    {
        SoundManager.Instance.ClickButton();

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
        screenshotCtrl.Capture_Button();
    }

    // [설정 버튼] 클릭
    public void ClickSettingButton()
    {
        SoundManager.Instance.ClickButton();
        settingPanel.SetActive(!settingPanel.activeSelf);
        settingCtrl.SetSoundData(settingPanel.activeSelf);
    }

    // [재도전 버튼] 클릭
    public void ClickGameRetryButton()
    {
        SoundManager.Instance.ClickButton();
        ResetGameBoard();
        ClickSettingButton();
    }

    // [나가기 버튼] 클릭
    public void ClickGameExitButton()
    {
        SoundManager.Instance.ClickButton();
        SceneManager.LoadScene("01. Main Scene");
    }
}
