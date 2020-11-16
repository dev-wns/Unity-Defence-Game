using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : Singleton<EnemyObjectPool>
{
    // 복사될 프리팹
    public Enemy prefab;

    // 사용중인 오브젝트 리스트
    private Stack<Enemy> pool = new Stack<Enemy>();

    private int allocate_count;
    private int increase_count;

    private GameObject use_pool;
    private GameObject wait_pool;

    private void Start()
    {
        use_pool = new GameObject( typeof( Enemy ).ToString() + "UsePool" );
        use_pool.transform.SetParent( this.transform );
        wait_pool = new GameObject( typeof( Enemy ).ToString() + "WaitPool" );
        wait_pool.transform.SetParent( this.transform );

        allocate_count = 10;
}

    private void Allocate()
    {
        for ( int count = 0; count < allocate_count; count++ )
        {
            Enemy enemy = Instantiate<Enemy>( prefab );
            enemy.name = prefab.name + increase_count++.ToString();
            enemy.transform.SetParent( wait_pool.transform );
            enemy.gameObject.SetActive( false );
            pool.Push( enemy );
        }
    }

    public Enemy Spawn()
    {
        if ( pool.Count <= 0 )
        {
            Allocate();
        }

        Enemy enemy = pool.Pop();
        enemy.Initialize();
        enemy.transform.SetParent( use_pool.transform );
        enemy.gameObject.SetActive( true );
        GameManager.Instance.GetEnemies().AddLast( enemy );
        return enemy;
    }

    public void Despawn( Enemy _enemy )
    {
        _enemy.transform.SetParent( wait_pool.transform );
        _enemy.gameObject.SetActive( false );
        GameManager.Instance.GetEnemies().Remove( _enemy );
        pool.Push( _enemy );
    }
}
