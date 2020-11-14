using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DebuffType : int { Slow, }
public class Debuff
{
    private DebuffType type;
    private Timer timer = new Timer();
    private float amount;
    private float duration;

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
        amount = _amount;
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
}