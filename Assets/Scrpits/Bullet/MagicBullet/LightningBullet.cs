using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBullet : Bullet
{
    public override void Ability( Enemy _target )
    {
        duration = Random.Range( 0.05f, 0.45f );
        foreach ( Enemy enemy in GameManager.Instance.GetEnemies() )
        {
            if ( Vector2.Distance( _target.transform.position, enemy.transform.position ) <= range )
            {
                enemy.GetDebuff( DebuffType.Stun )?.Initialize( duration, duration );
            }
        }
    }
}
