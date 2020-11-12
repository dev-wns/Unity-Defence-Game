using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet prefab;

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

    protected virtual void AttackToEnemy()
    {
        if ( target == null ) return;

        Vector3 direction = ( target.transform.position - this.transform.position ).normalized;
        BulletObjectPool.Instance.Spawn( prefab, this.transform.position, direction );
        
        target = null;
    }
}
