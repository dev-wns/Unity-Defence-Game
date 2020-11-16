using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public enum DebuffType : int { Slow, Stun, Curse, }
public class Debuff
{
    private bool is_apply;
    private float amount;
    private float duration;
    private Stopwatch timer = new Stopwatch();
    
    public bool isApply()
    {
        return is_apply;
    }

    public void Restart()
    {
        if ( isApply() == true )
        {
            timer.Restart();
            is_apply = true;
        }
    }

    public float GetAmountAndUpdate()
    {
        if ( timer.ElapsedMilliseconds >= duration * 1000.0f )
        {
            is_apply = false;
            amount = 0.0f;
            timer.Stop();
        }

        return amount;
    }

    public void Initialize( float _amount, float _duration )
    {
        if ( this.amount < _amount )
        {
            this.amount = _amount;
        }
        duration = _duration;
        is_apply = true;
        timer.Restart();
    }

    public void OnStop()
    {
        amount = 0.0f;
        duration = 0.0f;
        timer.Stop();
    }
}