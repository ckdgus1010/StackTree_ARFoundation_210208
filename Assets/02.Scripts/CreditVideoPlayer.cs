using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CreditVideoPlayer : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public float timer = 27.0f;
    private float currtime = 0.0f;

    public GameObject skipButton;
    public CanvasManager canvasManager;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        currtime = 0.0f;
        skipButton.SetActive(false);
    }

    void Update()
    {
        if (videoPlayer.isPrepared == true)
        {
            currtime += Time.deltaTime;

            if (currtime >= timer)
            {
                //Debug.Log("CreditVideoPlayer ::: 영상 끝, skip 버튼 활성화");

                skipButton.SetActive(true);
            }
        }
    }

    public void ClickSkipButton()
    {
        currtime = 0.0f;
        skipButton.SetActive(false);
        canvasManager.ClickCreditSkipButton();
    }
}
