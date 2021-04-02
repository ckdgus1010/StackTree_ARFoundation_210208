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
    [HideInInspector]
    public CheckerboardCtrl checkerboardCtrl;

    // 정답 확인(혼자하기 모드 유형 01 - 큐브 보고 개수 맞추기)
    public void CheckAnwerAloneMode01(InputField inputField)
    {
        if (string.IsNullOrEmpty(inputField.text) == false)
        {
            int stageID = GameManager.Instance.stageID - 1;
            int totalCount = aloneModeQuestCtrl.totalCount;

            if (inputField.text == totalCount.ToString())
            {
                Debug.Log("AnswerManager ::: 정답입니다.");
                isCorrect = true;

                GameManager.Instance.currStageStateArray[stageID] = AloneModeStageState.Cleared;

                if (stageID != GameManager.Instance.currStageStateArray.Count - 1)
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
        }
    }

    // 정답 확인(그외 나머지 모드)
    public void CheckAnswerOthers()
    {
        // 문제 데이터를 받아옴
        int gridSize = aloneModeQuestCtrl.gridSize;
        string frontAnswer = aloneModeQuestCtrl.front;
        string sideAnswer = aloneModeQuestCtrl.side;
        string topAnswer = aloneModeQuestCtrl.top;

        // 정답을 확인할 Checkerboard 활성화
        checkerboardCtrl.SetCheckerboard(gridSize);

        // 플레이어의 답안을 받아옴
        checkerboardCtrl.GetPlayerAnswerData(gridSize);
        string front = checkerboardCtrl.playerAnswers[0];
        string side = checkerboardCtrl.playerAnswers[1];
        string top = checkerboardCtrl.playerAnswers[2];

        // 둘을 비교
        int answerCount = 0;

        // 앞면 확인
        if (front == frontAnswer)
        {
            answerCount += 1;
            Debug.Log($"AnswerManager ::: 앞면 정답 {front}");
        }
        else
        {
            Debug.Log($"AnswerManager ::: 앞면 틀림 {front} // {frontAnswer}");
        }

        // 옆면 확인
        if (side == sideAnswer)
        {
            answerCount += 1;
            Debug.Log($"AnswerManager ::: 옆면 정답 {side}");
        }
        else
        {
            Debug.Log($"AnswerManager ::: 옆면 틀림 {side} // {sideAnswer}");
        }

        // 윗면 확인
        if (top == topAnswer)
        {
            answerCount += 1;
            Debug.Log($"AnswerManager ::: 윗면 정답 {top}");
        }
        else
        {
            Debug.Log($"AnswerManager ::: 윗면 틀림 {top} // {topAnswer}");
        }

        // 정오답 확인
        bool isPlayerRight = answerCount == 3 ? true : false;
        Debug.Log($"AnswerManager ::: answerCount = {answerCount}");

        // OX Panel
        SetOXPanel(isPlayerRight);
    }




    void SetOXPanel(bool _isCorrect)
    {
        if (oxPanel == null)
        {
            oxPanel = Instantiate(panelPrefab, worldCanvas.transform);
            GameObject obj = oxPanel.transform.GetChild(1).gameObject;

            titleText = obj.transform.GetChild(0).GetComponent<Text>();
            panelImage = obj.transform.GetChild(1).GetComponent<Image>();

            Button retryButton = obj.transform.GetChild(2).GetComponent<Button>();
            retryButton.onClick.AddListener(() => buttonManager.ClickRetryGameButton());

            Button continueButton = obj.transform.GetChild(3).GetComponent<Button>();
            continueButton.onClick.AddListener(() => buttonManager.ClickContinueGameButton());
            
            nextLevelButton = obj.transform.GetChild(4).GetComponent<Button>();
            nextLevelButton.onClick.AddListener(() => buttonManager.ClickNextLevelButton());

            exitGameButton = obj.transform.GetChild(5).GetComponent<Button>();
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

            UpdateStageState();
        }
        else
        {
            titleText.text = "틀렸습니다!!";
            exitGameButton.transform.gameObject.SetActive(true);
            nextLevelButton.transform.gameObject.SetActive(false);

            // 업적 정보 업데이트(최초 오답)
            AchievementManager.Instance.UpdateAchievementData(AchievementState.Fail_Count);
        }
    }

    void UpdateStageState()
    {
        int stageID = GameManager.Instance.stageID - 1;
        GameManager.Instance.currStageStateArray[stageID] = AloneModeStageState.Cleared;

        // 업적 정보 업데이트(혼자하기 - 단계 모두 클리어)
        AchievementManager.Instance.UpdateAchievementData(AchievementState.AloneModeClear_Count);

        int count = GameManager.Instance.currStageStateArray.Count - 1;
        if (stageID != count)
        {
            int _stageID = stageID + 1;
            GameManager.Instance.currStageStateArray[_stageID] = AloneModeStageState.Current;
        }
        else
        {
            Debug.Log($"AnswerManager ::: 축하합니다. {GameManager.Instance.modeType}의 모든 스테이지를 클리어했습니다.");
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
