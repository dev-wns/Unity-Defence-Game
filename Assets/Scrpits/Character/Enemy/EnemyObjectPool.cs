using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : Singleton<EnemyObjectPool>
{
    // 복사될 프리팹
    public Enemy prefab;

    // 사용중인 오브젝트 리스트
    private Stack<Enemy> pool = new Stack<Enemy>();

    private int allocateCount;
    private int increaseCount;

    private GameObject usePool;
    private GameObject waitPool;

    private void Start()
    {
        usePool = new GameObject( typeof( Enemy ).ToString() + "UsePool" );
        usePool.transform.SetParent( this.transform );
        waitPool = new GameObject( typeof( Enemy ).ToString() + "WaitPool" );
        waitPool.transform.SetParent( this.transform );

        allocateCount = 10;
}

    private void Allocate()
    {
        for ( int count = 0; count < allocateCount; count++ )
        {
            Enemy enemy = Instantiate<Enemy>( prefab );
            enemy.name = prefab.name + increaseCount++.ToString();
            enemy.transform.SetParent( waitPool.transform );
            enemy.gameObject.SetActive( false );
            pool.Push( enemy );
        }
    }

    public Enemy Spawn( Vector2 _pos )
    {
        if ( pool.Count <= 0 )
        {
            Allocate();
        }

        Enemy enemy = pool.Pop();
        enemy.Initialize( 100000.0f, Random.Range( 110.0f, 150.0f ) );
        enemy.transform.position = _pos;
        enemy.transform.SetParent( usePool.transform );
        enemy.gameObject.SetActive( true );
        GameManager.Instance.GetEnemies().AddLast( enemy );
        return enemy;
    }

    public void Despawn( Enemy _enemy )
    {
        _enemy.transform.position = new Vector2( 0.0f, 1000.0f );
        _enemy.transform.SetParent( waitPool.transform );
        _enemy.gameObject.SetActive( false );
        GameManager.Instance.GetEnemies().Remove( _enemy );
        pool.Push( _enemy );
    }
}
