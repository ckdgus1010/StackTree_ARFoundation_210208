using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    BGM, Effect, Button, Credit
}

public class SoundManager : MonoBehaviour
{
    #region Singleton Pattern

    // 싱글톤 패턴을 사용하기 위한 인스턴스 변수
    private static SoundManager _instance;

    // 인스턴스에 접근하기 위한 프로퍼티
    public static SoundManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(SoundManager)) as SoundManager;

                if (_instance == null)
                    Debug.Log("SoundManager ::: no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        // 아래의 함수를 사용하여 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    //--------------------------------------------------------------

    [Header("Audio Sources")]
    public AudioSource BGMPlayer;
    public AudioSource effectSoundPlayer;

    [Header("AudioClips")]
    public AudioClip BGM;
    public AudioClip creditMusic;
    public AudioClip buttonSound;
    public AudioClip screenshotSound;

    [Header("Volume Control")]
    [Range(0, 1)]
    public float BGMVolume = 0.5f;
    [Range(0, 1)]
    public float effectVolume = 0.5f;

    [Header("Mute Control")]
    public bool muteBGM = false;
    public bool muteEffectSound = false;

    public void ClickButton()
    {
        if (muteEffectSound == true)
        {
            return;
        }

        effectSoundPlayer.clip = buttonSound;
        effectSoundPlayer.volume = effectVolume;
        effectSoundPlayer.Play();
    }

    public void ClickScreenshotButton()
    {
        if (muteEffectSound == true)
        {
            Debug.Log("SoundManager ::: 음소거 됨");
            return;
        }

        effectSoundPlayer.clip = screenshotSound;
        effectSoundPlayer.volume = effectVolume;
        effectSoundPlayer.Play();
        Debug.Log($"SoundManager ::: {screenshotSound.name} // {effectVolume}");
    }
}
