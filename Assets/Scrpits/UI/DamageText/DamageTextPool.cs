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
    private GameObject usePool;
    private GameObject waitPool;

    // ������ ����
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
