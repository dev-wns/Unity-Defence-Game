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
    private GameObject usePool;
    private GameObject waitPool;

    // 생성할 개수
    private int allocateCount;
    private int increaseCount;

    private void Start()
    {
        usePool = new GameObject( typeof( DamageText ).ToString() + "UsePool" );
        usePool.transform.SetParent( this.transform );
        waitPool = new GameObject( typeof( DamageText ).ToString() + "WaitPool" );
        waitPool.transform.SetParent( this.transform );

        allocateCount = 10;
    }

    private void Allocate()
    {
        for ( int count = 0; count < allocateCount; count++ )
        {
            DamageText obj = Instantiate<DamageText>( prefab );
            obj.name = "obj_" + increaseCount++.ToString();
            obj.transform.SetParent( waitPool.transform );
            obj.gameObject.SetActive( false );
            pool.Push( obj );
        }
    }

    public DamageText Spawn( Vector2 _pos, float _damage )
    {
        if ( pool.Count <= 0 )
        {
            Allocate();
        }

        DamageText obj = pool.Pop();
        obj.Initialize( _pos, ( int )_damage );
        obj.transform.SetParent( usePool.transform );
        obj.gameObject.SetActive( true );

        return obj;
    }

    public void Despawn( DamageText _obj )
    {
        //_obj.transform.position = new Vector2( 0.0f, 1000.0f );
        _obj.transform.SetParent( waitPool.transform );
        _obj.gameObject.SetActive( false );
        pool.Push( _obj );
    }
}
