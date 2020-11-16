using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer
{
    private bool is_run = false;
    [SerializeField]
    private float timer = 0.0f;

    public bool IsRun()
    {
        return is_run;
    }

    public void Initialize( float _duration )
    {
        if ( timer <= _duration )
        {
            timer = _duration;
        }
        is_run = true;
    }

    public bool Update()
    {
        timer -= Time.deltaTime;
        if ( timer <= 0.0f )
        {
            is_run = false;
        }

        return is_run;
    }

    public void OnStop()
    {
        is_run = false;
        timer = 0.0f;
    }
}
