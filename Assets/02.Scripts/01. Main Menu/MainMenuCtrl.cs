using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ModeName
{
    CreateMode, AloneMode, TogetherMode
}

public class MainMenuCtrl : MonoBehaviour
{
    [Header("혼자하기 모드 애니메이션")]
    public Animator animator;
    public bool isButtonClicked = false;
    public float timer = 1.0f;
    public GameObject profile;
    public GameObject setting;
    private int hashIsClicked;

    [Header("Help Popups")]
    public GameObject[] helpPopups = new GameObject[3];

    [Header("Control Menu Swiping")]
    public SwipeMenu swipeMenu;
    public ScrollRect scrollRect;
    public Scrollbar scrollbar;

    [Header("Together Mode Help Popup")]
    public Toggle mainMessage;

    [Header("Block Other Buttons")]
    public Button profileButton;
    public Button optionButton;

    void Start()
    {
        isButtonClicked = false;
        
        hashIsClicked = Animator.StringToHash("IsClicked");
    }

    // [혼자하기 모드] 버튼 클릭 시
    public void ClickAloneButton()
    {
        if (isButtonClicked == false)
        {
            swipeMenu.ClickAloneModeButton();

            profile.SetActive(false);
            setting.SetActive(false);

            isButtonClicked = true;
            animator.SetBool(hashIsClicked, true);
        }
        else
        {
            Invoke("ConvertButtons", timer);

            isButtonClicked = false;
            animator.SetBool(hashIsClicked, false);
        }
    }

    void ConvertButtons()
    {
        profile.SetActive(true);
        setting.SetActive(true);
    }

    public void ClickHelpButton(ModeName modeName)
    {
        GameObject helpPopup = helpPopups[(int)modeName];
        Debug.Log($"MainMenuCtrl ::: helpPopup = {helpPopup.name}");

        // 다른 버튼 활성화/비활성화
        profileButton.enabled = helpPopup.activeSelf;
        optionButton.enabled = helpPopup.activeSelf;

        // 메뉴 스와이프 잠금/해제
        swipeMenu.enabled = helpPopup.activeSelf;
        scrollRect.enabled = helpPopup.activeSelf;
        scrollbar.value = (int)modeName * 0.5f;

        // 같이하기 모드 안내창 초기화
        if (modeName == ModeName.TogetherMode)
        {
            mainMessage.isOn = true;
        }

        helpPopup.SetActive(!helpPopup.activeSelf);
    }

    public void ChangeSwipingState()
    {
        swipeMenu.enabled = !swipeMenu.enabled;
        scrollRect.enabled = !scrollRect.enabled;
    }
}
