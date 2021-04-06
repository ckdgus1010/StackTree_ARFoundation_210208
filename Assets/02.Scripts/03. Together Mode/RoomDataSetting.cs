using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomDataSetting : MonoBehaviour
{
    public PhotonManager photonManager;

    [Header("Room Info")]
    public InputField inputField;
    public int textCountLimit = 10;
    private string _roomName;
    private byte maxPlayersPerRoom = 0;

    [Header("Button Images")]
    public Sprite grayImage;
    public Sprite yellowImage;
    public Image countButton02;
    public Image countButton03;

    // [방 만들기 버튼] 클릭 시
    public void SetRoomData()
    {
        if (string.IsNullOrEmpty(_roomName) == true)
        {
            return;
        }

        if (maxPlayersPerRoom == 0)
        {
            return;
        }

        if (_roomName.Length > textCountLimit)
        {
            photonManager.roomName = _roomName.Substring(0, textCountLimit - 1);
        }
        else
        {
            photonManager.roomName = _roomName;
        }

        photonManager.maxPlayersPerRoom = maxPlayersPerRoom;
    }

    // 방 제목 설정
    public void SetRoomName()
    {
        _roomName = inputField.text;
    }

    // 플레이어 인원 수 설정
    public void SetPlayerCount(int count)
    {
        maxPlayersPerRoom = (byte)count;

        if (count == 2)
        {
            countButton02.sprite = yellowImage;
            countButton03.sprite = grayImage;
        }
        else
        {
            countButton02.sprite = grayImage;
            countButton03.sprite = yellowImage;
        }
    }

    // 데이터 리셋
    public void ResetRoomData()
    {
        _roomName = "";
        maxPlayersPerRoom = 0;
        SetRoomData();

        inputField.text = "";

        countButton02.sprite = grayImage;
        countButton03.sprite = grayImage;
    }
}
