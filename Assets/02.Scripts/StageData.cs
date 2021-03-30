using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageData : MonoBehaviour
{
    public int stageID = 0;

    public void ChangeStageID()
    {
        GameManager.Instance.stageID = this.stageID;
    }
}
