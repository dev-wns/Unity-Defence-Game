using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public override void Ability( Enemy _target )
    {
        foreach ( Enemy enemy in GameManager.Instance.GetEnemies() )
        {
            if ( Vector2.Distance( _target.transform.position, enemy.transform.position ) <= range )
            {
                enemy.TakeDamage( GameManager.Instance.GetDamage() );
            }
        }
    }
}
