using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
#pragma warning disable CS0649 // 초기화 경고 무시
    [SerializeField]
    private List<AudioClip> dead_Sounds;
#pragma warning restore CS0649

    private enum EnemyState { Die, Walk, }
    [SerializeField]
    private EnemyState current_state;
    private Animator animator;

    private Slider hp_bar;
    private Dictionary<DebuffType, Debuff> debuffs = new Dictionary<DebuffType, Debuff>();
    private Vector2 spawn_position;

    [SerializeField]
    private float health;
    private float origin_health;

    [SerializeField]
    private float origin_armor;
    [SerializeField]
    private float armor;

    [SerializeField]
    private float origin_speed;
    [SerializeField]
    private float move_speed;

    public float damage { get; private set; }

    public delegate void PlaterHitEvent( Enemy _enemy );
    public static event PlaterHitEvent player_hit_event;

    public Transform current_transform { get; private set; }

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
        current_state = EnemyState.Walk;
        animator.SetInteger( "Enemy_State", ( int )current_state );

        short game_round = GameManager.Instance.game_round;
        current_transform.position = spawn_position;
        origin_health = health = ( 1.0f + ( game_round * 0.5f ) ) * 100.0f;
        damage = ( 1.0f + ( game_round * 0.37f ) ) * 10.0f;
        origin_armor = armor = 10.0f; // ( 1.0f + ( game_round * 0.14f ) ) * 3.0f;
        hp_bar.value = 1.0f;
        StartCoroutine( FSMRoutine() );
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
            hp_bar.value = health / origin_health;
        }
    }

    private void Awake()
    {
        hp_bar = GetComponentInChildren<Slider>();
        animator = GetComponent<Animator>();

        float width = 1080.0f;
        float height = 1920.0f;
        current_transform = transform;
        float spawn_width_range = ( width * 0.5f ) - ( current_transform.localScale.x * 0.5f );
        spawn_position = new Vector2( Random.Range( -spawn_width_range, spawn_width_range ), ( ( height * 0.5f ) + ( current_transform.localScale.y * 0.5f ) ) );

        origin_speed = move_speed = Random.Range( 50.0f, 200.0f );

        debuffs.Add( DebuffType.Stun, gameObject.AddComponent<Debuff>() );
        debuffs.Add( DebuffType.Slow, gameObject.AddComponent<Debuff>() );
        debuffs.Add( DebuffType.Curse, gameObject.AddComponent<Debuff>() );
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
            player_hit_event( this );
            OnDie();
        }
    }

    private IEnumerator FSMRoutine()
    {
        while ( true )
        {
            yield return StartCoroutine( current_state.ToString() );
        }
    }

    private IEnumerator Walk()
    {
        do
        {
            if ( health <= 0.0f )
            {
                yield return StartCoroutine( Die() );
            }

            // Decreased defense by curse
            float curse_amount = debuffs[DebuffType.Curse].amount;
            armor = origin_armor - curse_amount;

            // Speed reduction due to debuff
            float slow_percent = debuffs[DebuffType.Slow].amount;
            move_speed = origin_speed * ( 1.0f - ( slow_percent * 0.01f ) );

            // Limitation of movement speed by stun
            float stun_amount = debuffs[DebuffType.Stun].amount;
            if ( stun_amount > 0.0f )
            {
                move_speed = 0.0f;
            }

            current_transform.Translate( Vector2.down * move_speed * Time.deltaTime );
            yield return null;
        }
        while ( current_state.Equals( EnemyState.Walk ) );
    }

    private IEnumerator Die()
    {
        do
        {
            current_state = EnemyState.Die;
            animator.SetInteger( "Enemy_State", ( int )current_state );
            // 죽는 모션이 실행되는 시간
            yield return YieldCache.WaitForSeconds( 0.332f );
            OnDie();
        }
        while ( current_state.Equals( EnemyState.Die ) );
    }

    private void OnDie()
    {
        StopAllCoroutines();
        current_transform.position = spawn_position;
        debuffs[DebuffType.Slow].OnStop();
        debuffs[DebuffType.Stun].OnStop();
        debuffs[DebuffType.Curse].OnStop();
        AudioManager.Instance.PlaySound( dead_Sounds );
        EnemyObjectPool.Instance.Despawn( this );
    }
}
