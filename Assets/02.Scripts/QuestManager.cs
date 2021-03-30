using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region Singleton Pattern

    private static QuestManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static QuestManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    // ----------------------------------------------
    [Header("모드별 Game Data")]
    public List<QuestData> alone_Count;
    public List<QuestData> alone_Minus;
    public List<QuestData> alone_Plus;
    public List<QuestData> together_Two;
    public List<QuestData> together_Three;
    public List<QuestData> currQuest;

    [Header("Grid Image")]
    public Sprite[] emptySprites;
    public Sprite[] fillSprites;

    void Start()
    {
        LoadQuestData();
        SetCurrQuest();
    }

    // Quest Data 불러오기
    void LoadQuestData()
    {
        // 각 모드별 Dictionary 선언
        alone_Count = new List<QuestData>();
        alone_Minus = new List<QuestData>();
        alone_Plus = new List<QuestData>();
        together_Two = new List<QuestData>();
        together_Three = new List<QuestData>();

        // csv 파일 읽어오기
        List<Dictionary<string, object>> data = CSVReader.Read("QuestData");

        // 불러온 정보를 알맞은 Dictionary로 분류
        for (int i = 0; i < data.Count; i++)
        {
            string modeType = data[i]["ModeType"].ToString();
            int stageID = (int)data[i]["StageID"];
            int gridSize = (int)data[i]["GridSize"];
            string front = data[i]["FrontQuestData"].ToString();
            string side = data[i]["SideQuestData"].ToString();
            string top = data[i]["TopQuestData"].ToString();
            QuestData questData = new QuestData(modeType, stageID, gridSize, front, side, top);

            switch (modeType)
            {
                case "Alone_Count":
                    alone_Count.Add(questData);
                    break;
                case "Alone_Minus":
                    alone_Minus.Add(questData);
                    break;
                case "Alone_Plus":
                    alone_Plus.Add(questData);
                    break;
                case "Together_Two":
                    together_Two.Add(questData);
                    break;
                case "Together_Three":
                    together_Three.Add(questData);
                    break;
                case "None":
                    Debug.LogError($"QuestManager ::: data[{i}][ModeType] = {modeType}");
                    break;
            }
        }
    }

    public void SetCurrQuest()
    {
        switch(GameManager.Instance.modeType)
        {
            case ModeType.Create:
            case ModeType.None:
                Debug.Log($"QuestManager ::: {GameManager.Instance.modeType} // currQuest is null");
                break;
            case ModeType.Alone_Count:
                currQuest = alone_Count;
                break;
            case ModeType.Alone_Minus:
                currQuest = alone_Minus;
                break;
            case ModeType.Alone_Plus:
                currQuest = alone_Plus;
                break;
            case ModeType.Together_Two:
                currQuest = together_Two;
                break;
            case ModeType.Together_Three:
                currQuest = together_Three;
                break;
        }
    }
}
