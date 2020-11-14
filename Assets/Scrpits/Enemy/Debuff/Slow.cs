using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : Debuff
{
    public float slowAmount = 0.0f;

    public void Run( float _amount, float _duration )
    {
        slowAmount = _amount;
        duration = _duration;
        timer.Execute( duration );
    }

    public override void Apply( Enemy _target )
    {
        if ( timer.isExecute == true )
        {
            _target.moveSpeed = _target.originSpeed - slowAmount;
        }
        else
        {
            slowAmount = 0.0f;
        }

        base.Apply( _target );
    }
}
