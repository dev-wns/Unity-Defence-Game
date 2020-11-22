using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : Singleton<EnemyObjectPool>
{
    // 복사될 프리팹
    public Enemy prefab;

    // 사용중인 오브젝트 리스트
    private Stack<Enemy> pool = new Stack<Enemy>();

    // 풀링된 오브젝트를 자식오브젝트로 연결 시켜줄 부모 오브젝트
    private PoolData use_pool;
    private PoolData wait_pool;

    private readonly byte allocate_count = 50;

    private void Start()
    {
        string name = typeof( Enemy ).ToString();

        GameObject usepool_parent = new GameObject( name + "UsePool" );
        Transform usepool_transform = usepool_parent.transform;
        use_pool = new PoolData( usepool_parent, usepool_transform );
        use_pool.obj_transform.SetParent( transform );
        use_pool.obj_parent.isStatic = true;

        GameObject waitpool_parent = new GameObject( name + "WaitPool" );
        Transform waitpool_transform = waitpool_parent.transform;
        wait_pool = new PoolData( waitpool_parent, waitpool_transform );
        wait_pool.obj_transform.SetParent( transform );
        wait_pool.obj_parent.isStatic = true;

        Allocate();
    }

    private void Allocate()
    {
        for ( int count = 0; count < allocate_count; count++ )
        {
            Enemy enemy = Instantiate<Enemy>( prefab );
            enemy.current_transform.SetParent( wait_pool.obj_transform );
            enemy.gameObject.SetActive( false );
            enemy.gameObject.isStatic = true;

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
        enemy.current_transform.SetParent( use_pool.obj_transform );
        enemy.gameObject.SetActive( true );
        enemy.Initialize();
        enemy.gameObject.isStatic = false;
        GameManager.Instance.enemy_enable_list.AddLast( enemy );

        return enemy;
    }

    public void Despawn( Enemy _enemy )
    {
        _enemy.current_transform.SetParent( wait_pool.obj_transform );
        _enemy.gameObject.SetActive( false );
        _enemy.gameObject.isStatic = true;
        GameManager.Instance.enemy_enable_list.Remove( _enemy );

        pool.Push( _enemy );
    }
}
