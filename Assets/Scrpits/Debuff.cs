using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public enum DebuffType : int { Slow, Stun, Curse, }
public class Debuff : MonoBehaviour
{
    private float duration;
    private Stopwatch timer = new Stopwatch();
    public float amount { get; private set; }
    public bool isApplied { get; private set; }

    private void Update()
    {
        if ( timer.ElapsedMilliseconds >= duration )
        {
            isApplied = false;
            amount = 0.0f;
            timer.Stop();
        }
    }

    public void Restart()
    {
        if ( isApplied )
        {
            timer.Restart();
            isApplied = true;
        }
    }   

    public void Initialize( float _amount, float _duration )
    {
        if ( amount < _amount )
        {
            amount = _amount;
        }
        duration = _duration * 1000.0f;
        isApplied = true;
        timer.Restart();
    }

    public void OnStop()
    {
        amount = 0.0f;
        duration = 0.0f;
        timer.Stop();
    }
}