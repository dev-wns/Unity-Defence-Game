using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer
{
    private bool isRun = false;
    [SerializeField]
    private float timer = 0.0f;

    public bool IsRun()
    {
        return isRun;
    }

    public void Initialize( float _duration )
    {
        if ( timer <= _duration )
        {
            timer = _duration;
        }
        isRun = true;
    }

    public bool Update()
    {
        timer -= Time.deltaTime;
        if ( timer <= 0.0f )
        {
            isRun = false;
        }

        return isRun;
    }

    public void OnStop()
    {
        isRun = false;
        timer = 0.0f;
    }
}
