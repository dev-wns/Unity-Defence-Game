using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuff : MonoBehaviour
{
    public float duration = 0.0f;
    public Timer timer = new Timer();

    public virtual void Apply( Enemy _target )
    {
        // timer.Execute( duration );
    }
}
