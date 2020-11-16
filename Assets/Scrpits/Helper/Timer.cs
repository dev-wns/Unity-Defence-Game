using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer
{
    private bool is_apply = false;
    [SerializeField]
    private float timer = 0.0f;

    public bool IsApply()
    {
        return is_apply;
    }

    public void Initialize( float _duration )
    {
        if ( timer <= _duration )
        {
            timer = _duration;
        }
        is_apply = true;
    }

    public bool Update()
    {
        timer -= Time.deltaTime;
        if ( timer <= 0.0f )
        {
            is_apply = false;
        }

        return is_apply;
    }

    public void OnStop()
    {
        is_apply = false;
        timer = 0.0f;
    }
}
