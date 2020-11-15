using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBullet : Bullet
{
    [SerializeField]
    private float explosionDamage = 100.0f;

    public override void Ability( Enemy _target )
    {
        foreach ( Enemy enemy in GameManager.Instance.GetEnemies() )
        {
            if ( Vector2.Distance( _target.transform.position, enemy.transform.position ) <= range )
            {
                if ( ( range * 0.01f ) <= 0.0f )
                    _target.TakeDamage( explosionDamage * ( range * 0.01f ) );
            }
        }
    }
}
