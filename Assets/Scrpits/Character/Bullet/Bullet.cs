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

    private float timer;
    private float lifeTime;
    protected float range;
    [SerializeField]
    protected float duration;


    public void Initialize( Vector2 _position, Vector2 _direction )
    {
        this.direction = _direction;
        this.transform.position = _position;
        this.timer = 0.0f;
    }

    private void Awake()
    {
        this.speed = 2000.0f;
        this.timer = 0.0f;
        this.lifeTime = 1.7f;
        this.range = 300.0f;
        this.duration = 3.0f;
}

    private void Update()
    {
        this.timer += Time.deltaTime;

        if ( this.timer >= lifeTime )
        {
            isCrash = false;
            BulletObjectPool.Instance.Despawn( this );
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
            timer = 0.0f;
        }
    }

    public virtual void Ability( Enemy _target ) { }
}
