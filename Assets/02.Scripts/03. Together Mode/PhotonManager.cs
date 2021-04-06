using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [Header("Manager Scrtips")]
    public ButtonManager04 buttonManager;
    public CanvasManager02 canvasManager;
    public RoomCtrl roomCtrl;
    public TogetherPlayCtrl playCtrl;

    [Header("Game Info")]
    public int playerReadyState;
    public GameLevel gameLevel;
    public GameTheme gameTheme;
    private bool isGameDataReady = false;

    [Header("Room List Ctrl")]
    public GameObject listPanelPrefab;
    public GameObject roomListScrollView;
    public List<GameObject> roomPanelList = new List<GameObject>();

    // Room List Panel 왼쪽에 나타날 이미지
    public Sprite maxPlayerCount02;
    public Sprite maxPlayerCount03;

    [Header("New Room Info")]
    [HideInInspector] public string roomName;
    [HideInInspector] public byte maxPlayersPerRoom = 0;
    private byte _maxPlayersPerRoom = 0;

    void Start()
    {
        ConnectToPhotonServer();
    }

    // Photon 서버에 연결
    void ConnectToPhotonServer()
    {
        // 동기화 시작
        PhotonNetwork.AutomaticallySyncScene = true;

        // 이미 연결됐다면
        if (PhotonNetwork.IsConnected == true)
        {
            Debug.Log("Photon Manager ::: 이미 연결됨");
        }
        else
        {
            // Photon 서버에 연결
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // 방 만들기
    public void CreateNewRoom()
    {
        if (maxPlayersPerRoom == 2 || maxPlayersPerRoom == 3)
        {
            Debug.Log($"PhotonManager ::: maxPlayersPerRoom = {maxPlayersPerRoom}");
            _maxPlayersPerRoom = maxPlayersPerRoom;
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = _maxPlayersPerRoom;
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
    }

    // 선택한 방에 입장하기
    public void EnterSelectedRoom(string title)
    {
        PhotonNetwork.JoinRoom(title);
    }

    // 방 나가기
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    // 로비 나가기
    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    // 게임 시작 버튼 클릭 시 설정 정보 확인
    public bool CheckGameData()
    {
        // 플레이어 준비 상태 확인
        isGameDataReady = playerReadyState == _maxPlayersPerRoom - 1 ? true : false;
        Debug.Log($"PhotonManager ::: GameLevel Setting = {isGameDataReady} \n {playerReadyState}/{_maxPlayersPerRoom}명 준비 완료");

        // 게임 난이도 설정 확인
        Debug.Log($"PhotonManager ::: GameLevel = {gameLevel}");

        // 게임 맵 설정 확인
        Debug.Log($"PhotonManager ::: GameTheme = {gameTheme}");

        return isGameDataReady;
    }


    #region Callback 함수들

    // ----------[Photon 서버에 연결]--------------------

    // Photon 서버 연결에 성공한 경우
    public override void OnConnectedToMaster()
    {
        Debug.Log("PhotonManager ::: Photon 서버에 연결됨");
        Debug.Log($"PhotonManager ::: 지역 = {PhotonNetwork.CloudRegion} // 버전 = {PhotonNetwork.NetworkingClient.AppVersion} // 접속 인원 = {PhotonNetwork.CountOfPlayers}");

        if (PhotonNetwork.CountOfPlayers >= 20)
        {
            Debug.Log("PhotonManager ::: 서버에 너무 많은 사람이 들어왔습니다.");
            PhotonNetwork.Disconnect();
            return;
        }

        // 로비로 접속(로비 접속이 되면 자동으로 대기 방 목록 정보를 받아옴)
        PhotonNetwork.JoinLobby();

        // 플레이어 이름을 Photon 서버에 저장
        PhotonNetwork.NickName = GameManager.Instance.username;
        Debug.Log($"PhotonManager ::: {PhotonNetwork.NickName} // {GameManager.Instance.username}");
    }

    // Photon 서버 연결에 실패한 경우
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError($"PhotonManager ::: 연결 문제 있음 \n {cause}");
    }


    // ----------[방 목록]--------------------

    // 대기 방 목록 업데이트
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log($"PhotonManager ::: 방 목록 불러오기 완료 = {roomList.Count}");

        // 기존에 있던 방 목록을 삭제
        if (roomPanelList.Count > 0)
        {
            for (int i = 0; i < roomPanelList.Count; i++)
            {
                Destroy(roomPanelList[i]);
            }
            roomPanelList.Clear();
        }

        // 방 목록 업데이트
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].PlayerCount == 0)
            {
                continue;
            }

            GameObject obj = Instantiate(listPanelPrefab, roomListScrollView.transform);

            // 방 이미지 및 플레이어 인원 수 표시
            if (roomList[i].MaxPlayers == 2)
            {
                obj.transform.GetChild(0).GetComponent<Image>().sprite = maxPlayerCount02;

                string message = string.Format("{0} / {1}명", roomList[i].PlayerCount, roomList[i].MaxPlayers);
                obj.transform.GetChild(2).GetComponent<Text>().text = message;
            }
            else if (roomList[i].MaxPlayers == 3)
            {
                obj.transform.GetChild(0).GetComponent<Image>().sprite = maxPlayerCount03;

                string message = string.Format("{0} / {1}명", roomList[i].PlayerCount, roomList[i].MaxPlayers);
                obj.transform.GetChild(2).GetComponent<Text>().text = message;
            }

            // 방 이름 표시
            string title = roomList[i].Name;
            obj.transform.GetChild(1).GetComponent<Text>().text = title;
            obj.name = title;

            obj.GetComponent<Button>().onClick.AddListener(() => EnterSelectedRoom(title));
            obj.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => EnterSelectedRoom(title));

            roomPanelList.Add(obj);
        }
    }


    // ----------[선택한 방에 입장]--------------------

    // 선택한 방에 입장 성공한 경우
    public override void OnJoinedRoom()
    {
        canvasManager.OpenRoomWaitingCanvas();
        Debug.Log("PhotonManager ::: 선택한 방 입장");

        // 방 정보 최신화
        roomCtrl.SetRoomInfo(PhotonNetwork.CurrentRoom.Name
                            , PhotonNetwork.CurrentRoom.PlayerCount);
        roomName = string.Empty;
        maxPlayersPerRoom = 0;
    }

    // 선택한 방에 입장 실패한 경우
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"PhotonManager ::: 입장 실패 \n {message}");
        buttonManager.ShowJoinErrorPanel();
    }


    // ----------[방에서 나가기]--------------------

    // 방에서 나가기
    public override void OnLeftRoom()
    {
        Debug.Log("PhotonManager ::: 방에서 나옴");
        canvasManager.OpenRoomListCanvas();
    }


    // ----------[새로운 플레이어 접속 / 나가기]--------------------

    // 새로운 플레이어가 들어옴
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"PhotonManager ::: 새로운 플레이어 들어옴 // {newPlayer.NickName}");
        roomCtrl.SetRoomInfo(PhotonNetwork.CurrentRoom.Name
                            , PhotonNetwork.CurrentRoom.PlayerCount);
    }

    // 플레이어가 나감
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"PhotonManager ::: 플레이어 나감 // {otherPlayer.NickName}");

        if (canvasManager.isARPlayOn == true)
        {
            // 게임 데이터 초기화
            playCtrl.ResetTogetherGameData();

            // Canvas 전환
            canvasManager.OpenRoomWaitingCanvas();
        }
        else
        {
            roomCtrl.SetRoomInfo(PhotonNetwork.CurrentRoom.Name
                                , PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("PhotonManager ::: OnJoinedLobby");
    }

    #endregion
}
