using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : Singleton<EnemyObjectPool>
{
    public Enemy prefabEnemy;

    // 사용중인 오브젝트 리스트
    [SerializeField]
    private Stack<Enemy> pool = new Stack<Enemy>();

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
        if ( pool.Count <= 0 )
        {
            Allocate();
        }

        Enemy enemy = pool.Pop();
        enemy.Initialize( 100000.0f, Random.Range( 110, 150 ) );
        enemy.transform.position = _pos;
        enemy.transform.parent = usePool.transform;
        enemy.gameObject.SetActive( true );
        GameManager.Instance.GetEnemies().AddLast( enemy );

        return enemy;
    }

    public void Despawn( Enemy _enemy )
    {
        _enemy.transform.position = new Vector3( 0, 1000, 0 );
        _enemy.transform.parent = waitPool.transform;
        _enemy.gameObject.SetActive( false );
        GameManager.Instance.GetEnemies().Remove( _enemy );
        pool.Push( _enemy );
    }
}
