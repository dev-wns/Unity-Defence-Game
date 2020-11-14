using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private bool isRun = false;
    private float timer = 0.0f;
    private float duration = 0.0f;

    public void Initialize( float _duration )
    {
        duration = _duration;
        timer = 0.0f;
        isRun = true;
    }

    public bool Update()
    {
        if ( timer >= duration )
        {
            isRun = false;
        }
        timer += Time.deltaTime;

        return isRun;
    }
}
