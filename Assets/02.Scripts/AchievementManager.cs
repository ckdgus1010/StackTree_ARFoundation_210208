using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementState
{
    Login_Count, Achievement_Count, Master_Count
    , Screenshot_Count, CreateMode_Count, overPlayTime_Count
    , Fail_Count, CreditRun_Count, AloneModeClear_Count
}

public class AchievementManager : MonoBehaviour
{
    #region Singleton Pattern

    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static AchievementManager _instance;

    // 인스턴스에 접근하기 위한 프로퍼티
    public static AchievementManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(AchievementManager)) as AchievementManager;

                if (_instance == null)
                    Debug.Log("AchievementManager ::: no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    //--------------------------------------------------------------

    [Header("업적")]
    public bool[] achievement = new bool[9] { false, false, false
                                            , false, false, false
                                            , false, false, false };


    [Header("업적 현황")]
    public int loginCount = 0;                  // 1. 로그인 횟수
    public int achievementCount = 0;            // 2. 달성한 업적 개수
    public int masterCount = 0;                 // 3. 같이하기 모드에서 방장을 한 횟수

    public int screenshotCount = 0;             // ()4. 스크린샷 찍은 횟수
    public int createModeCount = 0;             // ()5. Create Mode 실행 횟수
    public int overPlayTimeCount = 0;           // 6. 기준 시간동안 혼자하기 모드 문제를 못 푼 횟수

    public int failCount = 0;                   // ()7. 오답 횟수
    public int creditRunCount = 0;              // ()8. 크레딧 실행 횟수
    public int aloneModeClearCount = 0;         // ()9. 혼자하기 모드 클리어 한 단계 개수

    [Header("업적 달성 기준")]
    private int loginCountStd = 1;
    private int achievementCountStd = 9;
    private int masterCountStd = 5;

    private int screenshotCountStd = 5;
    private int createModeCountStd = 1;
    private int overPlayTimeCountStd = 0;

    private int failCountStd = 1;
    private int creditRunCountStd = 1;
    private int aloneModeClearCountStd = 150;

    private int[] countArray;
    private int[] stdArray;

    [Header("업적 달성 이미지")]
    public Sprite achievementLockSprite;
    public Sprite[] achievementSprites;

    //--------------------------------------------------------------

    void Start()
    {
        countArray = new int[] { loginCount, achievementCount, masterCount
                               , screenshotCount, createModeCount, overPlayTimeCount
                               , failCount, creditRunCount, aloneModeClearCount };

        stdArray = new int[] { loginCountStd, achievementCountStd ,masterCountStd
                             ,screenshotCountStd ,createModeCountStd ,overPlayTimeCountStd
                             ,failCountStd ,creditRunCountStd ,aloneModeClearCountStd };
    }

    public void AAAAAAA(int num)
    {
        AchievementState state = (AchievementState)num;
        UpdateAchievementData(state);
    }

    // 업적 데이터 최신화
    public void UpdateAchievementData(AchievementState state)
    {
        int num = (int)state;

        if (achievement[num] == false)
        {
            countArray[num] += 1;
            Debug.Log($"AchievementManager ::: {num} // {countArray[num]}");

            if (countArray[num] >= stdArray[num])
            {
                achievement[num] = true;
                SaveAchievementData(num);

                Debug.Log($"AchievementManager ::: 업적 0{num + 1} 클리어");
            }
        }
    }

    // 업적 01 ::: 반가워요!! - 첫 로그인 시 획득
    public void CheckLoginAchievement()
    {
        if (achievement[0] == false)
        {
            loginCount += 1;

            if (loginCount >= loginCountStd)
            {
                achievement[0] = true;
                SaveAchievementData(0);

                Debug.Log("AchievementManager ::: 업적 01 클리어");
            }
        }
    }

    // 업적 02 ::: 당신은 고인물?!?! - 모든 업적 달성 시 획득
    public void CheckAllAchievement()
    {
        if (achievement[1] == false)
        {
            //clearCount += 1;

            //if (clearCount >= achievementCountStd)
            //{
            //    Debug.Log("AchievementManager ::: 업적 02 클리어");
            //    achievement[1] = true;

            //    SaveAchievementData(1);
            //}
        }
    }

    // 업적 03 ::: 리더십!! - 같이하기 모드에서 '방장' 5번 수행 시 획득
    public void CheckLeaderShipAchievement()
    {
        if (achievement[2] == false)
        {
            masterCount += 1;

            if (masterCount >= masterCountStd)
            {
                achievement[2] = true;
                SaveAchievementData(2);

                Debug.Log("AchievementManager ::: 업적 03 클리어");
            }
        }
    }

    // 업적 04 ::: 찍사!! - 스크린샷 5회 시 획득
    public void CheckScreenshotCoutnAchievement()
    {
        if (achievement[3] == false)
        {
            screenshotCount += 1;

            if (screenshotCount >= screenshotCountStd)
            {
                achievement[3] = true;
                SaveAchievementData(3);

                Debug.Log("AchievementManager ::: 업적 04 클리어");
            }
        }
    }

    // 업적 05 ::: 당신도 Creator - Create Mode 최초 1회 실행 시 획득
    public void CheckCreateModeCountAchievement()
    {
        if (achievement[4] == false)
        {
            createModeCount += 1;

            if (createModeCount >= createModeCountStd)
            {
                achievement[4] = true;
                SaveAchievementData(4);

                Debug.Log("AchievementManager ::: 업적 05 클리어");
            }
        }
    }

    // 업적 06 ::: 애송이 - 혼자하기 모드 5분 이상 실행 시 획득
    public void CheckAloneModePlayTimeAchievement()
    {
        //if (playTimer >= playTimerStd)
        //{
        //    Debug.Log("AchievementManager ::: 업적 06 클리어");
        //    achievement[5] = true;

        //    SaveAchievementData(5);
        //}
    }

    // 업적 07 ::: 실패는 성공의 어머니 - 최초 오답 시 획득
    public void CheckFailCountAchievement()
    {
        if (achievement[6] == false)
        {
            failCount += 1;

            if (failCount >= failCountStd)
            {
                achievement[6] = true;
                SaveAchievementData(6);

                Debug.Log("AchievementManager ::: 업적 07 클리어");
            }
        }
    }

    // 업적 08 ::: 우리의 게임을 즐겨줘서 고마워요!! - 크레딧 최초 실행 시 획득
    public void GetAchievement08()
    {
        if (achievement[7] == false)
        {
            Debug.Log("AchievementManager ::: 업적 08 클리어");
            achievement[7] = true;

            SaveAchievementData(7);
        }
    }

    // 없애야 함?
    // 업적 09 ::: 혼자하기 마스터!! - 혼자하기 모드에서 별 81개 수집 시 획득
    public void GetAchievement09()
    {
        if (achievement[8] == false)
        {
            aloneModeClearCount += 1;

            if (aloneModeClearCount >= aloneModeClearCountStd)
            {
                Debug.Log("AchievementManager ::: 업적 09 클리어");
                achievement[8] = true;

                SaveAchievementData(8);
            }
        }
    }

    //--------------------------------------------------------------

    // 클리어한 정보 저장하기
    void SaveAchievementData(int order)
    {
        //Debug.Log($"AchievementManager ::: 업적 0{order + 1} 클리어 정보 저장");

        //SaveManager.achievement[order] = true;
        //SaveManager.Save();
    }

    // 업적 정보 리셋
    public void ResetAchievementData()
    {
        loginCount = 0;
        achievementCount = 0;
        masterCount = 0;

        screenshotCount = 0;
        createModeCount = 0;
        overPlayTimeCount = 0;

        failCount = 0;
        creditRunCount = 0;
        aloneModeClearCount = 0;
    }
}
