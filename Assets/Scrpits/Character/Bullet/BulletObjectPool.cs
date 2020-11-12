using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : Singleton<BulletObjectPool>
{
    public Bullet prefabBullet;

    // 사용중인 오브젝트 리스트
    public List<Bullet> bulletUsePool = new List<Bullet>();
    // 대기중인 오브젝트 리스트
    public List<Bullet> bulletWaitPool = new List<Bullet>();

    public Dictionary<string, List<Bullet>> bulletusepool = new Dictionary<string, List<Bullet>>();
    public Dictionary<string, List<Bullet>> bulletwaitpool = new Dictionary<string, List<Bullet>>();

    private Dictionary<string, GameObject> usePool  = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> waitPool = new Dictionary<string, GameObject>();

    // 생성할 개수
    private int allocateCount = 10;

    private GameObject UsePool;
    private GameObject WaitPool;

    private void Awake()
    {
        //UsePool = new GameObject( typeof( Bullet ).ToString() + "UsePool" );
        //UsePool.transform.parent = this.transform;
        //WaitPool = new GameObject( typeof( Bullet ).ToString() + "WaitPool" );
        //WaitPool.transform.parent = this.transform;
    }

    private void Start()
    {
        //Allocate();
    }

    public void Allocate( Bullet prefab )
    {
        string name = prefab.GetType().Name;

        if ( bulletwaitpool.ContainsKey( name ) == false )
        {
            bulletusepool.Add( name, new List<Bullet>() );
            bulletwaitpool.Add( name, new List<Bullet>() );

            usePool.Add( name, new GameObject( name + "UsePool" ) );
            usePool[name].transform.parent = this.transform;

            waitPool.Add( name, new GameObject( name + "WaitPool" ) );
            waitPool[name].transform.parent = this.transform;
        }

        for ( int count = 0; count < allocateCount; count++ )
        {
            Bullet bullet = Instantiate<Bullet>( prefab );
            bullet.transform.parent = waitPool[name].transform;
            bullet.gameObject.SetActive( false );
            bulletwaitpool[name].Add( bullet );
        }
    }

    public Bullet Spawn( Bullet b, Vector3 position, Vector3 direction )
    {
        string name = b.GetType().Name;
        // 리스트에 총알이 없다면 새로 생성
        if ( bulletwaitpool.ContainsKey( name ) == false || bulletwaitpool[name].Count <= 0 )
            Allocate( b );

        Bullet bullet = bulletwaitpool[name][0];
        bulletusepool[name].Add( bullet );
        bulletwaitpool[name].Remove( bullet );
        bullet.transform.parent = usePool[name].transform;
        bullet.SetBullet( position, direction );
        bullet.gameObject.SetActive( true );

        return bullet;
    }

    public void Despawn( Bullet bullet )
    {
        string name = bullet.GetType().Name;

        bullet.transform.position = new Vector3( 0, -1000.0f, 0 );
        bulletwaitpool[name].Add( bullet );
        bulletusepool[name].Remove( bullet );
        bullet.transform.parent = waitPool[name].transform;
        bullet.gameObject.SetActive( false );
    }

    //private void Allocate()
    //{
    //    for ( int count = 0; count < allocateCount; count++ )
    //    {
    //        Bullet bullet = Instantiate<Bullet>( prefabBullet );
    //        bullet.transform.parent = WaitPool.transform;
    //        bullet.gameObject.SetActive( false );
    //        bulletWaitPool.Add( bullet );
    //    }
    //}

    //public Bullet Spawn( Vector3 position, Vector3 direction )
    //{
    //    // 리스트에 총알이 없다면 새로 생성
    //    if ( bulletWaitPool.Count <= 0 )
    //        Allocate();

    //    Bullet bullet = bulletWaitPool[0];
    //    bulletUsePool.Add( bullet );
    //    bulletWaitPool.Remove( bullet );
    //    bullet.transform.parent = UsePool.transform;
    //    bullet.SetBullet( position, direction );
    //    bullet.gameObject.SetActive( true );

    //    return bullet;
    //}

    //public void Despawn( Bullet bullet )
    //{
    //    bullet.transform.position = new Vector3( 0, -1000.0f, 0 );
    //    bulletWaitPool.Add( bullet );
    //    bulletUsePool.Remove( bullet );
    //    bullet.transform.parent = WaitPool.transform;
    //    bullet.gameObject.SetActive( false );
    //}
}
