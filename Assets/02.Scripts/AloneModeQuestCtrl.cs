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

        // 혼자하기 모드 - 유형 01
        if (modeType == ModeType.Alone_Count)
        {
            // 문제 정보 받아오기
            QuestManager.Instance.SetCurrQuest();
            int stageID = GameManager.Instance.stageID - 1;
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
                GameObject gridCell = currGrid.transform.GetChild(i).gameObject;

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
        // 혼자하기 모드 - 유형 02, 03
        else if (modeType == ModeType.Alone_Minus || modeType == ModeType.Alone_Plus)
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
