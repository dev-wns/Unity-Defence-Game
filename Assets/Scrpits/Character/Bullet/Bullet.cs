using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;
    public float timer;
    public float lifeTime;
    public Vector3 direction;

    public bool isCrash;

    private void Awake()
    {
        speed = 2000.0f;
        timer = 0.0f;
        lifeTime = 1.5f;
    }

    private void Update()
    {
        this.timer += Time.deltaTime;

        if ( this.timer >= lifeTime )
        {
            BulletObjectPool.Instance.Despawn( this );
            isCrash = false;
        }
        if ( isCrash == false )
            this.transform.Translate( direction * speed * Time.deltaTime );
    }

    //private void OnCollisionEnter( Collision collision )
    //{
    //    if ( collision.transform.CompareTag( "Enemy" ) == true )
    //    {
    //        isCrash = true;
    //        timer = 0.0f;
    //    }
    //}

    private void OnTriggerEnter( Collider other )
    {
        if ( other.transform.CompareTag( "Enemy" ) == true )
        {
            isCrash = true;
            timer = 0.0f;
        }
    }

    public void SetBullet( float _damage, Vector3 _position, Vector3 _direction )
    {
        this.damage = _damage;
        this.direction = _direction;
        this.transform.position = _position;
        this.timer = 0.0f;
    }
}
