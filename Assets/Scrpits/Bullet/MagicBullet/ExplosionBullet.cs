using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public override void Ability( Enemy _target )
    {
        colliders = Physics2D.OverlapCircleAll( _target.transform.position, range );
        foreach ( Collider2D col in colliders )
        {
            if ( col.transform.CompareTag( "Enemy" ) )
            {
                col.GetComponent<Enemy>().TakeDamage( GameManager.Instance.GetDamage() );
            }
        }

        //foreach ( Enemy enemy in GameManager.Instance.GetEnemies() )
        //{
        //    if ( Vector2.Distance( _target.transform.position, enemy.transform.position ) <= range )
        //    {
        //        enemy.TakeDamage( GameManager.Instance.GetDamage() );
        //    }
        //}
    }
}
