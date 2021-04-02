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
    public bool[] achievementInfo = new bool[9] { false, false, false
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

    [HideInInspector]
    public int[] currAchievementState;
    private int[] stdArray;

    [Header("업적 달성 이미지")]
    public Sprite achievementLockSprite;
    public Sprite[] achievementSprites;

    //--------------------------------------------------------------

    void Start()
    {
        currAchievementState = new int[] { loginCount, achievementCount, masterCount
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

        if (achievementInfo[num] == false)
        {
            currAchievementState[num] += 1;
            Debug.Log($"AchievementManager ::: {num} // {currAchievementState[num]}");

            if (currAchievementState[num] >= stdArray[num])
            {
                achievementInfo[num] = true;
                SaveAchievementData(num);

                Debug.Log($"AchievementManager ::: 업적 0{num + 1} 클리어");
            }
        }
    }

    //--------------------------------------------------------------

    // 클리어한 정보 저장하기
    void SaveAchievementData(int order)
    {
        SaveManager.achievementInfo[order] = true;
        SaveManager.Save();

        Debug.Log($"AchievementManager ::: 업적 0{order + 1} 클리어 정보 저장");
    }

    // 업적 정보 리셋
    public void ResetAchievementData()
    {
        for (int i = 0; i < currAchievementState.Length; i++)
        {
            currAchievementState[i] = 0;
        }
    }
}
