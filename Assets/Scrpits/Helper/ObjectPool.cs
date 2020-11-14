using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<Type> : MonoBehaviour // : Singleton<Type> where Type : Singleton<Type>
{
    private delegate void Initialize();
    private Initialize initialize;

    // ������� ������Ʈ ����Ʈ
    public List<Type> objectUsePool = new List<Type>();
    // ������� ������Ʈ ����Ʈ
    public List<Type> objectWaitPool = new List<Type>();

    // ������ ����
    public int allocateCount;

    public GameObject UsePool;
    public GameObject WaitPool;

    private void Awake()
    {
        Allocate();
    }

    private void Start()
    {
        //UsePool = GameObject.Find( typeof( Type ).ToString() ).transform.Find( "UsePool" ).gameObject;
        //WaitPool = GameObject.Find( typeof( Type ).ToString() ).transform.Find( "WaitPool" ).gameObject;
    }

    protected virtual void Allocate()
    {
        //for( int count = 0; count < allocateCount; count++ )
        //{
        //    Instantiate( prefab );
        //    objectWaitPool.Add( new Type );
        //}
    }

    public virtual Type Spawn()
    {
        // ����Ʈ�� ������Ʈ�� ���ٸ� ���� ����
        if ( objectWaitPool.Count <= 0 )
        {
            Allocate();
        }

        Type obj = objectWaitPool[0];
        objectUsePool.Add( obj );
        objectWaitPool.Remove( obj );
        //obj.transform.parent = UsePool.transform;
        //obj.gameObject.SetActive( true );

        return obj;
    }

    public virtual void Despawn( Type _obj )
    {
        objectWaitPool.Add( _obj );
        objectUsePool.Remove( _obj );
        //_obj.transform.position = new Vector3( 1000, 0, 0 );
        //_obj.transform.parent = WaitPool.transform;
        //_obj.gameObject.SetActive( false );
    }
}
