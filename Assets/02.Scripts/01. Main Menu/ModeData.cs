using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeData : MonoBehaviour
{
    public ModeType modeType = ModeType.None;

    public void ChangeModeType()
    {
        GameManager.Instance.modeType = modeType;

        switch(modeType)
        {
            case ModeType.Create:
            case ModeType.Together:
                Debug.Log($"ModeType = {modeType}");
                break;
            case ModeType.Alone_Count:
                GameManager.Instance.currStageStateArray = GameManager.Instance.alone_01;
                break;
            case ModeType.Alone_Minus:
                GameManager.Instance.currStageStateArray = GameManager.Instance.alone_02;
                break;
            case ModeType.Alone_Plus:
                GameManager.Instance.currStageStateArray = GameManager.Instance.alone_03;
                break;
        }
    }
}
