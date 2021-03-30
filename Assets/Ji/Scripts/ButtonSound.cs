using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource bgmSource;
    public AudioSource effectSource;


    [Header("BGM")]
    public AudioClip bgmClip;
    public GameObject bgmOn;
    public GameObject bgmOff;
    public Slider bgmSlider;
    private float bgmValue = 0;

    [Header("Button Sound")]
    public AudioClip buttonClip;
    public GameObject effectOn;
    public GameObject effectOff;
    public Slider effectSlider;
    private float effectValue = 0;

    // 스크립트
    //public GameMap gameMap;

    // 맵 테마에 맞는 사운드 재생
    public void MapTemaSound()
    {
        //bgmSource.clip = gameMap.audioClip;
        bgmSource.Play();
    }

    // 플레이 시 브금 시작
    public void PlayBGM()
    {
        bgmSource.Play();
    }
    // 버튼 클릭 시
    public void ButtonClicked()
    {
        effectSource.clip = buttonClip;
        effectSource.Play();
    }

    // 배경음 조절
    public void ControllBGMVolume()
    {
        bgmSource.volume = bgmSlider.value;

        if (bgmSlider.value == 0)
        {
            bgmOff.SetActive(true);
            bgmOn.SetActive(false);
        }
        else
        {
            bgmOn.SetActive(true);
            bgmOff.SetActive(false);
        }
    }

    // 효과음 조절
    public void ControllEffectVolume()
    {
        effectSource.volume = effectSlider.value;

        if (effectSlider.value == 0)
        {
            effectOff.SetActive(true);
            effectOn.SetActive(false);
        }
        else
        {
            effectOn.SetActive(true);
            effectOff.SetActive(false);
        }
    }

    // 배경음 음소거
    public void MuteBGM()
    {
        if (bgmOff.activeSelf == false)
        {
            bgmOff.SetActive(true);
            bgmOn.SetActive(false);

            bgmValue = bgmSlider.value;
            bgmSlider.value = 0;
            bgmSource.volume = 0;
        }
        else
        {
            bgmOn.SetActive(true);
            bgmOff.SetActive(false);

            bgmSource.volume = bgmValue;
            bgmSlider.value = bgmValue;
        }
    }

    // 효과음 음소거
    public void MuteEffect()
    {
        if (effectOff.activeSelf == false)
        {
            effectOff.SetActive(true);
            effectOn.SetActive(false);

            effectValue = effectSlider.value;
            effectSlider.value = 0;
            effectSource.volume = 0;
        }
        else
        {
            effectOn.SetActive(true);
            effectOff.SetActive(false);

            effectSource.volume = effectValue;
            effectSlider.value = effectValue;
        }
    }
}
