using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : Singleton<EnemyObjectPool>
{
    public Enemy prefabEnemy;

    // 사용중인 오브젝트 리스트
    public List<Enemy> enemyUsePool = new List<Enemy>();
    // 대기중인 오브젝트 리스트
    public List<Enemy> enemyWaitPool = new List<Enemy>();

    // 적 생성 개수
    public int allocateCount;

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
            enemy.transform.parent = WaitPool.transform;
            enemy.gameObject.SetActive( false );
            enemyWaitPool.Add( enemy );
        }
    }

    public Enemy Spawn( Vector3 position )
    {
        // 리스트에 총알이 없다면 새로 생성
        if ( enemyWaitPool.Count <= 0 )
            Allocate();

        Enemy enemy = enemyWaitPool[0];
        enemy.transform.position = position;
        enemyUsePool.Add( enemy );
        enemyWaitPool.Remove( enemy );
        enemy.transform.parent = UsePool.transform;
        enemy.healthPoint = 100000.0f;
        enemy.gameObject.SetActive( true );

        return enemy;
    }

    public void Despawn( Enemy enemy )
    {
        enemy.transform.position = new Vector3( 0, 1000, 0 );
        enemyWaitPool.Add( enemy );
        enemyUsePool.Remove( enemy );
        enemy.transform.parent = WaitPool.transform;
        enemy.gameObject.SetActive( false );
    }
}
