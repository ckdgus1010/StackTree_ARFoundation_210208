using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementData : MonoBehaviour
{
    public int achievementID = 0;
    public GameObject achievementImage;
    public GameObject questionImage;

    public AchievementData[] achievementArray = new AchievementData[9];

    // 프로필 팝업창 오픈 시 업적 정보를 확인
    public void UpdateAchievementStatus()
    {
        // Achievement Scroll View에서만 사용할 것
        if (achievementID == 1000)
        {
            for (int i = 0; i < achievementArray.Length; i++)
            {
                achievementArray[i].CheckAchievementStatus();
            }
        }
    }

    public void CheckAchievementStatus()
    {
        // AchievementManager 업적 달성 여부 확인
        bool isAchieved = AchievementManager.Instance.achievement[achievementID - 1];

        achievementImage.SetActive(isAchieved);
        questionImage.SetActive(!isAchieved);
    }
}
