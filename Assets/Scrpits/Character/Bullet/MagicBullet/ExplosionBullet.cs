using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public GameObject effect;

    public float explosionDamage = 100.0f;
    public float attackRange = 100.0f;

    protected override void Attack()
    {
        foreach ( Enemy enemy in EnemyObjectPool.Instance.GetEnemyUsePool() )
        {
            if ( Vector2.Distance( this.transform.position, enemy.transform.position ) <= attackRange )
            {
                enemy.healthPoint -= explosionDamage;
            }
        }
        StartCoroutine( Effect() );
    }

    private IEnumerator Effect()
    {
        if ( effect == null )
        {
            yield return null;
        }

        GameObject obj = Instantiate( effect );
        obj.transform.position = this.transform.position;
        yield return new WaitForSeconds( 3.0f );

        Destroy( obj );
    }
}
