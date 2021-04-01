using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerboardData : MonoBehaviour
{
    [SerializeField]
    private float rayDistance = 1;

    [Header("Checkerboard")]
    public GameObject[] checkers;
    private List<GameObject> currGrid = new List<GameObject>();

    private string[] playerAnswerArray = new string[3];
    private int cubeCount = 0;

    public string GetPlayerAnswer(int num)
    {
        cubeCount = 0;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        int childCount = checkers[num].transform.childCount;
        Vector3 dir = checkers[num].transform.forward;
        for (int i = 0; i < childCount; i++)
        {
            GameObject obj = checkers[num].transform.GetChild(i).gameObject;
            Ray ray = new Ray(obj.transform.position, dir * rayDistance);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance, 1 << 8))
            {
                if (hitInfo.collider.CompareTag("CUBE"))
                {
                    cubeCount = 1;
                    Debug.Log($"CheckboardData ::: {num} // {i} 큐브 있음");
                }
                else
                {
                    cubeCount = 0;
                    Debug.Log($"CheckboardData ::: {num} // {i} 큐브 없음");
                }
            }
            else
            {
                cubeCount = 0;
                Debug.Log($"CheckboardData ::: {num} // {i} 아무 것도 없음");
            }

            sb.Append(cubeCount.ToString());
        }
        string playerAnswer = sb.ToString();

        return playerAnswer;
    }
}
