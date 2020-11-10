using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float attackRange;
    public float attackDamage;
    public float attackDelay;
    
    // ������ ���� Ÿ��
    GameObject target;

    List<GameObject> enemyList;

    protected virtual IEnumerator Attack()
    {
        // �÷��̾�� �� �Ÿ����
        // EnemyList�� ���߿� GameManager ���鶧 �������� �ű� ����
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
