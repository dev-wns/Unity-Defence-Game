using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float attackRange;
    public float attackDamage;
    public float attackDelay;
    
    // ������ ���� Ÿ��
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
            // �÷��̾�� �� �Ÿ����
            // EnemyList�� ���߿� GameManager ���鶧 �������� �ű� ����
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
