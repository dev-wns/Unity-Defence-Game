using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : Singleton<EnemyObjectPool>
{
    public Enemy prefabEnemy;

    // ������� ������Ʈ ����Ʈ
    private Stack<Enemy> pool = new Stack<Enemy>();
    // ������� ������Ʈ ����Ʈ
    // private Stack<Enemy> enemyWaitPool = new Stack<Enemy>();

    // �� ���� ����
    private int allocateCount;

    private GameObject usePool;
    private GameObject waitPool;

    private void Awake()
    {
        usePool = new GameObject( typeof( Enemy ).ToString() + "UsePool" );
        usePool.transform.parent = this.transform;
        waitPool = new GameObject( typeof( Enemy ).ToString() + "WaitPool" );
        waitPool.transform.parent = this.transform;

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
            enemy.transform.parent = waitPool.transform;
            enemy.gameObject.SetActive( false );
            pool.Push( enemy );
        }
    }

    public Enemy Spawn( Vector3 _pos )
    {
        // ����Ʈ�� �Ѿ��� ���ٸ� ���� ����
        if ( pool.Count <= 0 )
        {
            Allocate();
        }

        Enemy enemy = pool.Pop();
        enemy.Initialize( 100000.0f, Random.Range( 110, 150 ) );
        enemy.transform.position = _pos;
        enemy.transform.parent = usePool.transform;
        enemy.gameObject.SetActive( true );

        return enemy;
    }

    public void Despawn( Enemy enemy )
    {
        enemy.transform.position = new Vector3( 0, 1000, 0 );
        enemy.transform.parent = waitPool.transform;
        enemy.gameObject.SetActive( false );
        pool.Push( enemy );
    }
}
