using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameObject[] otherPanels;

    public void OnEnable () {
        print("OnEnable");
        
        for (int i = 0; i < otherPanels.Length; i++)
        {
            otherPanels[i].SetActive(true);
        }

    }

    public void OnDisable () {
        print("OnDisable");
        for (int i = 0; i < otherPanels.Length; i++)
        {
            otherPanels[i].SetActive(false);
        }
    }
}
