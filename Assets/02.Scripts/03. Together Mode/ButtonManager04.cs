using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ButtonManager04 : MonoBehaviour
{
    public CanvasManager02 canvasManager;
    public PhotonManager photonManager;
    public PhotonView photonView;

    [Header("RoomList Canvas")]
    public GameObject roomListPrefab;
    public GameObject roomMakerPanel;
    public GameObject roomMakerHelpPanel;
    public GameObject roomJoinErrorPanel;
    public RoomDataSetting roomDataSetting;

    [Header("Room Waiting Canvas")]
    public RoomCtrl roomCtrl;
    public GameObject errorPanel;

    [Header("Together Play Canvas")]
    public GameObject togetherPlayCanvas;
    public TogetherPlayCtrl togetherPlayCtrl;
    public GameObject playHelperPrefab;
    private GameObject playHelper;

    #region RoomList Canvas

    // RoomList Canvas - [뒤로 가기 버튼]을 눌렀을 때
    public void ClickBackButtonInRoomListCanvas()
    {
        Debug.Log("메인 메뉴로");
        photonManager.LeaveLobby();
        SceneManager.LoadScene("01. Main Scene");
    }

    // RoomList Canvas - [방 만들기 버튼]을 눌렀을 때
    public void ClickMakeRoomPanelButton()
    {
        if (roomMakerPanel.activeSelf == true)
        {
            roomDataSetting.ResetRoomData();
        }

        // 방 만들기 팝업 오픈
        roomMakerPanel.SetActive(!roomMakerPanel.activeSelf);
    }

    // RoomList Canvas - 방 만들기 팝업에서 [방 만들기 버튼]을 눌렀을 때
    public void ClickCreateNewRoomButton()
    {
        roomDataSetting.SetRoomData();
        Debug.Log($"ButtonManager04 ::: roomName = {photonManager.roomName} // maxPlayersPerRoom = {photonManager.maxPlayersPerRoom}");
        if (string.IsNullOrEmpty(photonManager.roomName))
        {
            return;
        }
        
        if (photonManager.maxPlayersPerRoom == 0)
        {
            return;
        }

        photonManager.CreateNewRoom();
        ClickMakeRoomPanelButton();
    }

    // RoomList Canvas - 방 만들기 팝업에서 [도움말 버튼]을 눌렀을 때
    public void ClickHelpButtonInRoomMakerPopup()
    {
        roomMakerHelpPanel.SetActive(!roomMakerHelpPanel.activeSelf);
    }

    // RoomList Canvas - 방에 입장할 때 오류가 생긴 경우
    public void ShowJoinErrorPanel()
    {
        roomJoinErrorPanel.SetActive(!roomJoinErrorPanel.activeSelf);
    }

    #endregion


    #region Room Waiting Canvas

    // [뒤로 가기 버튼]을 누른 경우
    public void ClickBackToRoomList()
    {
        photonManager.LeaveRoom();
    }

    // ----------[준비 버튼]을 누른 경우--------------------

    public void SendReadyCount(Image image)
    {
        // 방장이 아닌 경우
        if (PhotonNetwork.IsMasterClient == false)
        {
            // 방장만 실행
            bool isPlayerReady = roomCtrl.ClickReadyButton(image);
            photonView.RPC("ClickReadyButton", RpcTarget.MasterClient, isPlayerReady);

            // 본인을 제외한 나머지 플레이어 모두 실행
            int playerNumber = roomCtrl.CheckReadyPlayer(image);
            photonView.RPC("ChangeReadyButtonColor", RpcTarget.OthersBuffered, isPlayerReady, playerNumber);

            Debug.Log($"ButtonManager04 ::: 준비 버튼 클릭 {isPlayerReady}");
        }
    }

    // 방장만 다른 플레이어들의 준비 상태에 대한 정보를 저장
    [PunRPC]
    public void ClickReadyButton(bool isPlayerReady)
    {
        if (isPlayerReady == true)
        {
            roomCtrl.playerReadyCount += 1;
        }
        else
        {
            roomCtrl.playerReadyCount -= 1;
        }

        Debug.Log($"ButtonManager ::: PlayerReadyCount = {roomCtrl.playerReadyCount}");
    }

    // 다른 플레이어들이 준비된 상태를 공유
    [PunRPC]
    public void ChangeReadyButtonColor(bool isPlayerReady, int playerNumber)
    {
        roomCtrl.ChangeReadyButtonColor(isPlayerReady, playerNumber);
    }

    // ----------[게임 난이도 버튼]을 누른 경우--------------------

    public void SendChangingGameLevel(int value)
    {
        // 방장인 경우
        if (PhotonNetwork.IsMasterClient == true)
        {
            // 모두 실행
            photonView.RPC("ClickGameLevelButton", RpcTarget.AllBuffered, value);
        }
    }

    // 게임 난이도 설정
    [PunRPC]
    public void ClickGameLevelButton(int value)
    {
        roomCtrl.ClickLevelControlButton(value);
    }


    // ----------[맵 선택 버튼]을 누른 경우--------------------

    public void SendChangeMapTheme(int value)
    {
        // 방장인 경우
        if (PhotonNetwork.IsMasterClient == true)
        {
            // 모두 실행
            photonView.RPC("ClickMapNameButton", RpcTarget.AllBuffered, value);
        }
    }

    // 게임 맵 설정
    [PunRPC]
    public void ClickMapNameButton(int value)
    {
        roomCtrl.ClickMapNameControlButton(value);
    }


    // ----------[시작 하기 버튼]을 누른 경우--------------------
    public void SendGameStart()
    {
        // 방장인 경우
        if (PhotonNetwork.IsMasterClient == true)
        {
            // 게임 데이터 최종 세팅
            roomCtrl.SetPlayData();

            // 게임 데이터가 모두 입력됐는지 확인
            bool isGameDataPrepared = photonManager.CheckGameData();
            if (isGameDataPrepared == true)
            {
                // 모두 실행
                photonView.RPC("ClickPlayButton", RpcTarget.AllBuffered);
            }
            else
            {
                ConvertErrorPanel();
            }
        }
    }

    [PunRPC]
    public void ClickPlayButton()
    {
        Debug.Log($"ButtonManager ::: 게임 시작");

        canvasManager.OpenTogetherPlayCanvas();
        StartTogetherPlay();
    }

    // 게임 설정이 완료되지 않은 경우
    public void ConvertErrorPanel()
    {
        errorPanel.SetActive(!errorPanel.activeSelf);
    }

    #endregion


    #region Together Play Canvas

    // 게임 방법 안내 팝업 생성
    public void StartTogetherPlay()
    {
        Invoke("ShowHowToPlay", 2.0f);
    }

    void ShowHowToPlay()
    {
        if (togetherPlayCtrl.playHelper == null)
        {
            playHelper = Instantiate(playHelperPrefab, togetherPlayCanvas.transform);
            playHelper.GetComponent<PlayHelperCtrl>().SetButtonData(this);
            togetherPlayCtrl.playHelper = playHelper;
        }
        else
        {
            togetherPlayCtrl.playHelper.SetActive(true);
        }

        Debug.Log("ButtonManager04 ::: 게임 방법 안내 시작");
    }

    public void ClickPlayHelperHideButton()
    {
        playHelper.SetActive(false);
        Debug.Log("ButtonManager04 ::: [게임 시작 / 스킵 버튼] 누름");
    }

    #endregion
}
