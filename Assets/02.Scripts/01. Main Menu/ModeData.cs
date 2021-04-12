using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeData : MonoBehaviour
{
    public ModeType modeType = ModeType.None;

    public void ChangeModeType()
    {
        GameManager.Instance.modeType = modeType;

        if (modeType == ModeType.Alone_Count)
        {
            GameManager.Instance.currStageStateArray = GameManager.Instance.alone_01;
        }
        else if (modeType == ModeType.Alone_Minus)
        {
            GameManager.Instance.currStageStateArray = GameManager.Instance.alone_02;
        }
        else if (modeType == ModeType.Alone_Plus)
        {
            GameManager.Instance.currStageStateArray = GameManager.Instance.alone_03;
        }
        else if (modeType == ModeType.Create)
        {
            Debug.Log($"ModeData ::: modeType = {modeType}");
        }
        else
        {
            Debug.LogError($"ModeData ::: modeType = {modeType}");
        }
    }
}
