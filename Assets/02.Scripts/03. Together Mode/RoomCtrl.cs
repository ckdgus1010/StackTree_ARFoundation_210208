using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public enum GameLevel
{
    Easy, Normal, Hard, None
}

public enum GameTheme
{
    Beach, GlobePoint, Zombie, Chaos, Snow
}

public class RoomCtrl : MonoBehaviour
{
    public PhotonManager photonManager;
    public ButtonManager04 buttonManager;

    [Header("Room Info")]
    public Text roomNameText;
    public Text playButtonText;

    [Header("Player List")]
    public Transform playerList;
    public GameObject playerInfoPanelPrefab;
    public Sprite masterSprite;
    public Sprite clientSprite;
    private List<GameObject> list = new List<GameObject>();

    [Header("플레이어들의 준비 상태")]
    public int playerReadyCount = 0;
    public Color buttonColor;
    private bool isPlayerReady = false;

    [Header("Game Level")]
    [SerializeField]
    private int gameLevelNum = 0;
    public Sprite defaultStateImage;
    public Sprite selectedStateImage;
    public Image[] gameLevelList = new Image[3];

    [Header("Map Theme")]
    public Text mapNameText;
    private int gameThemeNum = 0;
    [SerializeField]
    private string[] mapNameList = new string[5] { "반짝반짝 모래마을", "글포마을"
                                                  , "좀비성", "혼돈의 카오스"
                                                  , "올라프의 성" };

    void Start()
    {
        mapNameText.text = mapNameList[0];
        ClickLevelControlButton(0);
    }

    // [시작 하기 버튼] 클릭 시 Photon Manager의 게임 정보 업데이트
    public void SetPlayData()
    {
        photonManager.playerReadyState = playerReadyCount;
        photonManager.gameLevel = (GameLevel)gameLevelNum;
        photonManager.gameTheme = (GameTheme)gameThemeNum;
    }

    // 방에 입장했을 때
    public void SetRoomInfo(string roomName, int playerCount)
    {
        // 방 이름 설정
        roomNameText.text = roomName;

        // 플레이어 리스트 최신화
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Destroy(list[i]);
            }
            list.Clear();
        }

        for (int i = 0; i < playerCount; i++)
        {
            // Player Info Panel 생성
            GameObject obj = Instantiate(playerInfoPanelPrefab, playerList);
            list.Add(obj);

            bool isMasterClient = PhotonNetwork.PlayerList[i].IsMasterClient;
            bool isMine = PhotonNetwork.PlayerList[i].NickName == GameManager.Instance.username ? true : false;
            string userName = PhotonNetwork.PlayerList[i].NickName;

            // Player Info Panel에 있는 UI 기능 업데이트
            PlayerInfoPanelData panelData = obj.GetComponent<PlayerInfoPanelData>();
            panelData.SetPanelData(isMasterClient
                                  , isMine
                                  , userName
                                  , masterSprite
                                  , clientSprite
                                  , buttonManager);
        }

        // 플레이 버튼 이름 설정
        playButtonText.text = PhotonNetwork.IsMasterClient == true ? "게임 시작!!" : "준비!!";
    }

    // 준비 상태 설정(본인)
    public bool ClickReadyButton(Image image)
    {
        if (buttonColor == null)
        {
            Debug.Log("RoomCtrl ::: buttonColor is null");
        }

        isPlayerReady = !isPlayerReady;
        image.color = isPlayerReady == true ? buttonColor : Color.white;

        return isPlayerReady;
    }

    // 준비 상태인 플레이어 확인
    public int CheckReadyPlayer(Image image)
    {
        int playerNumber = 0;
        GameObject obj = image.transform.parent.gameObject;
        for (int i = 0; i < list.Count; i++)
        {
            if (obj == list[i])
            {
                playerNumber = i;
                break;
            }
        }

        return playerNumber;
    }

    // 플레이어 준비 상태 업데이트(다른 플레이어들)
    public void ChangeReadyButtonColor(bool isPlayerReady, int playerNumber)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (i == playerNumber)
            {
                list[i].transform.GetChild(3).GetComponent<Image>().color = isPlayerReady == true ? buttonColor : Color.white;
            }
        }
    }

    // 게임 난이도 조절
    public void ClickLevelControlButton(int value)
    {
        gameLevelNum = value;

        for (int i = 0; i < gameLevelList.Length; i++)
        {
            gameLevelList[i].sprite = i == gameLevelNum ? selectedStateImage : defaultStateImage;
        }
    }

    // 게임 맵 테마 조절
    public void ClickMapNameControlButton(int value)
    {
        gameThemeNum += value;

        if (gameThemeNum < 0)
        {
            gameThemeNum = mapNameList.Length - 1;
        }
        else if (gameThemeNum > mapNameList.Length - 1)
        {
            gameThemeNum = 0;
        }

        mapNameText.text = mapNameList[gameThemeNum];
    }
}
