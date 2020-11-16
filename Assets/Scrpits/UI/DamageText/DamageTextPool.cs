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
    private GameObject use_pool;
    private GameObject wait_pool;

    // ������ ����
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
