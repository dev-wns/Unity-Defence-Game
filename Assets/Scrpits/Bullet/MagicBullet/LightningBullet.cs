using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBullet : Bullet
{
    public override void Ability( Enemy _target )
    {
        duration = Random.Range( 0.05f, 0.45f );
        colliders = Physics2D.OverlapCircleAll( _target.transform.position, range );
        foreach ( Collider2D col in colliders )
        {
            if ( col.transform.CompareTag( "Enemy" ) )
            {
                col.GetComponent<Enemy>().GetDebuff( DebuffType.Stun )?.Initialize( duration, duration );
            }
        }
        //foreach ( Enemy enemy in GameManager.Instance.GetEnemies() )
        //{
        //    if ( Vector2.Distance( _target.transform.position, enemy.transform.position ) <= range )
        //    {
        //        enemy.GetDebuff( DebuffType.Stun )?.Initialize( duration, duration );
        //    }
        //}
    }
}
