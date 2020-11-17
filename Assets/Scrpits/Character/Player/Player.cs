using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    public Bullet prefab;
    //protected Collider2D target;
    protected Enemy target;

    private float attack_range;
    private float attack_delay;

    private float critical_percent;
    private float critical_damage_increase;

    private float min_damage;
    private float max_damage;
    private float damage;

    protected Transform current_transform;

    private WaitForSeconds attack_delay_seconds;

    public float GetDamage()
    {
        damage = Random.Range( min_damage, max_damage );
        if ( Random.Range( ( int )1, ( int )101 ) <= critical_percent )
        {
            damage = damage + ( damage * ( critical_damage_increase * 0.01f ) );
        }
        return damage;
    }

    private void Awake()
    {
        current_transform = transform;
    }

    private void Start()
    {
        min_damage = 1.0f;
        max_damage = 5.0f;

        critical_percent = 10.0f;
        critical_damage_increase = 100.0f;

        attack_delay = 0.25f;// Random.Range( 0.1f, 1.0f );
        attack_delay_seconds = new WaitForSeconds( attack_delay );

        attack_range = 1500.0f;
        StartCoroutine( Attack() );
    }

    private IEnumerator Attack()
    {
        while ( true )
        {
            //float min_distance = attack_range;
            //foreach ( Enemy enemy in GameManager.Instance.enemy_enable_list )
            //{
            //    float distance = Vector2.Distance( transform.position, enemy.transform.position );
            //    if ( Vector2.Distance( transform.position, enemy.transform.position ) <= min_distance )
            //    {
            //        min_distance = distance;
            //        target = enemy;
            //    }
            //}
            LinkedList<Enemy>.Enumerator enemy_enumerator = GameManager.Instance.enemy_enable_list.GetEnumerator();
            while ( enemy_enumerator.MoveNext() )
            {
                if ( Vector2.Distance( current_transform.position, enemy_enumerator.Current.current_transform.position ) <= attack_range )
                {
                    target = enemy_enumerator.Current;
                    break;
                }
            }

            if ( target != null )
            {
                Bullet bullet = BulletObjectPool.Instance.Spawn( prefab );
                bullet.Initialize( this, current_transform.position, ( target.current_transform.position - current_transform.position ).normalized );
                target = null;
            }

            yield return attack_delay_seconds;
        }
    }

    public virtual void Ability( Vector2 _pos )
    {

    }
}
