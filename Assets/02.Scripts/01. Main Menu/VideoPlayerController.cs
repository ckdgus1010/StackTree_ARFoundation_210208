using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    public CanvasManager canvasManager;
    public float timer = 1.5f;

    // 영상이 끝났는지 확인
    private bool isFinished = false;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        isFinished = false;
    }

    void Update()
    {
        if (isFinished == false && videoPlayer.isPrepared && videoPlayer.isPlaying == false)
        {
            Debug.Log("VideoCtrl ::: 영상 끝, 로그인 화면으로 이동");

            isFinished = true;
            Invoke("FinishIntroVideo", timer);
        }
    }

    void FinishIntroVideo()
    {
        canvasManager.FinishIntroVideo();
    }
}
