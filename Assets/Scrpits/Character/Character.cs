using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float attackRange;
    public float attackDamage;
    public float attackDelay;
    
    // 범위에 들어온 타겟
    public Enemy target;

    //public List<Enemy> enemyList = new List<Enemy>();

    private void Awake()
    {
        attackRange = 700.0f;
        attackDamage = 0.0f;
        attackDelay = Random.Range( 0.1f, 1.0f );
    }

    private void Start()
    {
        StartCoroutine( Attack() );
    }

    protected IEnumerator Attack()
    {
        while ( true )
        {
            // 플레이어와 적 거리계산
            // EnemyList는 나중에 GameManager 만들때 그쪽으로 옮길 예정
            foreach ( Enemy enemy in EnemyObjectPool.Instance.GetEnemyUsePool() )
            {
                if ( Vector2.Distance( this.transform.position, enemy.transform.position ) <= attackRange )
                {
                    target = enemy;
                    break;
                }
            }

            AttackToEnemy();

            yield return new WaitForSeconds( attackDelay );
        }
    }

    protected virtual void AttackToEnemy()
    {
        if ( target == null ) return;

        Vector3 direction = ( target.transform.position - this.transform.position ).normalized;
        BulletObjectPool.Instance.Spawn( 10.0f, this.transform.localPosition, direction );

        target = null;
    }
}
