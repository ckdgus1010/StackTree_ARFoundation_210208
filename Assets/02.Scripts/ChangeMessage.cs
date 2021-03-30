using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMessage : MonoBehaviour
{
    public Toggle message;

    public void ChaneTogetherModeMessage()
    {
        message.isOn = true;
    }
}
