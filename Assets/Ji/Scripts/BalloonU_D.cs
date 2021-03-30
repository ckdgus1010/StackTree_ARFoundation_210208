using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonU_D : MonoBehaviour
{
    
    float a = 1;
   

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y < -6f)
        {
            a = 1;
        }
        else if (transform.localPosition.y > -4f)
        {
            a = -1;
        }

        transform.Translate(Vector3.up * 0.01f * Time.deltaTime * a);
    }


}
