using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool isExecute = false;
    public float timer = 0.0f;
    public float duration = 0.0f;

    private void Update()
    {
        if ( timer <= duration )
        {
            timer += Time.deltaTime;
        }
        else
        {
            isExecute = false;
        }
    }

    public void Execute( float _duration )
    {
        timer = 0.0f;
        isExecute = true;
        duration = _duration;
    }
}
