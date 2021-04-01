using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCtrl : MonoBehaviour
{
    [Header("Card Data")]
    public CardData frontCard;
    public CardData sideCard;
    public CardData topCard;

    [HideInInspector] public int gridSize;
    [HideInInspector] public string front;
    [HideInInspector] public string side;
    [HideInInspector] public string top;

    public void SetCardData()
    {
        // 문제 정보 가져오기
        QuestManager.Instance.SetCurrQuest();

        // 현재 단계 확인하기
        int stageID = GameManager.Instance.stageID - 1;

        gridSize = QuestManager.Instance.currQuest[stageID].GetGridSize();
        front = QuestManager.Instance.currQuest[stageID].GetFrontInfo();
        side = QuestManager.Instance.currQuest[stageID].GetSideInfo();
        top = QuestManager.Instance.currQuest[stageID].GetTopInfo();

        frontCard.SetCardData(gridSize, front);
        sideCard.SetCardData(gridSize, side);
        topCard.SetCardData(gridSize, top);
    }
}
