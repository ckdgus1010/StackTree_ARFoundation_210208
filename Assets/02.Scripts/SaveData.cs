using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // 플레이어 정보
    public string username;
    public int profileImageNum = 0;

    // 업적 정보
    public List<bool> achievementInfo = new List<bool>();
    public List<int> currAchievementState = new List<int>();

    // 혼자하기 모드 정보
    public List<int> aloneMode01 = new List<int>();
    public List<int> aloneMode02 = new List<int>();
    public List<int> aloneMode03 = new List<int>();

    // 사운드 정보
    public float bgmVolume = 0.5f;
    public float effectSoundVolume = 0.5f;
    public bool muteBGM = false;
    public bool muteEffectSound = false;

    public SaveData(string _username, int _profileImageNum
                   , List<bool> _achievementInfo, List<int> _currAchievementState
                   , List<int> _aloneMode01, List<int> _aloneMode02, List<int> _aloneMode03
                   , float _bgmVolume, float _effectSoundVolume, bool _isBGMMute, bool _isEffectSoundMute)
    {
        // 플레이어 정보
        this.username = _username;
        this.profileImageNum = _profileImageNum;

        // 업적 정보
        if (achievementInfo.Count > 0)
        {
            achievementInfo.Clear();
        }
        for (int i = 0; i < _achievementInfo.Count; i++)
        {
            achievementInfo.Add(_achievementInfo[i]);
        }

        if (currAchievementState.Count > 0)
        {
            currAchievementState.Clear();
        }
        for (int i = 0; i < _currAchievementState.Count; i++)
        {
            currAchievementState.Add(_currAchievementState[i]);
        }

        // 혼자하기 모드 정보
        if (aloneMode01.Count > 0)
        {
            aloneMode01.Clear();
        }
        for (int i = 0; i < _aloneMode01.Count; i++)
        {
            aloneMode01.Add(_aloneMode01[i]);
        }

        if (aloneMode02.Count > 0)
        {
            aloneMode02.Clear();
        }
        for (int i = 0; i < _aloneMode02.Count; i++)
        {
            aloneMode02.Add(_aloneMode02[i]);
        }

        if (aloneMode03.Count > 0)
        {
            aloneMode03.Clear();
        }
        for (int i = 0; i < _aloneMode03.Count; i++)
        {
            aloneMode03.Add(_aloneMode03[i]);
        }

        // 사운드 정보
        bgmVolume = _bgmVolume;
        effectSoundVolume = _effectSoundVolume;
        muteBGM = _isBGMMute;
        muteEffectSound = _isEffectSoundMute;
    }
}
