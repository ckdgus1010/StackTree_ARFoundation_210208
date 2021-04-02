using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AloneModeQuestCtrl : MonoBehaviour
{
    [Header("CubePrefab")]
    public GameObject cubePrefab;

    [Header("Cardboard Info")]
    public GameObject cardboardPrefab;
    private GameObject cardboard;
    private CardCtrl cardCtrl;

    private GameObject gameboard;
    private GameObject cubeList;
    private GameObject[] gridGroup;
    private GameObject currGrid;
    private List<GameObject> list = new List<GameObject>();
    private int currGridSize = 0;

    private int gridCount = 0;
    private int stageID = 0;


    [HideInInspector] public int totalCount = 0;
    [HideInInspector] public int gridSize = 0;
    [HideInInspector] public string front;
    [HideInInspector] public string side;
    [HideInInspector] public string top;

    public void SetCurrGameboard(GameObject currGameboard)
    {
        if (gameboard == null)
        {
            gameboard = currGameboard;
            cubeList = gameboard.transform.GetChild(2).gameObject;
            gridGroup = gameboard.GetComponent<GameboardCtrl>().gridGroup;
        }
    }

    // 혼자하기 모드 문제 설정
    public void SetAloneModeQuest(ButtonManager03 buttonManager, GameObject playSceneCanvas)
    {
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Destroy(list[i]);
            }
            list.Clear();
        }

        ModeType modeType = GameManager.Instance.modeType;

        // 혼자하기 모드 - 유형 01
        if (modeType == ModeType.Alone_Count)
        {
            ChangeGridSize();

            string top = QuestManager.Instance.currQuest[stageID].GetTopInfo();
            
            totalCount = 0;

            // 문제에 맞춰 CubePrefab 생성
            for (int i = 0; i < gridCount; i++)
            {
                int cubeCount = int.Parse(top.Substring(i, 1));
                GameObject gridCell = currGrid.transform.GetChild(i).gameObject;
                totalCount += cubeCount;

                if (cubeCount != 0)
                {
                    for (int j = 0; j < cubeCount; j++)
                    {
                        GameObject obj = Instantiate(cubePrefab
                                                    , gridCell.transform.GetChild(j).transform.position
                                                    , gameboard.transform.rotation
                                                    , cubeList.transform);
                        obj.transform.localScale = gameboard.GetComponent<GameboardCtrl>().GetCubeScale();

                        list.Add(obj);
                    }
                }
            }
        }
        // 혼자하기 모드 - 유형 02
        else if (modeType == ModeType.Alone_Minus)
        {
            ChangeGridSize();

            // 각 Grid마다 Grid Size 값만큼 Cube 생성
            if (list.Count == 0)
            {
                for (int i = 0; i < gridCount; i++)
                {
                    GameObject obj = currGrid.transform.GetChild(i).gameObject;

                    for (int j = 0; j < obj.transform.childCount; j++)
                    {
                        GameObject cube = Instantiate(cubePrefab
                                                        , obj.transform.GetChild(j).transform.position
                                                        , gameboard.transform.rotation
                                                        , cubeList.transform);
                        cube.transform.localScale = gameboard.GetComponent<GameboardCtrl>().GetCubeScale();

                        list.Add(cube);
                    }
                }
            }
            else
            {
                ResetCubeData();
            }

            SetCardBoard(buttonManager, playSceneCanvas);
        }
        // 혼자하기 유형 03
        else
        {
            ChangeGridSize();

            SetCardBoard(buttonManager, playSceneCanvas);
        }
    }

    // Grid Size 변경
    void ChangeGridSize()
    {
        // Grid Size 받아오기
        QuestManager.Instance.SetCurrQuest();
        stageID = GameManager.Instance.stageID - 1;
        int gridSize = QuestManager.Instance.currQuest[stageID].GetGridSize();
        gridCount = gridSize * gridSize;

        if (currGridSize != gridSize)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Destroy(list[i]);
            }
            list.Clear();
        }

        currGridSize = gridSize;

        // Gameboard에서 Grid 가지고 오기
        if (currGrid != null)
        {
            currGrid.SetActive(false);
            currGrid = null;
        }
        currGrid = gridGroup[gridSize - 3];
        currGrid.SetActive(true);
    }

    public void ResetCubeData()
    {
        Debug.Log($"AloneModeQuestCtrl ::: list.Count = {list.Count}");

        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(true);
        }
    }

    void SetCardBoard(ButtonManager03 buttonManager, GameObject playSceneCanvas)
    {
        // Card가 없는 경우
        if (cardCtrl == null)
        {
            cardboard = Instantiate(cardboardPrefab, playSceneCanvas.transform);
            buttonManager.cardboard = cardboard;
            cardCtrl = cardboard.GetComponent<CardCtrl>();
        }
        else
        {
            cardboard.SetActive(true);
        }

        cardCtrl.SetCardData();
        GetAnswerData();
    }

    void GetAnswerData()
    {
        gridSize = cardCtrl.gridSize;
        front = cardCtrl.front;
        side = cardCtrl.side;
        top = cardCtrl.top;
    }

    public void ResetAloneModeQuestData()
    {
        if (cardboard != null)
        {
            cardboard.SetActive(false);
        }
    }
}
