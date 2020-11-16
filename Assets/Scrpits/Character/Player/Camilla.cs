using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camilla : Player
{
    [SerializeField]
    private float ability_range = 300.0f;
    [SerializeField]
    private float ability_duration;
    private Collider2D[] colliders_in_attack_range;

    protected override void Ability()
    {
        ability_duration = Random.Range( 0.05f, 0.45f );
        colliders_in_attack_range = Physics2D.OverlapCircleAll( target.transform.position, ability_range );
        foreach ( Collider2D enemy_collider in colliders_in_attack_range )
        {
            if ( enemy_collider.transform.CompareTag( "Enemy" ) )
            {
                enemy_collider.GetComponent<Enemy>().SetDebuff( DebuffType.Stun, ability_duration, ability_duration );
            }
        }
    }
}
