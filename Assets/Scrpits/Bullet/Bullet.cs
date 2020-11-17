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
    private Player owner;
    public Transform current_transform;
    // 충돌 범위 내 적 확인용
    protected Collider2D[] colliders;

    public Player GetOwner()
    {
        return owner;
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

    private void Start()
    {
        speed = 3500.0f;
        life_time = 1.7f;
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
