using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<Type> : MonoBehaviour // : Singleton<Type> where Type : Singleton<Type>
{
    private delegate void Initialize();
    private Initialize initialize;

    // 사용중인 오브젝트 리스트
    public List<Type> objectUsePool = new List<Type>();
    // 대기중인 오브젝트 리스트
    public List<Type> objectWaitPool = new List<Type>();

    // 생성할 개수
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
        // 리스트에 오브젝트가 없다면 새로 생성
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
