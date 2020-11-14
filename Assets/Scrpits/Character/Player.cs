using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet prefab;

    private float attackRange;
    private float attackDelay;
    private bool isAttack;

    // 범위에 들어온 타겟
    private Enemy target;

    private float timer;

    private void Awake()
    {
        isAttack = true;
        attackRange = 1500.0f;
        attackDelay = Random.Range( 0.1f, 1.0f );
    }

    private void Update()
    {
        if ( Input.GetMouseButtonDown( 1 ) == true )
        {
            isAttack = !isAttack;
        }

        if ( isAttack == false )
        {
            return;
        }

        timer += Time.deltaTime;
        if ( timer >= attackDelay )
        {
            foreach ( Enemy enemy in GameManager.Instance.GetEnemies() )
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
        if ( target == null )
        {
            return;
        }

        Vector2 trans = this.transform.position;
        Vector2 direction = ( target.transform.position - this.transform.position ).normalized;
        BulletObjectPool.Instance.Spawn( prefab, trans, direction );
        
        target = null;
    }
}
