using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageTextPool : Singleton<DamageTextPool>
{ 
    // ����� ������
    public DamageText prefab;

    // �Ҵ� �� ��ü Ǯ
    private Stack<DamageText> pool = new Stack<DamageText>();

    // Ǯ�� �� ������Ʈ�� �ڽĿ�����Ʈ�� ���� ������ �θ� ������Ʈ
    private PoolData use_pool;
    private PoolData wait_pool;
    // ������ ����
    private readonly byte allocate_count = 100;

    private void Start()
    {
        string name = typeof( DamageText ).ToString();
        Transform current_transform = transform;

        GameObject usepool_parent = new GameObject( name + "UsePool" );
        Transform usepool_transform = usepool_parent.transform;
        use_pool = new PoolData( usepool_parent, usepool_transform );
        use_pool.obj_transform.SetParent( current_transform );
        use_pool.obj_parent.isStatic = true;

        GameObject waitpool_parent = new GameObject( name + "WaitPool" );
        Transform waitpool_transform = waitpool_parent.transform;
        wait_pool = new PoolData( waitpool_parent, waitpool_transform );
        wait_pool.obj_transform.SetParent( current_transform );
        wait_pool.obj_parent.isStatic = true;

        Allocate();
    }

    public void Allocate()
    {
        for ( int count = 0; count < allocate_count; count++ )
        {
            DamageText obj = Instantiate<DamageText>( prefab );
            obj.current_transform.SetParent( wait_pool.obj_transform );
            obj.gameObject.SetActive( false );
            pool.Push( obj );
        }
    }

    public DamageText Spawn()
    {
        if ( pool.Count <= 0 )
        {
            Allocate();
        }

        DamageText obj = pool.Pop();
        obj.current_transform.SetParent( use_pool.obj_transform );
        obj.gameObject.SetActive( true );

        return obj;
    }

    public void Despawn( DamageText _obj )
    {
        _obj.current_transform.SetParent( wait_pool.obj_transform );
        _obj.gameObject.SetActive( false );

        pool.Push( _obj );
    }
}
