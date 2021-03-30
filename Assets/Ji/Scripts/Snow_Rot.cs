using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow_Rot : MonoBehaviour
{
    float a = 1;

    // Update is called once per frame
    void Update()
    {
       // if (transform.localPosition.z < 9f)
       // {
       //     a = 1;
       // }
       // else if (transform.localPosition.z > 12f)
       // {
       //     a = -1;
       // }
       //
       // transform.Translate(Vector3.forward * 0.03f * Time.deltaTime * a);

        transform.Rotate(new Vector3( 0, 0, 30 * Time.deltaTime * 2f));

    }
}
