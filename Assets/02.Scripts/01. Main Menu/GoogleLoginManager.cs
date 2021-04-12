using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class GoogleLoginManager : MonoBehaviour
{
    public CanvasManager canvasManager;

    [Header("구글 로그인 에러 팝업")]
    public GameObject googleLoginErrorPopup;

    void Start()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    // 구글 로그인 버튼을 눌렀을 때
    public void ClickGoogleLoginButton()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                Debug.Log("구글 로그인 성공");
                canvasManager.ClickLoginButton();
            }
            else
            {
                Debug.Log("구글 로그인 실패");
                CloseErrorPopup();
            }
        });
    }

    public void CloseErrorPopup()
    {
        googleLoginErrorPopup.SetActive(!googleLoginErrorPopup.activeSelf);
    }
}
