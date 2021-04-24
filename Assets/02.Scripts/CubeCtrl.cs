using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCtrl : MonoBehaviour
{
    public GameObject cubePrefab;
    public List<GameObject> list = new List<GameObject>();

    // Gameboard 정보
    private GameObject gameboard;
    private GameObject cubeList;
    private GameboardCtrl gameboardCtrl;

    // Cube 생성 위치 정보
    [HideInInspector] public Transform cubeTr;

    public void SetGameboardData(GameObject _gameboard)
    {
        gameboard = _gameboard;
        cubeList = gameboard.transform.GetChild(2).gameObject;
        gameboardCtrl = gameboard.GetComponent<GameboardCtrl>();
    }    

    public void PlusCube(GameObject guideCube)
    {
        GameObject obj = Instantiate(cubePrefab
                                    , guideCube.transform.position
                                    , gameboard.transform.rotation
                                    , cubeList.transform);
        //obj.transform.localScale = gameboardCtrl.GetCubeScale();
        obj.transform.localScale = guideCube.transform.localScale;

        list.Add(obj);
    }

    public void MinusCube(GameObject obj)
    {
        Destroy(obj);
        Debug.Log("CubeCtrl ::: 큐브 빼기");
    }

    public void ResetCubeData()
    {
        if (list.Count == 0)
        {
            return;
        }

        Debug.Log("CubeCtrl ::: 큐브 리셋 시작");

        if (GameManager.Instance.modeType == ModeType.Create || GameManager.Instance.modeType == ModeType.Alone_Plus)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Destroy(list[i].gameObject);
            }

            list.Clear();
        }

        Debug.Log("CubeCtrl ::: 큐브 리셋 끝");
    }
}
