using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelData : MonoBehaviour
{
    [Header("Game Board Info")]
    public Button boardResetButton;
    public Slider boardSizeSlider;

    [Header("Cube Info")]
    public Button cubeResetButton;
    public Button plusButton;
    public Button minusButton;

    [Header("Create Mode")]
    public Slider gridSizeSlider;
    public Button confirmButton;

    [Header("혼자하기")]
    public Button checkAnswerButton;
    public Button cardButton;

    // 버튼 기능 할당
    public void SetGamePanel(ButtonManager03 buttonManager, ModeType modeType)
    {
        if (modeType == ModeType.Create)
        {
            SetGamePanel_Create(buttonManager);
        }
        else if (modeType == ModeType.Alone_Count)
        {
            SetGamePanel01(buttonManager);
        }
        else if (modeType == ModeType.Alone_Minus || modeType == ModeType.Alone_Plus)
        {
            SetGamePanel01(buttonManager);
            SetGamePanel02(buttonManager);
        }
    }

    // Create Mode인 경우
    void SetGamePanel_Create(ButtonManager03 buttonManager)
    {
        if (gridSizeSlider != null)
        {
            gridSizeSlider.onValueChanged.AddListener(delegate { buttonManager.SetGameboardGrid((int)gridSizeSlider.value); });
            confirmButton.onClick.AddListener(() => buttonManager.SetGamePanel_Create());
        }
        else
        {
            // Game Board 리셋
            boardResetButton.onClick.AddListener(() => buttonManager.ResetGameBoard());

            // Game Board 크기 조절 Slider
            boardSizeSlider.onValueChanged.AddListener(delegate { buttonManager.ChangeBoardSize(boardSizeSlider.value); });

            cubeResetButton.onClick.AddListener(() => buttonManager.ResetCubes());
            plusButton.onClick.AddListener(() => buttonManager.PlusCube());
            minusButton.onClick.AddListener(() => buttonManager.MinusCube());
        }
    }

    // 혼자하기 유형 01인 경우
    void SetGamePanel01(ButtonManager03 buttonManager)
    {
        // Game Board 리셋
        boardResetButton.onClick.AddListener(() => buttonManager.ResetGameBoard());

        // Game Board 크기 조절 Slider
        boardSizeSlider.onValueChanged.AddListener(delegate { buttonManager.ChangeBoardSize(boardSizeSlider.value); });

        // 정답 확인 버튼
        checkAnswerButton.onClick.AddListener(() => buttonManager.CheckAnswer());
    }

    // 혼자하기 유형 02, 03인 경우
    void SetGamePanel02(ButtonManager03 buttonManager)
    {
        cubeResetButton.onClick.AddListener(() => buttonManager.ResetCubes());
        plusButton.onClick.AddListener(() => buttonManager.PlusCube());
        minusButton.onClick.AddListener(() => buttonManager.MinusCube());
        cardButton.onClick.AddListener(() => buttonManager.ShowCard());
    }
}
