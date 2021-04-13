using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class PermissionManager : MonoBehaviour
{
    [Header("Permission Panel")]
    public GameObject permissionPanel;
    private bool isChecking = false;

    //카메라, 저장소 쓰기 권한 허용 유무
    private bool isCameraAllowed = false;
    private bool isStorageAllowed = false;

    [Header("Login Buttons")]
    public Button playfabLoginButton;
    public Button googleLoginButton;

    public void CheckPermission()
    {
        // 로그인 버튼 비활성화
        ControlLoginButtons(false);

        // 접근 권한 확인
        CheckPermissionState();
    }

    void CheckPermissionState()
    {
        // 카메라 권한 확인
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            isCameraAllowed = true;
            Debug.Log($"Permission Manager ::: 카메라 권한 승인됨");
        }

        // 저장소 쓰기 권한 확인
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            isStorageAllowed = true;
            Debug.Log($"Permission Manager ::: 저장소 쓰기 권한 승인됨");
        }

        // 저장소 쓰기 권한 확인
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Debug.Log($"Permission Manager ::: 저장소 읽기 권한 승인됨");
        }

        if (isCameraAllowed && isStorageAllowed)
        {
            Debug.Log($"Permission Manager ::: 모든 권한 승인됨");

            // 권한 요청 팝업창 비활성화
            permissionPanel.SetActive(false);

            // 로그인 버튼 활성화
            ControlLoginButtons(true);
        }
        else
        {
            Debug.Log($"Permission Manager ::: 필수 권한 요청 \n Camera // Write // Read = {Permission.HasUserAuthorizedPermission(Permission.Camera)} // {Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)} // {Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead)}");

            //권한 요청 팝업창 활성화
            permissionPanel.SetActive(true);
        }
    }

    // 권한 요청 팝업창에서 [확인 버튼]을 눌렀을 경우
    public void RequestPermission()
    {
        permissionPanel.SetActive(false);

        if (isChecking == false)
        {
            StartCoroutine(CheckPermissionCoroutine());
        }
    }

    IEnumerator CheckPermissionCoroutine()
    {
        // 코루틴 중복 방지
        isChecking = true;

        // 카메라 권한 요청
        RequestPermission_Camera();

        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => Application.isFocused == true);
        // 저장소 쓰기 권한 요청
        RequestPermission_ExternalStorageWrite();

        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => Application.isFocused == true);
        // 저장소 읽기 권한 요청
        RequestPermission_ExternalStorageRead();

        Debug.Log("Permission Manager ::: 권한 요청 완료");
        CheckPermissionState();

        isChecking = false;
    }

    // 카메라 권한 요청
    void RequestPermission_Camera()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Debug.Log($"Permission Manager ::: 카메라 권한 없음 // 권한 요청");
            Permission.RequestUserPermission(Permission.Camera);
        }
    }

    // 저장소 쓰기 권한 요청
    void RequestPermission_ExternalStorageWrite()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            Debug.Log($"Permission Manager ::: 저장소 쓰기 권한 없음 // 권한 요청");
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
    }

    // 저장소 읽기 권한 요청
    void RequestPermission_ExternalStorageRead()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Debug.Log($"Permission Manager ::: 저장소 쓰기 권한 없음 // 권한 요청");
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
    }

    // 로그인 버튼 활성화 관리
    void ControlLoginButtons(bool isPermissionGetted)
    {
        playfabLoginButton.enabled = isPermissionGetted;
        googleLoginButton.enabled = isPermissionGetted;
    }
}
