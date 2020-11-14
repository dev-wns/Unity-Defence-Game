using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBullet : Bullet
{
    public float attackRange = 300.0f;

    protected override void Attack()
    {
        Vector3 pos = this.transform.position;

        foreach ( Enemy enemy in EnemyObjectPool.Instance.GetEnemyUsePool() )
        {
            Debug.Log( enemy.name + " : " + Vector2.Distance( pos, enemy.transform.position ) );
            if ( Vector2.Distance( pos, enemy.transform.position ) <= attackRange )
            {
                Slow slow = enemy.debuffs[0] as Slow;
                slow.Run( 100.0f, 3.0f );
            }
        }
    }
}
