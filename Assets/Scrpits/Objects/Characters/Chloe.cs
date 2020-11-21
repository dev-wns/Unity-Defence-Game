using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chloe : Player
{
    [SerializeField]
    private float curse_amount = 15.0f;
    [SerializeField]
    private float ability_range = 100.0f;
    [SerializeField]
    private float ability_duration = 3.0f;
    private Collider2D[] colliders_in_attack_range;
    private readonly short layer_mask = 1 << 8;

    public override void Ability( Vector2 _pos )
    {
        colliders_in_attack_range = Physics2D.OverlapCircleAll( _pos, ability_range, layer_mask );
        for ( int count = 0; count < colliders_in_attack_range.Length; count++ )
        {
            if ( colliders_in_attack_range[count].CompareTag( "Enemy" ) )
            {
                colliders_in_attack_range[count].GetComponent<Enemy>().SetDebuff( DebuffType.Curse, curse_amount, ability_duration );
            }
        }
    }
}
