using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    public float explosionDamage = 100.0f;

    public override void Ability( Enemy _target )
    {
        foreach ( Enemy enemy in GameManager.Instance.GetEnemies() )
        {
            if ( Vector2.Distance( _target.transform.position, enemy.transform.position ) <= range )
            {
                _target.TakeDamage( explosionDamage );
            }
        }
    }
}
