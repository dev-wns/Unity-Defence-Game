using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageTextPool : Singleton<DamageTextPool>
{ 
    // 복사될 프리팹
    public DamageText prefab;

    // 할당 된 전체 풀
    private Stack<DamageText> pool = new Stack<DamageText>();

    // 풀링 된 오브젝트를 자식오브젝트로 연결 시켜줄 부모 오브젝트
    private PoolData use_pool;
    private PoolData wait_pool;
    // 생성할 개수
    private int allocate_count;
    private int increase_count;

    private void Start()
    {
        string name = typeof( DamageText ).ToString();
        Transform current_transform = transform;

        GameObject usepool_parent = new GameObject( name + "UsePool" );
        Transform usepool_transform = usepool_parent.transform;
        use_pool = new PoolData( usepool_parent, usepool_transform );
        use_pool.obj_transform.SetParent( current_transform );

        GameObject waitpool_parent = new GameObject( name + "WaitPool" );
        Transform waitpool_transform = usepool_parent.transform;
        wait_pool = new PoolData( waitpool_parent, waitpool_transform );
        wait_pool.obj_transform.SetParent( current_transform );

        allocate_count = 100;
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
        Debug.Log( "DamageText Object Count : " + increase_count );
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
        _obj.current_transform.position = new Vector3( 0.0f, 10000.0f, -1.0f );
        _obj.current_transform.SetParent( wait_pool.obj_transform );
        _obj.gameObject.SetActive( false );
        pool.Push( _obj );
    }
}
