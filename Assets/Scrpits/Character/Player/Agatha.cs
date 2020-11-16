using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agatha : Player
{
    [SerializeField]
    private float slow_percent = 75.0f;
    [SerializeField]
    private float ability_range = 300.0f;
    [SerializeField]
    private float ability_duration = 3.0f;
    private Collider2D[] colliders_in_attack_range;

    protected override void Ability()
    {
        colliders_in_attack_range = Physics2D.OverlapCircleAll( target.transform.position, ability_range );
        foreach ( Collider2D enemy_collider in colliders_in_attack_range )
        {
            if ( enemy_collider.transform.CompareTag( "Enemy" ) )
            {
                enemy_collider.GetComponent<Enemy>().SetDebuff( DebuffType.Slow, slow_percent, ability_duration );
            }
        }
    }
}