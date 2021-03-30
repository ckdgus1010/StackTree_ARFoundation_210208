using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingCtrl : MonoBehaviour
{
    [Header("Mute")]
    public GameObject BGMOn;
    public GameObject BGMOff;
    public GameObject effectSoundOn;
    public GameObject effectSoundOff;

    [Header("Volume Slider")]
    public Slider BGMVolumeSlider;
    public Slider effectSoundVolumeSlider;

    // 설정창 오픈 시 SoundManager에서 볼륨값 받아오기
    public void SetSoundData(bool isOpened)
    {
        bool muteBGM = SoundManager.Instance.muteBGM;
        bool muteEffectSound = SoundManager.Instance.muteEffectSound;

        if (isOpened)
        {
            BGMVolumeSlider.value = muteBGM ? 0.0f : SoundManager.Instance.BGMVolume;
            effectSoundVolumeSlider.value = muteEffectSound ? 0.0f : SoundManager.Instance.effectVolume;
        }
    }

    // BGM 볼륨 조절
    public void ControlBGMVolume()
    {
        if (BGMVolumeSlider.value > 0)
        {
            BGMOn.SetActive(true);
            BGMOff.SetActive(false);

            SoundManager.Instance.BGMVolume = BGMVolumeSlider.value;
        }
        else if (effectSoundVolumeSlider.value <= 0)
        {
            BGMOff.SetActive(true);
            BGMOn.SetActive(false);

            SoundManager.Instance.muteBGM = true;
        }

    }

    // 효과음 볼륨 조절
    public void ControlEffectSoundVolume()
    {
        if (effectSoundVolumeSlider.value > 0)
        {
            effectSoundOn.SetActive(true);
            effectSoundOff.SetActive(false);

            SoundManager.Instance.muteEffectSound = false;
            SoundManager.Instance.effectVolume = effectSoundVolumeSlider.value;
        }
        else if (effectSoundVolumeSlider.value <= 0)
        {
            effectSoundOff.SetActive(true);
            effectSoundOn.SetActive(false);

            effectSoundVolumeSlider.value = 0.0f;
            SoundManager.Instance.muteEffectSound = true;
        }
    }

    // BGM 음소거
    public void MuteBGM()
    {
        // 음소거 시
        if (BGMOn.activeSelf == true)
        {
            // 현재 볼륨 값 저장
            SoundManager.Instance.BGMVolume = BGMVolumeSlider.value;
            SoundManager.Instance.muteBGM = true;
            BGMVolumeSlider.value = 0.0f;

            BGMOff.SetActive(true);
            BGMOn.SetActive(false);
        }
        else if (BGMOff.activeSelf == true)
        {
            SoundManager.Instance.muteBGM = false;

            BGMOn.SetActive(true);
            BGMOff.SetActive(false);

            BGMVolumeSlider.value = SoundManager.Instance.BGMVolume;
        }
    }

    // 효과음 음소거
    public void MuteEffectSound()
    {
        // 음소거 시
        if (effectSoundOn.activeSelf == true)
        {
            // 현재 볼륨 값 저장
            SoundManager.Instance.effectVolume = effectSoundVolumeSlider.value;
            SoundManager.Instance.muteEffectSound = true;
            effectSoundVolumeSlider.value = 0.0f;

            effectSoundOff.SetActive(true);
            effectSoundOn.SetActive(false);
        }
        else if ( effectSoundOff.activeSelf == true)
        {
            SoundManager.Instance.muteEffectSound = false;

            effectSoundOn.SetActive(true);
            effectSoundOff.SetActive(false);

            effectSoundVolumeSlider.value = SoundManager.Instance.effectVolume;
        }
    }
}
