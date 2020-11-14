using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float originSpeed;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float healthPoint;

    public IEnumerator Slow( float _amount, float _duration )
    {
        if ( originSpeed - _amount <= 0 )
        {
            yield return null;
        }

        moveSpeed = originSpeed - _amount;

        yield return new WaitForSeconds( _duration );

        moveSpeed = originSpeed;
    }

    public void Initialize( float _hp, float _speed )
    {
        healthPoint = _hp;
        originSpeed = moveSpeed = _speed;
    }

    public void TakeDamage( float _damage )
    {
        if ( _damage >= 0.0f )
            healthPoint -= _damage;
    }

    private void Awake()
    {
        originSpeed = Random.Range( 110, 200 );
        moveSpeed = originSpeed;
    }

    private void OnTriggerEnter2D( Collider2D _col )
    {
        if ( _col.transform.CompareTag( "Bullet" ) )
        {
            healthPoint -= GameManager.Instance.playerDefaultDamage;
            _col.GetComponent<Bullet>().Ability( this );
        }

        if ( _col.transform.CompareTag( "DeathLine" ) )
        {
            EnemyObjectPool.Instance.Despawn( this );
        }
    }

    private void Update()
    {
        if ( healthPoint <= 0.0f )
        {
            EnemyObjectPool.Instance.Despawn( this );
        }
        this.transform.Translate( Vector2.down * moveSpeed * Time.deltaTime );
    }
}
