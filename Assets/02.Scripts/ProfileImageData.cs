using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileImageData : MonoBehaviour
{
    public int profileImageID = 1000;

    public void ChangeProfileImageNum()
    {
        Debug.Log($"ProfileImageID ::: profileImageID = {profileImageID}");

        GameManager.Instance.profileImageNum = profileImageID;
    }
}
