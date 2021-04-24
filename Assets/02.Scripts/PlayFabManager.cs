using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{
    public CanvasManager canvasManager;
    public PanelCtrl loginPanelCtrl;
    public PanelCtrl signupPanelCtrl;

    [Header("Login Info")]
    public InputField loginUsernameInput;
    public InputField loginPasswordInput;
    public GameObject loginErrorPopup;

    [Header("Signup Info")]
    public InputField signupUsernameInput;
    public InputField signupPasswordInput;
    public InputField signupEmailInput;
    public GameObject signupErrorPopup;

    private string username;

    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "34AE8";
        }
    }

    // [로그인 버튼] 클릭 시
    public void ClickPlayfabLoginButton()
    {
        username = loginUsernameInput.text;

        var request = new LoginWithPlayFabRequest { Username = loginUsernameInput.text, Password = loginPasswordInput.text };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    // 로그인 성공 시
    private void OnLoginSuccess(LoginResult result)
    {
        loginPanelCtrl.ResetPanelData();
        signupPanelCtrl.ResetPanelData();
        ResetLoginInputField();

        canvasManager.ClickLoginButton();
        GameManager.Instance.username = username;
        SaveManager.Save();
        Debug.Log("PlayfabManager ::: 로그인 성공");
    }

    // 로그인 실패 시
    private void OnLoginFailure(PlayFabError error)
    {
        loginErrorPopup.SetActive(true);
        username = null;
        Debug.Log($"PlayFabManager ::: 로그인 실패 {error}");
    }

    // [회원가입 버튼] 클릭 시
    public void ClickSignUpButton()
    {
        var request = new RegisterPlayFabUserRequest { Email = signupEmailInput.text, Password = signupPasswordInput.text, Username = signupUsernameInput.text };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    // 회원가입 성공 시
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        signupPanelCtrl.isButtonClicked = false;

        ResetSignupInpuField();
        Debug.Log("PlayFabManager ::: 회원가입 성공");
    }

    // 회원가입 실패 시
    private void OnRegisterFailure(PlayFabError error)
    {
        signupErrorPopup.SetActive(true);
        Debug.Log("PlayFabManager ::: 회원가입 실패");
    }

    // 로그인 관련 InputField 초기화
    public void ResetLoginInputField()
    {
        loginUsernameInput.text = "";
        loginPasswordInput.text = "";
    }

    // 회원가입 관련 InputField 초기화
    public void ResetSignupInpuField()
    {
        signupEmailInput.text = "";
        signupPasswordInput.text = "";
        signupUsernameInput.text = "";
    }

    // 에러 팝업 끄기
    public void CloseErrorPopup()
    {
        loginErrorPopup.SetActive(false);
        signupErrorPopup.SetActive(false);
    }
}
