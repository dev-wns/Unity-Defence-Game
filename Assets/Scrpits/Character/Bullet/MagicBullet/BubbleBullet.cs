using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBullet : Bullet
{
    public override void Ability( Enemy _target )
    {
        foreach ( Enemy enemy in GameManager.Instance.GetEnemies() )
        {
            if ( Vector2.Distance( _target.transform.position, enemy.transform.position ) <= range )
            {
                enemy.GetDebuff( DebuffType.Slow )?.Initialize( 100.0f, 3.0f );
            }
        }
    }
}
