using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolutionCtrl : MonoBehaviour
{
    public float width = 9.0f;
    public float height = 18.5f;
    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;

        // 스마트폰 해상도 비율 / 원하는 해상도 비율(9:16)
        float scaleHeight = ((float)Screen.width / Screen.height) / ((float)width / height);
        float scaleWidth = 1f / scaleHeight;

        // 날씬한 경우(상하 레터박스)
        if (scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        // 뚱뚱한 경우(좌우 레터박스)
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }

        camera.rect = rect;
    }
}
