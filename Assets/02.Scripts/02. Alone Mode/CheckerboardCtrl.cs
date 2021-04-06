using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerboardCtrl : MonoBehaviour
{
    public GameObject[] gridGroup;
    private GameObject currGrids;
    public List<string> playerAnswers = new List<string>();
    private string playerAnswer;

    // checkerboard 설정
    public void SetCheckerboard(int gridSize)
    {
        if (gridSize > 5)
        {
            Debug.LogError($"CheckerboardCtrl ::: Grid의 크기는 5 이하여야 함 \n gridSize = {gridSize}");
            return;
        }

        // 전부 끄기
        for (int i = 0; i < gridGroup.Length; i++)
        {
            gridGroup[i].SetActive(false);
        }

        // 알맞은 크기의 checkerboard 설정 및 켜기
        int num = gridSize - 3;
        gridGroup[num].SetActive(true);
        currGrids = gridGroup[num];
    }

    // 정답 확인
    public void GetPlayerAnswerData(int gridSize)
    {
        // 기존 플레이어의 답안 초기화
        playerAnswers.Clear();
        Debug.Log($"CheckerboardCtrl ::: playerAnswers.Count = {playerAnswers.Count}");

        int childCount = currGrids.transform.childCount;
        Debug.Log($"CheckerboardCtrl ::: childCount = {childCount}");

        CheckerboardData data = currGrids.GetComponent<CheckerboardData>();

        for (int i= 0; i < childCount; i++)
        {
            string _playerAnswer = data.GetPlayerAnswer(i);
            Debug.Log($"CheckerboardCtrl ::: {i} // {_playerAnswer}");
            playerAnswers.Add(_playerAnswer);
        }
    }
}
