using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AloneModeQuestCtrl : MonoBehaviour
{
    [Header("Cardboard Info")]
    public GameObject cardboardPrefab;
    private GameObject cardboard;
    private CardCtrl cardCtrl;

    private GameObject gameboard;
    private GameObject[] gridGroup;

    public void SetCurrGameboard(GameObject currGameboard)
    {
        if (gameboard == null)
        {
            gameboard = currGameboard;
            gridGroup = gameboard.GetComponent<GameboardCtrl>().gridGroup;
            Debug.Log($"AloneModeQuestCtrl ::: gridGroup.Length = {gridGroup.Length}");
        }
    }

    public void SetAloneModeQuest(ButtonManager03 buttonManager, GameObject playSceneCanvas)
    {
        ModeType modeType = GameManager.Instance.modeType;
        Debug.Log($"AloneModeQuestCtrl ::: modeType = {modeType}");

        if (modeType == ModeType.Alone_Count)
        {
            Debug.Log("AloneModeQuestCtrl ::: 큐브 보고 개수 맞추기");

            // Gameboard에서 Grid 가지고 오기
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
