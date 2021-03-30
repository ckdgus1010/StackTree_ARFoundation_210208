using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space_Rot : MonoBehaviour
{
    float a = 1;

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y < 3f)
        {
            a = 1;
        }
        else if (transform.localPosition.y > 3.5f)
        {
            a = -1;
        }

        transform.Translate(Vector3.up * 0.008f * Time.deltaTime * a);
    
        transform.Rotate(new Vector3(0, 30 * Time.deltaTime, 0));

    }
}
