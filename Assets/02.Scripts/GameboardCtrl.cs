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

    public void SetCubes(int _value)
    {
        if (list.Count == 0)
        {
            int gridCount = currGrid.transform.childCount;

            for (int i = 0; i < gridCount; i++)
            {
                for (int j = 0; j < _value; j++)
                {
                    GameObject obj = Instantiate(cubePrefab, cubeList.transform);
                    obj.transform.position = currGrid.transform.GetChild(i).position;
                    obj.transform.rotation = currGrid.transform.GetChild(i).rotation;
                    obj.transform.localScale = currGrid.transform.localScale;

                    list.Add(obj);
                }
            }
        }
        else
        {
            ResetCubeData();
        }
    }

    public void ResetCubeData()
    {
        Debug.Log($"GameboardCtrl ::: list.Count = {list.Count}");

        for (int i = 0; i < list.Count; i++)
        {
            list[i].SetActive(true);
        }
    }
}
