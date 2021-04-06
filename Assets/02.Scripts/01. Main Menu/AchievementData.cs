using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementData : MonoBehaviour
{
    public float size = 380;
    public Image[] achievementImages;
    private List<Vector2> imageSizeList = new List<Vector2>();

    // 프로필 팝업창 오픈 시 업적 정보를 확인
    public void UpdateAchievementStatus()
    {
        // 크기 저장
        if (imageSizeList.Count == 0)
        {
            for (int i = 0; i < achievementImages.Length; i++)
            {
                Vector2 imageSize = achievementImages[i].rectTransform.sizeDelta;
                imageSizeList.Add(imageSize);
            }
        }

        for (int i = 0; i < achievementImages.Length; i++)
        {
            if (AchievementManager.Instance.achievementInfo[i] == true)
            {
                achievementImages[i].sprite = AchievementManager.Instance.achievementSprites[i];
                achievementImages[i].rectTransform.sizeDelta = imageSizeList[i];
            }
            else
            {
                achievementImages[i].sprite = AchievementManager.Instance.achievementLockSprite;
                achievementImages[i].rectTransform.sizeDelta = new Vector2(size, size);
            }
        }
    }
}
