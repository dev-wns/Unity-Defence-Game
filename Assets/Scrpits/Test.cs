using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        GetComponent<Transform>().Rotate( Time.deltaTime * 10.0f, 0.0f, 0.0f );
    }
}
