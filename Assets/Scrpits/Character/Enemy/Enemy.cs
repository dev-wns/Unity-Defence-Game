using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Dictionary<DebuffType, Debuff> debuffs = new Dictionary<DebuffType, Debuff>();
    private Vector2 spawn_position;
    [SerializeField]
    private float health;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float origin_armor;
    [SerializeField]
    private float armor;
    [SerializeField]
    private float origin_speed;
    [SerializeField]
    private float move_speed;

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

    public void SetDebuff( DebuffType _type, float _amount, float _duration )
    {
        if ( !ReferenceEquals( debuffs, null ) && debuffs[_type].isApplied.Equals( false ) )
        {
            debuffs[_type].Initialize( _amount, _duration );
        }
        else
        {
            debuffs[_type].Restart();
        }
    }

    public void Initialize()
    {
        short game_round = GameManager.Instance.game_round;
        current_transform.position = spawn_position;
        health = ( 1.0f + ( game_round * 0.5f ) ) * 100.0f;
        damage = ( 1.0f + ( game_round * 0.37f ) ) * 10.0f;
        origin_armor = armor = 10.0f; // ( 1.0f + ( game_round * 0.14f ) ) * 3.0f;
    }

    public void TakeDamage( float _damage )
    {
        if ( _damage >= 0.0f )
        {
            float final_damage = _damage;
            if ( armor > 0 )
            {
                final_damage = _damage / armor;
            }
            health -= final_damage;
            DamageTextPool.Instance.Spawn().Initialize( current_transform.position, ( int )final_damage );
        }
    }

    private void Awake()
    {
        current_transform = transform;

        float spawn_width_range = ( Screen.width * 0.5f ) - ( current_transform.localScale.x * 0.5f );
        spawn_position = new Vector2( Random.Range( -spawn_width_range, spawn_width_range ), ( ( Screen.height * 0.5f ) + ( current_transform.localScale.y * 0.5f ) ) );
    }

    private void Start()
    {
        origin_speed = move_speed = Random.Range( 50.0f, 200.0f );

        debuffs.Add( DebuffType.Slow, new Debuff() );
        debuffs.Add( DebuffType.Stun, new Debuff() );
        debuffs.Add( DebuffType.Curse, new Debuff() );
    }

    private void OnTriggerEnter2D( Collider2D _col )
    {
        if ( _col.CompareTag( "Bullet" ) )
        {
            Bullet bullet = _col.GetComponent<Bullet>();
            TakeDamage( bullet.owner.damage );
            bullet.owner.Ability( current_transform.position );
            bullet.OnDie();
        }

        if ( _col.CompareTag( "DeathLine" ) )
        {
            OnDie();
        }
    }

    private void Update()
    {
        if ( health <= 0.0f )
        {
            OnDie();
        }

        // Decreased defense by curse
        float curse_amount = debuffs[DebuffType.Curse].GetAmountAndUpdate();
        armor = origin_armor - curse_amount;

        // Speed reduction due to debuff
        float slow_percent = debuffs[DebuffType.Slow].GetAmountAndUpdate();
        move_speed = origin_speed * ( 1.0f - ( slow_percent * 0.01f ) );

        // Limitation of movement speed by stun
        float stun_amount = debuffs[DebuffType.Stun].GetAmountAndUpdate();
        if ( stun_amount > 0.0f )
        {
            move_speed = 0.0f;
        }

        current_transform.Translate( Vector2.down * move_speed * Time.deltaTime );
    }

    private void OnDie()
    {
        debuffs[DebuffType.Slow].OnStop();
        debuffs[DebuffType.Stun].OnStop();
        debuffs[DebuffType.Curse].OnStop();
        EnemyObjectPool.Instance.Despawn( this );
    }
}
