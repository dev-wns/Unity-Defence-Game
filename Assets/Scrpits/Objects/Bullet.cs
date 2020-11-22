using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Bullet : MonoBehaviour
{
#pragma warning disable CS0649 // 초기화 경고 무시
    [SerializeField]
    private List<AudioClip> shot_sounds;
#pragma warning restore CS0649

    private readonly float life_time = 1.7f;
    private readonly float speed = 1500.0f;
    private Stopwatch destroy_timer = new Stopwatch();
    private Vector3 direction;
    public Player owner { get; private set; }

    protected Collider2D[] colliders;
    public Transform current_transform { get; private set; }

    public void Initialize( Player _owner, Vector2 spawn_pos, Vector2 _dir )
    {
        owner = _owner;
        direction = _dir;
        current_transform.position = new Vector3( spawn_pos.x, spawn_pos.y, -1.0f );
        destroy_timer.Restart();
        AudioManager.Instance.PlaySound( shot_sounds );
    }

    private void Awake()
    {
        current_transform = transform;
        ConfigUI.on_pause_event += OnPause;
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

    private void OnPause( bool _is_pause )
    {
        if ( !gameObject.activeInHierarchy )
        {
            return;
        }

        if ( _is_pause )
        {
            destroy_timer.Stop();
        }
        else
        {
            destroy_timer.Start();
        }
    }
}
