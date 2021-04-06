using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameboardCtrl : MonoBehaviour
{
    public GameObject[] gridGroup;
    public GameObject cubePrefab;

    private GameObject cubeList;
    private GameObject currGrid;
    private int value = 0;
    private List<GameObject> list = new List<GameObject>();

    void Start()
    {
        cubeList = transform.GetChild(2).gameObject;
    }

    // Gameboard 내 알맞은 Grid 켜기
    public void SetGameboardGrid(int _value)
    {
        if (GameManager.Instance.modeType == ModeType.Create)
        {
            value = _value - 3;
        }
        else
        {
            int stageID = GameManager.Instance.stageID - 1;
            value = QuestManager.Instance.currQuest[stageID].GetGridSize() - 3;
        }

        if (currGrid != null)
        {
            currGrid.SetActive(false);
        }

        currGrid = gridGroup[value];
        currGrid.SetActive(true);
    }

    public Vector3 GetCubeScale()
    {
        float size = currGrid.transform.GetChild(0).transform.localScale.x;
        Vector3 scale = Vector3.one * size;
        
        return scale;
    }
}
