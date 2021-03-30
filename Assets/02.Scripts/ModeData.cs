using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeData : MonoBehaviour
{
    public ModeType modeType = ModeType.None;

    public void ChangeModeType()
    {
        GameManager.Instance.modeType = modeType;

        if (modeType == ModeType.Alone_Count || modeType == ModeType.Alone_Minus || modeType == ModeType.Alone_Plus)
        {
            GameManager.Instance.currStageStateArray = GameManager.Instance.aloneModeStage[(int)modeType - 2];
        }
    }
}
