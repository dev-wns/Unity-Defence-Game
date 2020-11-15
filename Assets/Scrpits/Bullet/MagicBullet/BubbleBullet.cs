using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBullet : Bullet
{
    [SerializeField]
    private float slowPercent = 75.0f;

    public override void Ability( Enemy _target )
    {
        foreach ( Enemy enemy in GameManager.Instance.GetEnemies() )
        {
            if ( Vector2.Distance( _target.transform.position, enemy.transform.position ) <= range )
            {
                enemy.GetDebuff( DebuffType.Slow )?.Initialize( slowPercent, duration );
            }
        }
    }
}
