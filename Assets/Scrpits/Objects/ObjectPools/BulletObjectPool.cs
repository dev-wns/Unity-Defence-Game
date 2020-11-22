using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : Singleton<BulletObjectPool>
{
    // �Ҵ� �� ��ü Ǯ
    private Dictionary<string, Stack<Bullet>> pool = new Dictionary<string, Stack<Bullet>>();

    // Ǯ�� �� ������Ʈ�� �ڽĿ�����Ʈ�� ���� ������ �θ� ������Ʈ
    private Dictionary<string, PoolData> use_pool = new Dictionary<string, PoolData>();
    private Dictionary<string, PoolData> wait_pool = new Dictionary<string, PoolData>();

    // ������ ����
    private readonly byte allocate_count = 10;
    public Transform current_transform { get; set; }

    private void Awake()
    {
        current_transform = transform;
    }

    private void Allocate( Bullet _prefab )
    {
        string name = _prefab.name;

        if ( !pool.ContainsKey( name ) )
        {
            pool.Add( name, new Stack<Bullet>() );

            GameObject usepool_parent = new GameObject( name + "UsePool" );
            Transform usepool_transform = usepool_parent.transform;
            use_pool.Add( name, new PoolData( usepool_parent, usepool_transform  ) );
            use_pool[name].obj_transform.SetParent( current_transform );
            use_pool[name].obj_parent.isStatic = true;

            GameObject waitpool_parent = new GameObject( name + "WaitPool" );
            Transform waitpool_transform = waitpool_parent.transform;
            wait_pool.Add( name, new PoolData( waitpool_parent, waitpool_transform ) );
            wait_pool[name].obj_transform.SetParent( current_transform );
            wait_pool[name].obj_parent.isStatic = true;
        }

        for ( int count = 0; count < allocate_count; count++ )
        {
            Bullet bullet = Instantiate<Bullet>( _prefab );
            bullet.name = name;
            bullet.current_transform.SetParent( wait_pool[name].obj_transform );
            bullet.gameObject.SetActive( false );
            pool[name].Push( bullet );
        }
    }

    public Bullet Spawn( Bullet _bullet )
    {
        string name = _bullet.name;
        if ( !pool.ContainsKey( name ) || pool[name].Count <= 0 )
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
        string name = _bullet.name;
        _bullet.current_transform.SetParent( wait_pool[name].obj_transform );
        _bullet.gameObject.SetActive( false );

        pool[name].Push( _bullet );
    }
}
