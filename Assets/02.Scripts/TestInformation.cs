using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestInformation : MonoBehaviour
{
    public Text text; 

    void Start()
    {
        ModeType playModeType = GameManager.Instance.modeType;
        int stageID = GameManager.Instance.stageID;

        text.text = "PlayMode: " + playModeType + "\n" + "StageID: " + stageID;
    }
}
