using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : Singleton<EnemyObjectPool>
{
    public Enemy prefabEnemy;

    // ������� ������Ʈ ����Ʈ
    private List<Enemy> enemyUsePool = new List<Enemy>();
    // ������� ������Ʈ ����Ʈ
    private List<Enemy> enemyWaitPool = new List<Enemy>();

    // �� ���� ����
    private int allocateCount;

    private GameObject UsePool;
    private GameObject WaitPool;

    public List<Enemy> GetEnemyUsePool()
    {
        return enemyUsePool;
    }

    private void Awake()
    {
        UsePool = new GameObject( typeof( Enemy ).ToString() + "UsePool" );
        UsePool.transform.parent = this.transform;
        WaitPool = new GameObject( typeof( Enemy ).ToString() + "WaitPool" );
        WaitPool.transform.parent = this.transform;

        allocateCount = 10;
    }

    private void Start()
    {
        Allocate();
    }

    private void Allocate()
    {
        for ( int count = 0; count < allocateCount; count++ )
        {
            Enemy enemy = Instantiate<Enemy>( prefabEnemy );
            enemy.name = "Enemy_" + count.ToString();
            enemy.transform.parent = WaitPool.transform;
            enemy.gameObject.SetActive( false );
            enemyWaitPool.Add( enemy );
        }
    }

    public Enemy Spawn( Vector3 _pos )
    {
        // ����Ʈ�� �Ѿ��� ���ٸ� ���� ����
        if ( enemyWaitPool.Count <= 0 )
        {
            Allocate();
        }

        Enemy enemy = enemyWaitPool[0];
        enemy.Initialize( 100000.0f, Random.Range( 110, 150 ) );
        enemy.transform.position = _pos;
        enemy.transform.parent = UsePool.transform;
        enemyUsePool.Add( enemy );
        enemyWaitPool.Remove( enemy );
        enemy.gameObject.SetActive( true );

        return enemy;
    }

    public void Despawn( Enemy enemy )
    {
        enemy.transform.position = new Vector3( 0, 1000, 0 );
        enemy.transform.parent = WaitPool.transform;
        enemyWaitPool.Add( enemy );
        enemyUsePool.Remove( enemy );
        enemy.gameObject.SetActive( false );
    }
}
