using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    public Bullet prefab;
    protected Collider2D target;
    private float attack_range;
    private float attack_delay;
    private float critical_percent;
    private float critical_damage_increase;
    private Stopwatch attack_delay_timer = new Stopwatch();

    private void Start()
    {
        critical_percent = 10.0f;
        critical_damage_increase = 100.0f;
        attack_range = 1500.0f;
        attack_delay = 0.1f;// Random.Range( 0.1f, 1.0f );
        attack_delay_timer.Start();
    }

    private void Update()
    {
        // Attack
        if ( attack_delay_timer.ElapsedMilliseconds >= attack_delay * 1000.0f )
        {
            target = Physics2D.OverlapCircle( transform.position, attack_range );
            if ( target != null && target.transform.CompareTag( "Enemy" ) == true )
            {
                Bullet bullet = BulletObjectPool.Instance.Spawn( prefab );
                // argument : player_position, direction
                bullet.Initialize( transform.position, ( target.transform.position - transform.position ).normalized );
                Ability();
                attack_delay_timer.Restart();
                target = null;
            }
        }
    }

    protected virtual void Ability() { }
}
