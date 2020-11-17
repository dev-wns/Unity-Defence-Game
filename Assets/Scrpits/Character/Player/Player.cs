using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Player : MonoBehaviour
{
    private readonly float delay_time_to_find_enemies = 0.1f;

    public Bullet prefab;
    public float damage { get; private set; }

    protected Enemy target { get; private set; }

    private float attack_range;
    private float attack_delay;

    private float critical_percent;
    private float critical_damage_increase;

    private float min_damage;
    private float max_damage;

    private Transform origin_transform;
    public Transform current_transform
    {
        get
        {
            return origin_transform;
        }
        set
        {
            if ( !ReferenceEquals( value, null ) ) 
            {
                origin_transform = value;
            }
        }
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

        attack_delay = 0.1f;// Random.Range( 0.1f, 1.0f );

        attack_range = 1500.0f;

        StartCoroutine( FindEnemy() );
        StartCoroutine( Idle() );
    }

    private IEnumerator FindEnemy()
    {
        while ( true )
        {
            if ( ReferenceEquals( target, null ) )
            {
                LinkedList<Enemy>.Enumerator enemy_enumerator = GameManager.Instance.enemy_enable_list.GetEnumerator();
                while ( enemy_enumerator.MoveNext() )
                {
                    if ( Vector2.Distance( current_transform.position, enemy_enumerator.Current.current_transform.position ) <= attack_range )
                    {
                        target = enemy_enumerator.Current;
                        break;
                    }
                }
            }
            else
            {
                if ( Vector2.Distance( current_transform.position, target.current_transform.position ) > attack_range )
                {
                    target = null;
                }
            }
            yield return YieldCache.WaitForSeconds( delay_time_to_find_enemies );
        }
    }

    private IEnumerator Idle()
    {
        damage = Random.Range( min_damage, max_damage );
        if ( Random.Range( ( int )1, ( int )101 ) <= critical_percent )
        {
            damage = damage + ( damage * ( critical_damage_increase * 0.01f ) );
        }

        yield return StartCoroutine( Attack() );
    }

    private IEnumerator Attack()
    {
        if ( target != null )
        {
            Bullet bullet = BulletObjectPool.Instance.Spawn( prefab );
            bullet.Initialize( this, current_transform.position, ( target.current_transform.position - current_transform.position ).normalized );
        }

        yield return YieldCache.WaitForSeconds( attack_delay );
        StartCoroutine( Idle() );
    }

    public virtual void Ability( Vector2 _pos )
    {

    }
}
