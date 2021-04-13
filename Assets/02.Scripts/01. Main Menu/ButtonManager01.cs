using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager01 : MonoBehaviour
{
    public CanvasManager canvasManager;
    public PermissionManager permissionManager;

    [Header("Permission")]
    public GameObject permissionPanel;

    [Header("Login Canvas")]
    public PanelCtrl loginPanelCtrl;
    public PanelCtrl signupPanelCtrl;
    public ProfileImageScrollCtrl signupProfileImageCtrl;
    public PlayFabManager playFabManager;
    public GoogleLoginManager googleManager;

    [Header("Main Menu Scroll")]
    public MainMenuCtrl mainMenuCtrl;

    [Header("혼자하기 모드 단계 선택")]
    public GameObject stageSelectPanel;

    [Header("Help Popups")]
    public GameObject createModeHelpPopup;
    public GameObject aloneModeHelpPopup;
    public GameObject togetherModeHelpPopup;
    public Toggle mainMessage;

    [Header("Profile Panel")]
    public GameObject profilePanel;
    public Text usernameText;
    public Image profileImage;
    public Scrollbar achievementScrollbar;
    public AchievementData achievementData;
    public GameObject profileHelpPanel;
    public GameObject profileImagemodificationPanel;
    public ProfileImageScrollCtrl profileImageModificationCtrl;

    [Header("Setting Panel")]
    public GameObject settingPanel;
    public SettingCtrl settingCtrl;

    [Header("Credit Canvas")]
    public CreditVideoPlayer creditVideoPlayer;


    #region Login Canvas

    // 접근 권한 묻기
    public void ClickPermissionConfirmButton()
    {
        SoundManager.Instance.ClickButton();

        permissionManager.RequestPermission();
    }

    // 로그인 화면 >> [로그인] 버튼 클릭 시
    public void ClickLoginButton()
    {
        Debug.Log($"ButtonManager01 ::: ClickLoginButton");

        // 버튼 소리
        SoundManager.Instance.ClickButton();

        if (loginPanelCtrl.isButtonClicked == true)
        {
            playFabManager.ResetLoginInputField();
        }

        loginPanelCtrl.isButtonClicked = !loginPanelCtrl.isButtonClicked;
    }

    // 로그인 화면 >> [구글 로그인] 버튼 클릭 시
    public void ClickGoogleLoginButton()
    {
        Debug.Log($"ButtonManager01 ::: ClickGoogleLoginButton");

        // 버튼 소리
        SoundManager.Instance.ClickButton();
        googleManager.ClickGoogleLoginButton();
    }

    // 로그인 팝업 >> [로그인] 버튼 클릭 시
    public void ClickPlayFabLoginButton()
    {
        Debug.Log($"ButtonManager01 ::: ClickPlayFabLoginButtion");

        // 버튼 소리
        SoundManager.Instance.ClickButton();

        playFabManager.ClickPlayfabLoginButton();
    }


    // 로그인 팝업 >> [회원가입] 버튼 클릭 시
    public void ClickSignupButton()
    {
        Debug.Log($"ButtonManager01 ::: ClickSignupButton");

        // 버튼 소리
        SoundManager.Instance.ClickButton();

        if (signupPanelCtrl.isButtonClicked == true)
        {
            playFabManager.ResetSignupInpuField();
            signupProfileImageCtrl.currPointNum = 0;
        }

        signupPanelCtrl.isButtonClicked = !signupPanelCtrl.isButtonClicked;
        playFabManager.ResetLoginInputField();
    }

    // 회원가입 팝업 >> [회원가입] 버튼 클릭 시
    public void ClickPlayFabSignupButtion()
    {
        Debug.Log($"ButtonManager01 ::: ClickPlayFabSignupButtion");

        // 버튼 소리
        SoundManager.Instance.ClickButton();

        playFabManager.ClickSignUpButton();
    }

    // 로그인 에러 팝업 끄기
    public void CloseErrorPopup()
    {
        SoundManager.Instance.ClickButton();
        playFabManager.CloseErrorPopup();
    }

    // 구글 로그인 에러 팝업 끄기
    public void CloseGoogleLoginErrorPopup()
    {
        SoundManager.Instance.ClickButton();
        googleManager.CloseErrorPopup();
    }

    #endregion


    #region Main Menu Canvas

    // [Create Mode 버튼] 클릭 시
    public void ClickCreateMode()
    {
        Debug.Log("ButtonManager01 ::: [CreateMode] 버튼 클릭");

        SoundManager.Instance.ClickButton();
        AchievementManager.Instance.UpdateAchievementData(AchievementState.CreateMode_Count);
        SceneManager.LoadScene("03. Play Scene");
    }

    // [혼자하기 모드 버튼] 클릭 시
    public void ClickAloneModeButton()
    {
        SoundManager.Instance.ClickButton();

        mainMenuCtrl.ChangeSwipingState();
        mainMenuCtrl.ClickAloneButton();
    }

    // 혼자하기 모드 유형 선택
    public void SelectAloneModeStage()
    {
        if (stageSelectPanel.activeSelf == true)
        {
            GameManager.Instance.modeType = ModeType.None;
        }

        SoundManager.Instance.ClickButton();
        stageSelectPanel.SetActive(!stageSelectPanel.activeSelf);
    }

    // 혼자하기 모드 단계 선택
    public void SelectAloneStage()
    {
        SoundManager.Instance.ClickButton();

        int _stageID = GameManager.Instance.stageID - 1;
        if (GameManager.Instance.currStageStateArray[_stageID] != AloneModeStageState.Closed)
        {
            SceneManager.LoadScene("03. Play Scene");
        }
    }

    // [같이하기 모드] 버튼 클릭 시
    public void ClickTogetherMode()
    {
        Debug.Log("ButtonManager01 ::: [같이하기 모드] 버튼 클릭");

        SoundManager.Instance.ClickButton();

        SceneManager.LoadScene("04. Together Scene");
    }

    // [물음표] 버튼 클릭 시(Create Mode)
    public void ClickHelpCreateMode()
    {
        // 버튼 소리
        SoundManager.Instance.ClickButton();

        mainMenuCtrl.ClickHelpButton(ModeName.CreateMode);
    }

    // [물음표] 버튼 클릭 시(혼자하기 모드)
    public void ClickHelpAloneMode()
    {
        // 버튼 소리
        SoundManager.Instance.ClickButton();

        mainMenuCtrl.ClickHelpButton(ModeName.AloneMode);
    }

    // [물음표] 버튼 클릭 시(같이하기 모드)
    public void ClickHelpTogetherMode()
    {
        // 버튼 소리
        SoundManager.Instance.ClickButton();

        mainMenuCtrl.ClickHelpButton(ModeName.TogetherMode);
    }

    // 메인 메뉴 - [프로필] 버튼 클릭 시
    public void ClickProfileButton()
    {
        // 버튼 소리
        SoundManager.Instance.ClickButton();

        mainMenuCtrl.ChangeSwipingState();
        
        achievementScrollbar.value = 1.0f;
        usernameText.text = GameManager.Instance.username;
        profileImage.sprite = GameManager.Instance.profileImages[GameManager.Instance.profileImageNum];
        profilePanel.SetActive(!profilePanel.activeSelf);

        achievementData.UpdateAchievementStatus();
    }

    // 프로필 팝업 - [프로필 사진 수정 버튼] 클릭 시
    public void ClickProfileImageModificationButton()
    {
        profileImagemodificationPanel.SetActive(!profileImagemodificationPanel.activeSelf);
    }

    // 프로필 팝업 - 프로필 사진 수정 팝업 - [확인 버튼] 클릭 시
    public void ClickModificationConfirmButton()
    {
        // profileImageNum 변경
        GameManager.Instance.profileImageNum = profileImageModificationCtrl.currPointNum;
        profileImage.sprite = GameManager.Instance.profileImages[GameManager.Instance.profileImageNum];

        // 정보 저장
        SaveManager.Save();

        ClickProfileImageModificationButton();
    }

    // 프로필 팝업 - [도움말 버튼] 클릭 시
    public void ClickProfileHelpPanel()
    {
        SoundManager.Instance.ClickButton();

        profileHelpPanel.SetActive(!profileHelpPanel.activeSelf);
    }

    // 메인 메뉴 - [설정] 버튼 클릭 시
    public void ClickSettingButton()
    {
        //Debug.Log($"ButtonManager ::: ClickOptionButton()");

        SoundManager.Instance.ClickButton();

        mainMenuCtrl.ChangeSwipingState();
        settingCtrl.SetSoundData(!settingPanel.activeSelf);
        settingPanel.SetActive(!settingPanel.activeSelf);
    }

    // 설정 >> [만든이] 버튼 클릭 시
    public void ClickCreditButton()
    {
        SoundManager.Instance.ClickButton();

        settingPanel.SetActive(false);
        AchievementManager.Instance.UpdateAchievementData(AchievementState.CreditRun_Count);
        canvasManager.ClickCreditButton();
    }

    #endregion


    #region Credit Canvas

    public void ClickCreditSkipButton()
    {
        // 버튼 소리
        SoundManager.Instance.ClickButton();

        creditVideoPlayer.ClickSkipButton();
    }

    #endregion
}
