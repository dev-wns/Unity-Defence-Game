using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float originSpeed;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float healthPoint;

    private List<Debuff> debuffs = new List<Debuff>();

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

    public void Initialize( float _hp, float _speed )
    {
        healthPoint = _hp;
        originSpeed = moveSpeed = _speed;
    }

    public void TakeDamage( float _damage )
    {
        if ( _damage >= 0.0f )
        {
            healthPoint -= _damage;
            DamageTextPool.Instance.Spawn( this.transform.position, _damage );
        }
    }

    private void Start()
    {
        originSpeed = Random.Range( 110.0f, 200.0f );
        moveSpeed = originSpeed;

        debuffs.Add( new Debuff( DebuffType.Slow ) );
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

        if ( healthPoint <= 0.0f )
        {
            OnDie();
        }

        float slowPercent = GetDebuff( DebuffType.Slow ).GetAmount();
        moveSpeed = originSpeed * ( 1.0f - ( slowPercent * 0.01f ) );

        this.transform.Translate( Vector2.down * moveSpeed * Time.deltaTime );
    }

    public void OnDie()
    {
        foreach( Debuff debuff in debuffs )
        {
            debuff.OnStop();
        }
        EnemyObjectPool.Instance.Despawn( this );
    }
}
