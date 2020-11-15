using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DebuffType : int { Slow, }
public class Debuff
{
    private Timer timer = new Timer();
    private float amount;
    private float duration;
    private DebuffType type;

    public Debuff( DebuffType _type )
    {
        type = _type;
    }

    public DebuffType GetDebuffType()
    {
        return type;
    }

    public float GetAmount()
    {
        return amount;
    }

    public void Initialize( float _amount, float _duration )
    {
        if ( this.amount < _amount )
        {
            this.amount = _amount;
        }

        duration = _duration;
        timer.Initialize( duration );
    }

    public void Update()
    {
        if ( timer.Update() == false )
        {
            amount = 0.0f;
        }
    }

    public void OnStop()
    {
        amount = 0.0f;
        duration = 0.0f;
        timer.OnStop();
    }
}