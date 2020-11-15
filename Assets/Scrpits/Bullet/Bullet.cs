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
    private float lifeTime;
    [SerializeField]
    protected float range = 300.0f;
    [SerializeField]
    protected float duration;


    public void Initialize( Vector2 _position, Vector2 _direction )
    {
        this.direction = _direction;
        this.transform.position = _position;
        timer.Initialize( lifeTime );
    }

    private void Start()
    {
        this.speed = 2000.0f;
        this.lifeTime = 1.7f;
        this.duration = 3.0f;
    }

    private void Update()
    {
        if ( timer.Update() == false )
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
        }
    }

    public virtual void Ability( Enemy _target ) { }
}
