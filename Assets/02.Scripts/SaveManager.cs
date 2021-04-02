using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;

public class SaveManager : MonoBehaviour
{
    #region Singleton Pattern

    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static SaveManager _instance;

    // 인스턴스에 접근하기 위한 프로퍼티
    public static SaveManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(SaveManager)) as SaveManager;

                if (_instance == null)
                    Debug.Log("SaveManager ::: no Singleton obj");
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

    //----------------------------------------------------------------------------

    private static readonly string privateKey = "1718hy9dsf0jsdlfjds0pa9ids78ahgf81h32re";

    [Header("User Info")]
    public static string username;
    public static int profileImageNum = 0;

    [Header("Achievement Info")]
    public static List<bool> achievementInfo = new List<bool>();
    public static List<int> currAchievementState = new List<int>();

    [Header("AloneMode Info")]
    public static List<int> aloneMode01 = new List<int>();
    public static List<int> aloneMode02 = new List<int>();
    public static List<int> aloneMode03 = new List<int>();

    [Header("Sound Info")]
    public static float bgmVolume = 0.5f;
    public static float effectSoundVolume = 0.5f;
    public static bool isBGMMute = false;
    public static bool isEffectSoundMute = false;

    public static void Save()
    {
        // 플레이어 정보
        username = GameManager.Instance.username;
        profileImageNum = GameManager.Instance.profileImageNum;

        // 업적 정보
        achievementInfo.Clear();
        for (int i = 0; i < AchievementManager.Instance.achievementInfo.Length; i++)
        {
            achievementInfo.Add(AchievementManager.Instance.achievementInfo[i]);
        }

        currAchievementState.Clear();
        for (int i = 0; i < AchievementManager.Instance.currAchievementState.Length; i++)
        {
            currAchievementState.Add(AchievementManager.Instance.currAchievementState[i]);
        }

        // 스테이지 클리어 정보
        // 유형 01
        aloneMode01.Clear();
        for (int i = 0; i < GameManager.Instance.alone_01.Count; i++)
        {
            aloneMode01.Add((int)GameManager.Instance.alone_01[i]);
        }

        // 유형 02
        aloneMode02.Clear();
        for (int i = 0; i < GameManager.Instance.alone_02.Count; i++)
        {
            aloneMode02.Add((int)GameManager.Instance.alone_02[i]);
        }

        // 유형 03
        aloneMode03.Clear();
        for (int i = 0; i < GameManager.Instance.alone_03.Count; i++)
        {
            aloneMode03.Add((int)GameManager.Instance.alone_03[i]);
        }

        // 사운드 정보
        bgmVolume = SoundManager.Instance.BGMVolume;
        effectSoundVolume = SoundManager.Instance.effectVolume;
        isBGMMute = SoundManager.Instance.muteBGM;
        isEffectSoundMute = SoundManager.Instance.muteEffectSound;

        // 저장할 형태로 전환
        SaveData saveData = new SaveData(username, profileImageNum
                                        , achievementInfo, currAchievementState
                                        , aloneMode01, aloneMode02, aloneMode03
                                        , bgmVolume, effectSoundVolume, isBGMMute, isEffectSoundMute);

        string jsonString = DataToJson(saveData);
        string encrypString = Encrypt(jsonString);
        SaveFile(encrypString);

        //PlayFab 서버에 데이터 저장

        Debug.Log("SaveManager ::: 저장 완료");
    }

    public static SaveData Load()
    {
        ////인터넷이 연결됐는지 체크
        //PlayFabManager.Instance.GetData();

        //파일이 존재하는지부터 체크
        if (!File.Exists(GetPath()))
        {
            Debug.Log("SaveManager ::: Save 파일이 존재하지 않음");

            // 저장된 데이터가 없다면 기본 정보 생성
            GameManager.Instance.SetDefaultGameData();

            return null;
        }

        string encryptData = LoadFile(GetPath());
        string decryptData = Decrypt(encryptData);

        Debug.Log(decryptData);

        SaveData sd = JsonToData(decryptData);

        //Debug.Log(encryptData);
        //SaveData sd = JsonToData(encryptData);

        // 플레이어 정보 업데이트
        GameManager.Instance.username = sd.username;
        GameManager.Instance.profileImageNum = sd.profileImageNum;

        // 업적 정보 업데이트
        AchievementManager.Instance.achievementInfo = sd.achievementInfo.ToArray();
        AchievementManager.Instance.currAchievementState = sd.currAchievementState.ToArray();

        Debug.Log($"SaveManager ::: {sd.aloneMode01.Count} // {sd.aloneMode02.Count} //{sd.aloneMode03.Count}");
        Debug.Log($"{GameManager.Instance.alone_01.Count} // {GameManager.Instance.alone_02.Count} // {GameManager.Instance.alone_03.Count}");
        // 혼자하기 모드 정보
        for (int i = 0; i < sd.aloneMode01.Count; i++)
        {
            GameManager.Instance.alone_01.Add((AloneModeStageState)sd.aloneMode01[i]);
        }

        for (int i = 0; i < sd.aloneMode02.Count; i++)
        {
            GameManager.Instance.alone_02.Add((AloneModeStageState)sd.aloneMode02[i]);
        }

        for (int i = 0; i < sd.aloneMode03.Count; i++)
        {
            GameManager.Instance.alone_03.Add((AloneModeStageState)sd.aloneMode03[i]);
        }

        // 사운드 정보
        SoundManager.Instance.BGMVolume = sd.bgmVolume;
        SoundManager.Instance.effectVolume = sd.effectSoundVolume;
        SoundManager.Instance.muteBGM = sd.muteBGM;
        SoundManager.Instance.muteEffectSound = sd.muteEffectSound;


        Debug.Log("SaveManager ::: 불러오기 완료");

        return sd;
    }

    //----------------------------------------------------------------------------

    //Save 데이터를 json string으로 변환
    static string DataToJson(SaveData sd)
    {
        string jsonData = JsonUtility.ToJson(sd);
        return jsonData;
    }

    //json string을 SaveData로 변환
    static SaveData JsonToData(string jsonData)
    {
        SaveData sd = JsonUtility.FromJson<SaveData>(jsonData);
        return sd;
    }

    //----------------------------------------------------------------------------

    //json string을 파일로 저장
    static void SaveFile(string jsonData)
    {
        using (FileStream fs = new FileStream(GetPath(), FileMode.Create, FileAccess.Write))
        {
            //파일로 저장할 수 있게 바이트화
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

            //bytes의 내용물을 0 ~ max 길이까지 fs에 복사
            fs.Write(bytes, 0, bytes.Length);
        }
    }

    //파일 불러오기
    static string LoadFile(string path)
    {
        using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            //파일을 바이트화 했을 때 담을 변수를 제작
            byte[] bytes = new byte[(int)fs.Length];

            //파일스트림으로 부터 바이트 추출
            fs.Read(bytes, 0, (int)fs.Length);

            //추출한 바이트를 json string으로 인코딩
            string jsonString = System.Text.Encoding.UTF8.GetString(bytes);
            return jsonString;
        }
    }

    //----------------------------------------------------------------------------

    //암호화를 시켜주는 function
    private static string Encrypt(string data)
    {
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
        RijndaelManaged rm = CreateRijndaelManaged();
        ICryptoTransform ct = rm.CreateEncryptor();
        byte[] results = ct.TransformFinalBlock(bytes, 0, bytes.Length);
        return System.Convert.ToBase64String(results, 0, results.Length);
    }

    //암호화를 풀어주는 function
    private static string Decrypt(string data)
    {
        byte[] bytes = System.Convert.FromBase64String(data);
        RijndaelManaged rm = CreateRijndaelManaged();
        ICryptoTransform ct = rm.CreateDecryptor();
        byte[] resultArray = ct.TransformFinalBlock(bytes, 0, bytes.Length);
        return System.Text.Encoding.UTF8.GetString(resultArray);
    }

    //암호화와 관련된 것들을 총괄해주는 class
    private static RijndaelManaged CreateRijndaelManaged()
    {
        byte[] keyArray = System.Text.Encoding.UTF8.GetBytes(privateKey);
        RijndaelManaged result = new RijndaelManaged();

        byte[] newKeysArray = new byte[16];
        System.Array.Copy(keyArray, 0, newKeysArray, 0, 16);

        result.Key = newKeysArray;                  //암호화와 해석에 필요한 키. 외부로 유출되면 안됨.
        result.Mode = CipherMode.ECB;               //암호화 방식. 여기서 ECB는 key를 이용한 가장 간단한 암호화 방식임
        result.Padding = PaddingMode.PKCS7;         //데이터가 전체 암호화에 필요한 byte보다 짧을 대 남은 byte를 채워주는 방식을 설정
        return result;
    }

    //----------------------------------------------------------------------------

    //저장할 주소를 반환
    static string GetPath()
    {
        //Application.persistentDataPath 위치 (Windows 10 기준)
        //C:\Users\(사용자이름)\AppData\LocalLow\(회사이름)\(Unity Project 이름)

        return Path.Combine(Application.persistentDataPath, "Data.dll");
    }
}
