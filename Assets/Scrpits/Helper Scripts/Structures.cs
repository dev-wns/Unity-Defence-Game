using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct PoolData
{
    public GameObject obj_parent;
    public Transform obj_transform;
    public PoolData( GameObject _obj, Transform _transform )
    {
        obj_parent = _obj;
        obj_transform = _transform;
    }
}

public struct DebuffData
{
    public float duration;
    public float amount;
    public DebuffData( float _duration, float _amount )
    {
        duration = _duration;
        amount = _amount;
    }
}