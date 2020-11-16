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
    private GameObject use_pool;
    private GameObject wait_pool;

    // 생성할 개수
    private int allocate_count;
    private int increase_count;

    private void Start()
    {
        use_pool = new GameObject( typeof( DamageText ).ToString() + "UsePool" );
        use_pool.transform.SetParent( this.transform );
        wait_pool = new GameObject( typeof( DamageText ).ToString() + "WaitPool" );
        wait_pool.transform.SetParent( this.transform );

        allocate_count = 100;
    }

    private void Allocate()
    {
        for ( int count = 0; count < allocate_count; count++ )
        {
            DamageText obj = Instantiate<DamageText>( prefab );
            obj.name = "obj_" + increase_count++.ToString();
            obj.transform.SetParent( wait_pool.transform );
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
        obj.transform.SetParent( use_pool.transform );
        obj.gameObject.SetActive( true );

        return obj;
    }

    public void Despawn( DamageText _obj )
    {
        _obj.transform.SetParent( wait_pool.transform );
        _obj.gameObject.SetActive( false );
        pool.Push( _obj );
    }
}
