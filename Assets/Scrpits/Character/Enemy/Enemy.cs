using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private List<Debuff> debuffs = new List<Debuff>();
    private Vector2 spawn_position;
    [SerializeField]
    private float health;
    [SerializeField]
    private float armor;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float origin_speed;
    [SerializeField]
    private float move_speed;

    public Debuff GetDebuff( DebuffType _type )
    {
        foreach( Debuff debuff in debuffs )
        {
            if ( debuff.GetDebuffType() == _type )
            {
                return debuff;
            }
        }
        return null;
    }

    public void Initialize()
    {
        short game_round = GameManager.Instance.GetRound();
        transform.position = spawn_position;
        health = ( 1.0f + ( game_round * 0.5f ) ) * 100.0f;
        damage = ( 1.0f + ( game_round * 0.37f ) ) * 10.0f;
        armor = ( 1.0f + ( game_round * 0.14f ) ) * 3.0f;
    }

    public void TakeDamage( float _damage )
    {
        if ( _damage >= 0.0f )
        {
            float final_damage = _damage;
            if ( armor > 0 )
            {
                final_damage = _damage * ( 1.0f - ( armor / _damage * 0.01f ) );
            }
            health -= final_damage;
            DamageText damage_ui = GameManager.Instance.damage_text_pool.Spawn();
            damage_ui.Initialize( transform.position, ( int )final_damage );
        }
    }

    private void Awake()
    {
        spawn_position = new Vector2( Random.Range( -500.0f, 500.0f ), 1060.0f );
    }

    private void Start()
    {
        origin_speed = move_speed = Random.Range( 50.0f, 200.0f );

        debuffs.Add( new Debuff( DebuffType.Slow ) );
        debuffs.Add( new Debuff( DebuffType.Stun ) );
    }

    private void OnTriggerEnter2D( Collider2D _col )
    {
        if ( _col.transform.CompareTag( "Bullet" ) )
        {
            TakeDamage( GameManager.Instance.GetDamage() );
            _col.GetComponent<Bullet>().Ability( this );
        }

        if ( _col.transform.CompareTag( "DeathLine" ) )
        {
            OnDie();
        }
    }

    private void Update()
    {
        foreach ( Debuff debuff in debuffs )
        {
            debuff.Update();
        }

        if ( health <= 0.0f )
        {
            OnDie();
        }

        // Speed reduction due to debuff
        float slowPercent = GetDebuff( DebuffType.Slow ).GetAmount();
        move_speed = origin_speed * ( 1.0f - ( slowPercent * 0.01f ) );

        float stunAmount = GetDebuff( DebuffType.Stun ).GetAmount();
        if ( stunAmount > 0.0f )
        {
            move_speed = 0.0f;
        }

        this.transform.Translate( Vector2.down * move_speed * Time.deltaTime );
    }

    private void OnDie()
    {
        foreach( Debuff debuff in debuffs )
        {
            debuff.OnStop();
        }
        GameManager.Instance.enemy_object_pool.Despawn( this );
    }
}
