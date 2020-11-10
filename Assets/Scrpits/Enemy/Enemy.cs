using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float healthPoint;
    public Bullet hitBullet;

    private void Awake()
    {
        moveSpeed = Random.Range( 100, 250 );
        healthPoint = 100.0f;
    }

    private void OnTriggerEnter( Collider other )
    {
        if ( other.tag == "DeathLine")
        {
            EnemyObjectPool.Instance.Despawn( this );
        }

        if ( other.tag == "Bullet" )
        {
            hitBullet = other.GetComponentInParent<Bullet>();
            healthPoint -= hitBullet.damage;
            BulletObjectPool.Instance.Despawn( hitBullet );
        }
    }

    private void Update()
    {
        if ( healthPoint <= 0.0f )
            EnemyObjectPool.Instance.Despawn( this );

        this.transform.Translate( new Vector3( 0, -1, 0 ) * moveSpeed * Time.deltaTime );
    }
}
