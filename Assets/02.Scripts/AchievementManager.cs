using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //public bool isFirstLogin = false;           // 최초 로그인 확인
    public int clearCount = 0;                  // 혼자하기 모드 클리어된 단계 개수
    public int masterCount = 0;                 // 같이하기 모드에서 방장을 한 횟수

    public int screenshotCount = 0;             // 스크린샷 찍은 횟수
    //public bool isClickCreateMode = false;      // Create Mode 최초 실행 확인
    public float playTimer = 0.0f;              // 혼자하기 모드 문제 한 개 풀이 시간

    //public bool isFirstFail = false;            // 최초 오답 확인
    //public bool isFirstCreditRun = false;       // 크레딧 최초 실행 확인
    public int starCount = 0;                   // 혼자하기 모드에서 수집한 별 개수


    [Header("업적 달성 기준")]
    //private bool isFirstLoginStd = true;
    private int clearCountStd = 27;
    private int masterCountStd = 5;

    private int screenshotCountStd = 5;
    //private bool isClickCreateModeStd = true;
    private float playTimerStd = 300.0f;

    //private bool isFirstFailStd = true;
    //private bool isFirstCreditRun = true;
    private int starCountStd = 81;

    //--------------------------------------------------------------

    // 업적 01 ::: 반가워요!! - 첫 로그인 시 획득
    public void GetAchievement01()
    {
        if (achievement[0] == false)
        {
            Debug.Log("AchievementManager ::: 업적 01 클리어");
            achievement[0] = true;

            SaveAchievementData(0);
        }
    }

    // 업적 02 ::: 당신은 고인물?!?! - 혼자하기 모드 모두 클리어 시 획득
    public void GetAchievement02()
    {
        if (achievement[1] == false)
        {
            clearCount += 1;

            if (clearCount >= clearCountStd)
            {
                Debug.Log("AchievementManager ::: 업적 02 클리어");
                achievement[1] = true;

                SaveAchievementData(1);
            }
        }
    }

    // 업적 03 ::: 리더십!! - 같이하기 모드에서 '방장' 5번 수행 시 획득
    public void GetAchievement03()
    {
        if (achievement[2] == false)
        {
            masterCount += 1;

            if (masterCount >= masterCountStd)
            {
                Debug.Log("AchievementManager ::: 업적 03 클리어");
                achievement[2] = true;

                SaveAchievementData(2);
            }
        }
    }

    // 업적 04 ::: 찍사!! - 스크린샷 5회 시 획득
    public void GetAchievement04()
    {
        if (achievement[3] == false)
        {
            screenshotCount += 1;

            if (screenshotCount == screenshotCountStd)
            {
                Debug.Log("AchievementManager ::: 업적 04 클리어");
                achievement[3] = true;

                SaveAchievementData(3);
            }
        }
    }

    // 업적 05 ::: 당신도 Creator - Create Mode 최초 1회 실행 시 획득
    public void GetAchievement05()
    {
        if (achievement[4] == false)
        {
            Debug.Log("AchievementManager ::: 업적 05 클리어");
            achievement[4] = true;

            SaveAchievementData(4);
        }
    }

    // 업적 06 ::: 애송이 - 혼자하기 모드 5분 이상 실행 시 획득
    public void GetAchievement06()
    {
        if (playTimer >= playTimerStd)
        {
            Debug.Log("AchievementManager ::: 업적 06 클리어");
            achievement[5] = true;

            SaveAchievementData(5);
        }
    }

    // 업적 07 ::: 실패는 성공의 어머니 - 최초 오답 시 획득
    public void GetAchievement07()
    {
        if (achievement[6] == false)
        {
            Debug.Log("AchievementManager ::: 업적 07 클리어");
            achievement[6] = true;

            SaveAchievementData(6);
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
            starCount += 1;

            if (starCount >= starCountStd)
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
}
