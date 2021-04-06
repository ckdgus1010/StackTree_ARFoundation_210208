using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ModeType
{
    None, Create, Alone_Count, Alone_Minus, Alone_Plus, Together, Together_Two, Together_Three
}

public enum AloneModeStageState
{
    Closed, Current, Cleared
}

public class GameManager : MonoBehaviour
{
    #region Singleton Pattern

    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static GameManager _instance;

    // 인스턴스에 접근하기 위한 프로퍼티
    public static GameManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null)
                    Debug.Log("GameManager ::: no Singleton obj");
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

    [Header("User Information")]
    public string username;
    public int profileImageNum = 0;
    public Sprite[] profileImages;

    [Header("Stage Information")]
    public ModeType modeType = ModeType.None;
    public int stageID = 0;
    public int stageCount = 50;

    [Header("혼자하기 모드별 단계 관리")]
    public int currentStageID = 0;
    public List<AloneModeStageState> currStageStateArray = new List<AloneModeStageState>();
    public List<AloneModeStageState> alone_01 = new List<AloneModeStageState>();
    public List<AloneModeStageState> alone_02 = new List<AloneModeStageState>();
    public List<AloneModeStageState> alone_03 = new List<AloneModeStageState>();

    void Start()
    {
        SaveManager.Load();
        //SetDefaultGameData();
    }

    public void SetDefaultGameData()
    {
        SetDefaultPlayerData();
        SetDefaultAloneModeStatus();
        SaveManager.Save();
    }

    void SetDefaultPlayerData()
    {
        // User Name 설정
        int num = Random.Range(0, 10000);
        username = "Guset" + num.ToString();

        // 프로필 이미지 설정
        profileImageNum = 0;

        modeType = ModeType.None;
        stageID = 0;
    }

    // 기본 혼자하기 모드 데이터 설정
    void SetDefaultAloneModeStatus()
    {
        for (int i = 0; i < stageCount; i++)
        {
            AloneModeStageState state = i == 0 ? AloneModeStageState.Current : AloneModeStageState.Closed;

            alone_01.Add(state);
            alone_02.Add(state);
            alone_03.Add(state);
        }


    }
}
