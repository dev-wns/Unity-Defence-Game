using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float attackRange;
    public float attackDamage;
    public float attackDelay;
    
    // 범위에 들어온 타겟
    GameObject target;

    List<GameObject> enemyList;

    protected virtual IEnumerator Attack()
    {
        // 플레이어와 적 거리계산
        // EnemyList는 나중에 GameManager 만들때 그쪽으로 옮길 예정
        foreach ( GameObject enemy in enemyList )
        {
            if ( Vector2.Distance( this.transform.position, enemy.transform.position ) <= attackRange )
            {
                target = enemy;
                break;
            }
        }

        yield return new WaitForSeconds( attackDelay );
    }
}
