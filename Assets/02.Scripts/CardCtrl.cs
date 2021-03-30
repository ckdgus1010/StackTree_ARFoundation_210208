using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCtrl : MonoBehaviour
{
    [Header("Card Data")]
    public CardData frontCard;
    public CardData sideCard;
    public CardData topCard;

    public void SetCardData()
    {
        // 문제 정보 가져오기
        QuestManager.Instance.SetCurrQuest();

        // 현재 단계 확인하기
        int stageID = GameManager.Instance.stageID - 1;

        int gridSize = QuestManager.Instance.currQuest[stageID].GetGridSize();
        string front = QuestManager.Instance.currQuest[stageID].GetFrontInfo();
        string side = QuestManager.Instance.currQuest[stageID].GetSideInfo();
        string top = QuestManager.Instance.currQuest[stageID].GetTopInfo();

        frontCard.SetCardData(gridSize, front);
        sideCard.SetCardData(gridSize, side);
        topCard.SetCardData(gridSize, top);
    }
}
