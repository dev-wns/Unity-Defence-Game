using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Bullet : MonoBehaviour
{
    private readonly float life_time = 1.7f;
    private readonly float speed = 3500.0f;
    private Stopwatch destroy_timer = new Stopwatch();
    private Vector3 direction;
    public Player owner { get; private set; }

    // �浹 ���� �� �� Ȯ�ο�
    protected Collider2D[] colliders;
    private Transform origin_transform;
    public Transform current_transform
    {
        get
        {
            return origin_transform;
        }
        set
        {
            if ( !ReferenceEquals( value, null ) )
            {
                origin_transform = value;
            }
        }
    }

    public void Initialize( Player _owner, Vector2 spawn_pos, Vector2 _dir )
    {
        owner = _owner;
        direction = _dir;
        current_transform.position = spawn_pos;
        destroy_timer.Restart();
    }

    private void Awake()
    {
        current_transform = transform;
    }

    private void Update()
    {
        if ( destroy_timer.ElapsedMilliseconds >= life_time * 1000.0f )
        {
            OnDie();
        }

        current_transform.Translate( direction * speed * Time.deltaTime );
    }

    public void OnDie()
    {
        BulletObjectPool.Instance.Despawn( this );
        destroy_timer.Stop();
    }
}
