using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CreditPlay : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    float currentTime;
    [Range(0.1f, 5)]
    public float creatTime = 0.5f;

    public GameObject skip;

    // 스크립트
    //public CanvasController canCtrl;
    public ButtonSound buttonSound;
    public SwipeMenu swipeMenu;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        buttonSound.bgmSlider.value = 0f;
        if (videoPlayer.isPrepared)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 27f)
            {
                //Debug.Log("영상 끝");
                skip.SetActive(true);
                currentTime = 0;
            }
        }
    }

    public void SkipButtonOn()
    {
        //canCtrl.canvasArray[2].SetActive(true);
        //canCtrl.canvasArray[4].SetActive(true);

        //swipeMenu.enabled = true;

        //buttonSound.bgmSlider.value = 1f;
        //canCtrl.canvasArray[3].SetActive(false);
    }
}
