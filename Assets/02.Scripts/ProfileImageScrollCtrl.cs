using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileImageScrollCtrl : MonoBehaviour
{
    private int profileImageNum = 0;

    [Header("생성할 프로필 이미지 프리팹")]
    public GameObject profileImageIconPrefab;

    [Header("프로필 이미지 아이콘 관리")]
    public List<GameObject> profileImageIcons = new List<GameObject>();

    private List<float> points = new List<float>();
    private float imageSize;

    void Start()
    {
        SetProfileImageIcons();
    }

    void Update()
    {

    }

    public void SetProfileImageIcons()
    {
        // Profile Image Icon 생성
        if (profileImageIcons.Count > 0)
        {
            return;
        }

        int count = GameManager.Instance.profileImages.Length;

        for (int i = 0; i < count; i++)
        {
            GameObject icon = Instantiate(profileImageIconPrefab, transform);
            icon.GetComponent<Image>().sprite = GameManager.Instance.profileImages[i];

            if (i == 0)
            {
                imageSize = icon.GetComponent<RectTransform>().sizeDelta.x;
            }

            profileImageIcons.Add(icon);
        }

        // 이동할 수 있는 지점 생성
        if (points.Count > 0)
        {
            return;
        }

        HorizontalLayoutGroup layoutGroup = GetComponent<HorizontalLayoutGroup>();
        float leftPadding = layoutGroup.padding.left;
        float spacing = layoutGroup.spacing;

        for (int i = 0; i < count; i++)
        {
            int point = 0;

            if (i == 0)
            {
                points[i] = imageSize + leftPadding;
            }
            else
            {
                float gap = imageSize + spacing;
                points[i] = points[i - 1] - gap;
            }

            points.Add(point);
        }
    }

    public void ClickProfileImageIcon(int num)
    {
        profileImageNum = num;
    }

    public void ChangeProfileImageNum()
    {
        GameManager.Instance.profileImageNum = profileImageNum;
        profileImageNum = 0;
    }
}
