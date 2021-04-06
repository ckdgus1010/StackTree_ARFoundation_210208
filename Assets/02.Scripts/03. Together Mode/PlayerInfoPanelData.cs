using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoPanelData : MonoBehaviour
{
    public Image playerImage;
    public Text playerName;
    public Image mineMark;
    public Button readyButton;

    public void SetPanelData(bool isMasterClient, bool isMine, string userName, Sprite masterSprite, Sprite clientSprite, ButtonManager04 buttonManager)
    {
        Debug.Log($"SetPanelData ::: {isMasterClient} // {isMine}");
        // 플레이어 이름 설정
        playerName.text = userName;

        if (isMasterClient == true)
        {
            playerImage.sprite = masterSprite;
            playerImage.rectTransform.sizeDelta = new Vector2 (masterSprite.rect.width * 2
                                                              , masterSprite.rect.height * 2);
            readyButton.transform.GetChild(0).GetComponent<Text>().text = "방장";
        }
        else
        {
            playerImage.sprite = clientSprite;
            readyButton.onClick.AddListener(() => buttonManager.SendReadyCount(readyButton.image));
        }

        // mineMark 설정
        mineMark.enabled = isMine;
    }
}
