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

    public void SetCurrGameboard(GameObject currGameboard)
    {
        if (gameboard == null)
        {
            gameboard = currGameboard;
            cubeList = gameboard.transform.GetChild(2).gameObject;
            gridGroup = gameboard.GetComponent<GameboardCtrl>().gridGroup;
            Debug.Log($"AloneModeQuestCtrl ::: gridGroup.Length = {gridGroup.Length}");
        }
    }

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
        Debug.Log($"AloneModeQuestCtrl ::: modeType = {modeType}");

        if (modeType == ModeType.Alone_Count)
        {
            Debug.Log("AloneModeQuestCtrl ::: 큐브 보고 개수 맞추기");

            // 문제 정보 받아오기
            QuestManager.Instance.SetCurrQuest();
            int stageID = GameManager.Instance.stageID;
            int gridSize = QuestManager.Instance.currQuest[stageID].GetGridSize();
            int gridCount = gridSize * gridSize;
            string top = QuestManager.Instance.currQuest[stageID].GetTopInfo();

            // Gameboard에서 Grid 가지고 오기
            if (currGrid != null)
            {
                currGrid.SetActive(false);
                currGrid = null;
            }
            currGrid = gridGroup[gridSize - 3];
            currGrid.SetActive(true);

            // 문제에 맞춰 CubePrefab 생성
            for (int i = 0; i < gridCount; i++)
            {
                int cubeCount = int.Parse(top.Substring(i, 1));
                Debug.Log($"AloneModeQuestCtrl ::: cubeCount = {cubeCount}");

                if (cubeCount != 0)
                {
                    for (int j = 0; j < cubeCount; j++)
                    {
                        GameObject obj = Instantiate(cubePrefab
                                                    , currGrid.transform.GetChild(j).transform.position
                                                    , gameboard.transform.rotation
                                                    , cubeList.transform);
                        obj.transform.localScale = gameboard.GetComponent<GameboardCtrl>().GetCubeScale();

                        list.Add(obj);
                    }
                }
            }
        }
        else if (modeType == ModeType.Alone_Minus || modeType == ModeType.Alone_Plus)
        {
            // Card가 없는 경우
            if (cardCtrl == null)
            {
                cardboard = Instantiate(cardboardPrefab, playSceneCanvas.transform);
                buttonManager.cardboard = cardboard;
                cardCtrl = cardboard.GetComponent<CardCtrl>();
                cardCtrl.SetCardData();
            }
            else
            {
                cardboard.SetActive(true);
            }
        }
    }

    public void ResetAloneModeQuestData()
    {
        if (cardboard != null)
        {
            cardboard.SetActive(false);
        }
    }
}
