using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    void Start()
    {
        QuestManager.Instance.SetCurrQuest();
    }
}
