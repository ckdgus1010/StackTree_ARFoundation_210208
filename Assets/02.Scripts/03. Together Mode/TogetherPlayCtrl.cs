using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogetherPlayCtrl : MonoBehaviour
{
    [HideInInspector]
    public GameObject playHelper;

    public void ResetTogetherGameData()
    {
        Debug.Log("TogetherPlayController ::: 게임 데이터 초기화 시작");

        // 게임 방법 안내 팝업 끄기
        if (playHelper != null)
        {
            playHelper.SetActive(false);
        }

        Debug.Log("TogetherPlayController ::: 게임 데이터 초기화 끝");
    }
}
