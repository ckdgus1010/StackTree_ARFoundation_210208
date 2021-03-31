using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerManager : MonoBehaviour
{
    public ButtonManager03 buttonManager;

    [Header("OX Panel")]
    public GameObject panelPrefab;
    public GameObject worldCanvas;
    public Sprite spriteO;
    public Sprite spriteX;
    private GameObject oxPanel;
    private Image panelImage;
    private bool isCorrect;
    private Text titleText;
    private Button nextLevelButton;
    private Button exitGameButton;

    [HideInInspector]
    public AloneModeQuestCtrl aloneModeQuestCtrl;

    // 정답 확인(혼자하기 모드 유형 01 - 큐브 보고 개수 맞추기)
    public void CheckAnwerAloneMode01(InputField inputField)
    {
        if (string.IsNullOrEmpty(inputField.text) == false)
        {
            int stageID = GameManager.Instance.stageID - 1;
            string top = QuestManager.Instance.currQuest[stageID].GetTopInfo();
            int total = 0;

            for (int i = 0; i < top.Length; i++)
            {
                int num = int.Parse(top.Substring(i, 1));
                total += num;
            }

            if (inputField.text == total.ToString())
            {
                Debug.Log("AnswerManager ::: 정답입니다.");
                isCorrect = true;

                GameManager.Instance.currStageStateArray[stageID] = AloneModeStageState.Cleared;

                if (stageID != GameManager.Instance.currStageStateArray.Length - 1)
                {
                    GameManager.Instance.currStageStateArray[stageID + 1] = AloneModeStageState.Current;
                    Debug.Log("AnswerManager ::: 축하합니다. 모든 스테이지를 클리어했습니다.");
                }

                inputField.text = "";
            }
            else
            {
                Debug.Log("AnswerManager ::: 틀렸습니다.");
                isCorrect = false;
            }

            SetOXPanel(isCorrect);
            Debug.Log($"AnswerManager ::: total // txt = {total} // {inputField.text}");
        }
    }

    void SetOXPanel(bool _isCorrect)
    {2
        if (oxPanel == null)
        {
            oxPanel = Instantiate(panelPrefab, worldCanvas.transform);

            titleText = oxPanel.transform.GetChild(0).GetComponent<Text>();
            panelImage = oxPanel.transform.GetChild(1).GetComponent<Image>();

            Button retryButton = oxPanel.transform.GetChild(2).GetComponent<Button>();
            retryButton.onClick.AddListener(() => buttonManager.ClickRetryGameButton());

            Button continueButton = oxPanel.transform.GetChild(3).GetComponent<Button>();
            continueButton.onClick.AddListener(() => buttonManager.ClickContinueGameButton());
            
            nextLevelButton = oxPanel.transform.GetChild(4).GetComponent<Button>();
            nextLevelButton.onClick.AddListener(() => buttonManager.ClickNextLevelButton());

            exitGameButton = oxPanel.transform.GetChild(5).GetComponent<Button>();
            exitGameButton.onClick.AddListener(() => buttonManager.ClickGameExitButton());
        }
        else
        {
            oxPanel.SetActive(true);
        }

        panelImage.sprite = _isCorrect == true ? spriteO : spriteX;
        
        if (_isCorrect == true)
        {
            titleText.text = "정답입니다 :D";
            nextLevelButton.transform.gameObject.SetActive(true);
            exitGameButton.transform.gameObject.SetActive(false);
        }
        else
        {
            titleText.text = "틀렸습니다!!";
            exitGameButton.transform.gameObject.SetActive(true);
            nextLevelButton.transform.gameObject.SetActive(false);
        }
    }

    public void ResetOXPanel()
    {
        panelImage.sprite = null;
        oxPanel.SetActive(false);
    }

    public void ClickNextLevelButton(GameObject playSceneCanvas)
    {
        GameManager.Instance.stageID += 1;
        aloneModeQuestCtrl.SetAloneModeQuest(buttonManager, playSceneCanvas);
    }
}
