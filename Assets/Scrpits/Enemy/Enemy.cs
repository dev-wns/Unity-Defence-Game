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

    private void OnCollisionEnter( Collision collision )
    {
        if ( collision.transform.CompareTag( "Bullet" ) )
        {
            healthPoint -= GameManager.Instance.playerDefaultDamage;
        }
    }

    private void Update()
    {
        if ( healthPoint <= 0.0f )
            EnemyObjectPool.Instance.Despawn( this );

        this.transform.Translate( new Vector3( 0, -1, 0 ) * moveSpeed * Time.deltaTime );
    }
}
