using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeTest : MonoBehaviour
{
    public GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           //iTween.ScaleTo(map, iTween.Hash("x", 1, "y", 1, "z", 1, "default", .1,"easetype",iTween.EaseType.easeOutBounce));
        }
        
    }
}
