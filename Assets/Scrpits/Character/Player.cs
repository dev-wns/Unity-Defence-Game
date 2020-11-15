using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeReference]
    public Bullet prefab;

    private float attackRange;
    private float attackDelay;
    private bool isAttack;

    private Enemy target;
    private float timer;

    private void Start()
    {
        isAttack = true;
        attackRange = 1500.0f;
        attackDelay = 0.1f;// Random.Range( 0.1f, 1.0f );

        StartCoroutine( Attack() );
    }

    IEnumerator Attack()
    {
        while ( true )
        {
            if ( isAttack == true )
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
            }
            yield return new WaitForSeconds( attackDelay );
        }
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
