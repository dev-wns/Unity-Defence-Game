using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeReference]
    public Bullet prefab;

    private float attack_range;
    private float attack_delay;
    private bool is_attack;

    private Enemy target;

    private void Start()
    { 
        is_attack = true;
        attack_range = 1500.0f;
        attack_delay = 0.25f;// Random.Range( 0.1f, 1.0f );

        StartCoroutine( Attack() );
    }

    IEnumerator Attack()
    {
        while ( true )
        {
            if ( is_attack == true )
            {
                foreach ( Enemy enemy in GameManager.Instance.GetEnemies() )
                {
                    if ( Vector3.Distance( this.transform.position, enemy.transform.position ) <= attack_range )
                    {
                        target = enemy;
                        break;
                    }
                }

                AttackToEnemy();
            }
            yield return new WaitForSeconds( attack_delay );
        }
    }

    private void Update()
    {
        if ( Input.GetMouseButtonDown( 1 ) == true )
        {
            is_attack = !is_attack;
        }

        if ( is_attack == false )
        {
            return;
        }
    }

    protected virtual void AttackToEnemy()
    {
        if ( target == null )
        {
            return;
        }

        Bullet bullet = GameManager.Instance.bullet_object_pool.Spawn( prefab );
        // argument : player_position, direction
        bullet.Initialize( transform.position, ( target.transform.position - transform.position ).normalized );

        target = null;
    }
}
