using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float originSpeed;
    public float moveSpeed;
    public float healthPoint;
    public List<Debuff> debuffs = new List<Debuff>();

    private void Awake()
    {
        // debuffs.Add( new Slow() );
        originSpeed = Random.Range( 100, 250 );
        moveSpeed = originSpeed;
    }

    private void OnTriggerEnter2D( Collider2D collision )
{
    if ( collision.transform.CompareTag( "Bullet" ) )
        {
            healthPoint -= GameManager.Instance.playerDefaultDamage;
        }

        if ( collision.transform.CompareTag( "DeathLine" ) )
        {
            EnemyObjectPool.Instance.Despawn( this );
        }
    }

    private void Update()
    {
        foreach ( Debuff debuff in debuffs )
        {
            debuff.Apply( this );
        }
    
        if ( healthPoint <= 0.0f )
        {
            EnemyObjectPool.Instance.Despawn( this );
        }

        this.transform.Translate( new Vector3( 0.0f, -1.0f, 0.0f ) * moveSpeed * Time.deltaTime );
    }
}
