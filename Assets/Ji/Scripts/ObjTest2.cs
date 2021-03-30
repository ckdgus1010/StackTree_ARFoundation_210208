using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjTest2 : MonoBehaviour
{
    
    float a = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y < 0.49f)
        {
            a = 1;
        }
        else if (transform.localPosition.y >0.5f)
        {
            a = -1;
        }

        transform.Translate(Vector3.up * 0.008f * Time.deltaTime * a);
    }


}
