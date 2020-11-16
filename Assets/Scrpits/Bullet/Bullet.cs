using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType { Default, Bubble, Dark, Explosion, Flame, Lightning }
public class Bullet : MonoBehaviour
{
    private bool isCrash;

    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 direction;

    private Timer timer = new Timer();
    private float life_time;
    [SerializeField]
    protected float range = 300.0f;
    [SerializeField]
    protected float duration;
    // 충돌 범위 내 적 확인용
    protected Collider2D[] colliders;

    public void Initialize( Vector2 spawn_pos, Vector2 _dir )
    {
        direction = _dir;
        transform.position = spawn_pos;
        timer.Initialize( life_time );
    }

    private void Start()
    {
        speed = 2000.0f;
        life_time = 1.7f;
        duration = 3.0f;
    }

    private void Update()
    {
        if ( timer.Update() == false )
        {
            isCrash = false;
            GameManager.Instance.bullet_object_pool.Despawn( this );
        }

        if ( isCrash == false )
        {
            this.transform.Translate( direction * speed * Time.deltaTime );
        }
    }

    private void OnTriggerEnter2D( Collider2D _other )
    {
        if ( _other.transform.CompareTag( "Enemy" ) == true )
        {
            isCrash = true;
        }
    }

    public virtual void Ability( Enemy _target ) { }
}
