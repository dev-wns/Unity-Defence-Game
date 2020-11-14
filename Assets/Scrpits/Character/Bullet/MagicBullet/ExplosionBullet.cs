using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public GameObject effect;

    public float explosionDamage = 100.0f;
    public float attackRange = 100.0f;

    //public override void Ability( Enemy _target )
    //{
    //    // cols = Physics.OverlapCircleAll( transform.position, attackRange );
    //    // foreach ( Collider hit in cols )
    //    // {
    //    //     if ( hit.CompareTag( "Enemy" ) == true )
    //    //     {
    //    //          hit.GetComponent<Enemy>().debuff += Explosion;
    //    //         Slow slow = hit.GetComponent<Enemy>().debuffs[0] as Slow;
    //    //          slow.Run( 100.0f, 3.0f );
    //    //     }
    //    // }
    //}

    public void Explosion( Enemy _target )
    {
        _target.TakeDamage( explosionDamage );
    }
}
