using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : Singleton<EnemyObjectPool>
{
    // ����� ������
    public Enemy prefab;

    // ������� ������Ʈ ����Ʈ
    private Stack<Enemy> pool = new Stack<Enemy>();

    // Ǯ�� �� ������Ʈ�� �ڽĿ�����Ʈ�� ���� ������ �θ� ������Ʈ
    private PoolData use_pool;
    private PoolData wait_pool;

    private int allocate_count;
    private int increase_count;

    private void Start()
    {
        string name = typeof( Enemy ).ToString();

        GameObject usepool_parent = new GameObject( name + "UsePool" );
        Transform usepool_transform = usepool_parent.transform;
        use_pool = new PoolData( usepool_parent, usepool_transform );
        use_pool.obj_transform.SetParent( transform );

        GameObject waitpool_parent = new GameObject( name + "WaitPool" );
        Transform waitpool_transform = usepool_parent.transform;
        wait_pool = new PoolData( waitpool_parent, waitpool_transform );
        wait_pool.obj_transform.SetParent( transform );

        allocate_count = 50;
        Allocate();
    }

    private void Allocate()
    {
        for ( int count = 0; count < allocate_count; count++ )
        {
            Enemy enemy = Instantiate<Enemy>( prefab );
            enemy.current_transform.SetParent( wait_pool.obj_transform );
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
        enemy.current_transform.SetParent( use_pool.obj_transform );
        enemy.gameObject.SetActive( true );
        GameManager.Instance.PushEnemy( enemy );
        return enemy;
    }

    public void Despawn( Enemy _enemy )
    {
        _enemy.current_transform.position = new Vector3( 0.0f, 10000.0f, -1.0f );
        _enemy.current_transform.SetParent( wait_pool.obj_transform );
        _enemy.gameObject.SetActive( false );
        GameManager.Instance.PopEnemy( _enemy );
        pool.Push( _enemy );
    }
}
