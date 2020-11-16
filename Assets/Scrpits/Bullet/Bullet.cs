using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 direction;
    private Stopwatch destroy_timer = new Stopwatch();
    private float life_time;

    // 충돌 범위 내 적 확인용
    protected Collider2D[] colliders;

    public void Initialize( Vector2 spawn_pos, Vector2 _dir )
    {
        direction = _dir;
        transform.position = spawn_pos;
        destroy_timer.Restart();
    }

    private void Start()
    {
        speed = 3000.0f;
        life_time = 1.7f;
    }

    private void Update()
    {
        if ( destroy_timer.ElapsedMilliseconds >= life_time * 1000.0f )
        {
            BulletObjectPool.Instance.Despawn( this );
        }

        this.transform.Translate( direction * speed * Time.deltaTime );
    }

    private void OnTriggerEnter2D( Collider2D _other )
    {
        if ( _other.transform.CompareTag( "Enemy" ) == true )
        {
            BulletObjectPool.Instance.Despawn( this );
            destroy_timer.Stop();
        }
    }

    public virtual void Ability( Enemy _target ) { }
}
