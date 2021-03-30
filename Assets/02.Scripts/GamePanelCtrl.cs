using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelCtrl : MonoBehaviour
{
    public ButtonManager03 buttonManager;
    public GameObject playSceneCanvas;

    [Header("Information Panel")]
    public GameObject notePanel;
    private GameObject _notePanel;
    public float timer = 1.5f;

    [Header("Game Panel Prefab")]
    public GameObject gamePanel_BoardSize;
    public GameObject gamePanel_Create;
    public GameObject gamePanel01;
    public GameObject gamePanel02;

    [Header("스크린샷")]
    public GameObject screenshotButtonPrefab;
    private GameObject screenshotButton;
    public ScreenshotCtrl screenshotCtrl;

    [HideInInspector]
    public GameObject currGamePanel;
    [HideInInspector]
    public GameObject createModeGamePanel;

    void Start()
    {
        _notePanel = Instantiate(notePanel, playSceneCanvas.transform);
        Invoke("CloseNotePanel", timer);

        SetCurrGamePanel();
    }

    void CloseNotePanel()
    {
        _notePanel.SetActive(false);
    }

    void SetCurrGamePanel()
    {
        ModeType modeType = GameManager.Instance.modeType;

        // Create Mode인 경우
        if (modeType == ModeType.Create)
        {
            currGamePanel = Instantiate(gamePanel_BoardSize, playSceneCanvas.transform);

            createModeGamePanel = Instantiate(gamePanel_Create, playSceneCanvas.transform);
            createModeGamePanel.GetComponent<GamePanelData>().SetGamePanel(buttonManager, modeType);
            createModeGamePanel.SetActive(false);
            screenshotCtrl.UIList.Add(createModeGamePanel);
        }
        // 혼자하기 모드 유형 01인 경우
        else if (modeType == ModeType.Alone_Count)
        {
            currGamePanel = Instantiate(gamePanel01, playSceneCanvas.transform);
        }
        // 혼자하기 모드 유형 02, 03인 경우
        else
        {
            currGamePanel = Instantiate(gamePanel02, playSceneCanvas.transform);
        }

        currGamePanel.GetComponent<GamePanelData>().SetGamePanel(buttonManager, modeType);
        currGamePanel.SetActive(false);
        screenshotCtrl.UIList.Add(currGamePanel);
    }

    public void ResetCurrGamePanel()
    {
        if (GameManager.Instance.modeType == ModeType.Create)
        {
            createModeGamePanel.transform.GetChild(1).GetComponent<Slider>().value = 1;
            createModeGamePanel.SetActive(false);
            currGamePanel.transform.GetChild(0).GetComponent<Slider>().value = 5;
        }
        else
        {
            ConvertGamePanel();
        }
    }

    public void ConvertGamePanel()
    {
        SetScreenshotButton();

        if (GameManager.Instance.modeType == ModeType.Create)
        {
            currGamePanel.transform.GetChild(0).GetComponent<Slider>().value = 5;
        }
        else
        {
            currGamePanel.transform.GetChild(1).GetComponent<Slider>().value = 1.0f;
        }

        currGamePanel.SetActive(!currGamePanel.activeSelf);
        Debug.Log($"GamePanelCtrl ::: {currGamePanel.name} // {currGamePanel.activeSelf}");
    }

    private void SetScreenshotButton()
    {
        if (screenshotButton == null)
        {
            screenshotButton = Instantiate(screenshotButtonPrefab, playSceneCanvas.transform);
            screenshotButton.GetComponent<Button>().onClick.AddListener(() => buttonManager.ClickScreenshotButton());
            screenshotCtrl.UIList.Add(screenshotButton);
            Debug.Log($"GamePanelCtrl ::: {screenshotButton.name}");
        }
        else
        {
            screenshotButton.SetActive(!screenshotButton.activeSelf);
        }
    }
}
