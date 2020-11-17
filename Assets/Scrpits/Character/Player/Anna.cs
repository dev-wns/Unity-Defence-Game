using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anna : Player
{
    [SerializeField]
    private float ability_range = 100.0f;
    private Collider2D[] colliders_in_attack_range;

    public override void Ability( Vector2 _pos )
    {
        colliders_in_attack_range = Physics2D.OverlapCircleAll( _pos, ability_range );
        for ( int count = 0; count < colliders_in_attack_range.Length; count++ )
        {
            if ( colliders_in_attack_range[count]?.CompareTag( "Enemy" ) == true )
            {
                colliders_in_attack_range[count].GetComponent<Enemy>().TakeDamage( GetDamage() );
            }
        }
    }
}
