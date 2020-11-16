using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBullet : Bullet
{
    [SerializeField]
    private float slow_percent = 75.0f;

    public override void Ability( Enemy _target )
    {
        colliders = Physics2D.OverlapCircleAll( _target.transform.position, range );
        foreach( Collider2D col in colliders )
        {
            if ( col.transform.CompareTag( "Enemy" ) )
            {
                col.GetComponent<Enemy>().GetDebuff( DebuffType.Slow )?.Initialize( slow_percent, duration );
            }
        }
        //foreach ( Enemy enemy in GameManager.Instance.GetEnemies() )
        //{
        //    if ( Vector2.Distance( _target.transform.position, enemy.transform.position ) <= range )
        //    {
        //        enemy.GetDebuff( DebuffType.Slow )?.Initialize( slowPercent, duration );
        //    }
        //}
    }
}
