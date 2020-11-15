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

    List<Debuff> debuffs = new List<Debuff>();

    public GameObject hudDamageTextPrefab;
    public GameObject damageTextParent;

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
            GameObject hudText = Instantiate( hudDamageTextPrefab ); // 생성할 텍스트 오브젝트
            hudText.transform.SetParent( damageTextParent.transform );

            Vector2 pos = new Vector2( this.transform.position.x + 540 - Random.Range( -50.0f, 50.0f ), this.transform.position.y + ( this.transform.localScale.y * 0.5f ) + 960 );
            hudText.transform.position = pos; // 표시될 위치
            hudText.GetComponent<DamageText>().damage = ( int )_damage; // 데미지 전달
        }
    }

    private void Awake()
    {
        originSpeed = Random.Range( 110, 200 );
        moveSpeed = originSpeed;

        debuffs.Add( new Debuff( DebuffType.Slow ) );
    }

    private void Start()
    {
        damageTextParent = GameObject.FindGameObjectWithTag( "DamageText" );
    }

    private void OnTriggerEnter2D( Collider2D _col )
    {
        if ( _col.transform.CompareTag( "Bullet" ) )
        {
            TakeDamage( GameManager.Instance.playerDefaultDamage );
            _col.GetComponent<Bullet>().Ability( this );
        }

        if ( _col.transform.CompareTag( "DeathLine" ) )
        {
            EnemyObjectPool.Instance.Despawn( this );
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
            EnemyObjectPool.Instance.Despawn( this );
        }

        float slowPercent = GetDebuff( DebuffType.Slow ).GetAmount();
        moveSpeed = originSpeed * ( 1.0f - ( slowPercent * 0.01f ) );

        this.transform.Translate( Vector2.down * moveSpeed * Time.deltaTime );
    }
}
