using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float attackRange;
    public float attackDelay;
    
    // 범위에 들어온 타겟
    public Enemy target;

    public float timer;

    private void Awake()
    {
        attackRange = 1500.0f;
        attackDelay = Random.Range( 0.1f, 1.0f );
    }

    private void Start()
    {
        // StartCoroutine( Attack() );
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if ( timer >= attackDelay )
        {
            foreach ( Enemy enemy in EnemyObjectPool.Instance.GetEnemyUsePool() )
            {
                if ( Vector3.Distance( this.transform.position, enemy.transform.position ) <= attackRange )
                {
                    target = enemy;
                    break;
                }
            }

            AttackToEnemy();
            timer = 0.0f;
        }
    }
    //protected IEnumerator Attack()
    //{
    //    while ( true )
    //    {
    //        // 플레이어와 적 거리계산
    //        // EnemyList는 나중에 GameManager 만들때 그쪽으로 옮길 예정
    //        foreach ( Enemy enemy in EnemyObjectPool.Instance.GetEnemyUsePool() )
    //        {
    //            if ( Vector3.Distance( this.transform.position, enemy.transform.position ) <= attackRange )
    //            {
    //                target = enemy;
    //                break;
    //            }
    //        }

    //        AttackToEnemy();

    //        yield return new WaitForSeconds( attackDelay );
    //    }
    //}

    protected virtual void AttackToEnemy()
    {
        if ( target == null ) return;

        Vector3 direction = ( target.transform.position - this.transform.position ).normalized;
        Bullet bullet = BulletObjectPool.Instance.Spawn( 10.0f, this.transform.position, direction );
        target = null;
    }
}
