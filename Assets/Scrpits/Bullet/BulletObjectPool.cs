using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : Singleton<BulletObjectPool>
{
    // ĳ���� ���� �Ѿ��� �ٸ��� ������
    // �Ѿ� �������� �÷��̾��ʿ��� �������.

    // �Ҵ� �� ��ü Ǯ
    private Dictionary<string, Stack<Bullet>> pool = new Dictionary<string, Stack<Bullet>>();

    // Ǯ�� �� ������Ʈ�� �ڽĿ�����Ʈ�� ���� ������ �θ� ������Ʈ
    private Dictionary<string, PoolData> use_pool = new Dictionary<string, PoolData>();
    private Dictionary<string, PoolData> wait_pool = new Dictionary<string, PoolData>();

    // ������ ����
    private int allocate_count;
    private int increase_count;

    private Transform current_transform;

    private void Awake()
    {
        current_transform = transform;
    }

    private void Start()
    {
        allocate_count = 10;
    }

    private void Allocate( Bullet _prefab )
    {
        string name = _prefab.GetType().Name;

        if ( pool.ContainsKey( name ) == false )
        {
            pool.Add( name, new Stack<Bullet>() );

            GameObject usepool_parent = new GameObject( name + "UsePool" );
            Transform usepool_transform = usepool_parent.transform;
            use_pool.Add( name, new PoolData( usepool_parent, usepool_transform  ) );
            use_pool[name].obj_transform.SetParent( current_transform );

            GameObject waitpool_parent = new GameObject( name + "WaitPool" );
            Transform waitpool_transform = usepool_parent.transform;
            wait_pool.Add( name, new PoolData( waitpool_parent, waitpool_transform ) );
            wait_pool[name].obj_transform.SetParent( current_transform );
        }

        for ( int count = 0; count < allocate_count; count++ )
        {
            Bullet bullet = Instantiate<Bullet>( _prefab );
            bullet.current_transform.SetParent( wait_pool[name].obj_transform );
            bullet.gameObject.SetActive( false );
            pool[name].Push( bullet );
        }
    }

    public Bullet Spawn( Bullet _bullet )
    {
        string name = _bullet.GetType().Name;
        // ����Ʈ�� �Ѿ��� ���ٸ� ���� ����
        if ( pool.ContainsKey( name ) == false || pool[name].Count <= 0 )
        {
            Allocate( _bullet );
        }

        Bullet bullet = pool[name].Pop();
        bullet.current_transform.SetParent( use_pool[name].obj_transform );
        bullet.gameObject.SetActive( true );

        return bullet;
    }

    public void Despawn( Bullet _bullet )
    {
        string name = _bullet.GetType().Name;
        _bullet.current_transform.position = new Vector3( 0.0f, 10000.0f, -1.0f );
        _bullet.current_transform.SetParent( wait_pool[name].obj_transform );
        _bullet.gameObject.SetActive( false );
        pool[name].Push( _bullet );
    }
}
