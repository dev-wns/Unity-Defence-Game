using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBullet : Bullet
{
    public float attackRange = 100.0f;

    protected override void Attack()
    {
        foreach ( Enemy enemy in EnemyObjectPool.Instance.GetEnemyUsePool() )
        {
            if ( Vector3.Distance( this.transform.position, enemy.transform.position ) <= attackRange )
            {
                StartCoroutine( enemy.Slow( 100.0f, 3.0f ) );
            }
        }
    }
}
