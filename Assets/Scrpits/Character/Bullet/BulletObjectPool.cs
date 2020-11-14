using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : Singleton<BulletObjectPool>
{
    public Bullet prefabBullet;

    // 사용중인 풀
    private Dictionary<string, List<Bullet>> bulletUsePool = new Dictionary<string, List<Bullet>>();
    // 대기중인 풀
    private Dictionary<string, List<Bullet>> bulletWaitPool = new Dictionary<string, List<Bullet>>();

    private Dictionary<string, GameObject> usePool  = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> waitPool = new Dictionary<string, GameObject>();

    // 생성할 개수
    private int allocateCount = 10;

    public void Allocate( Bullet _prefab )
    {
        string name = _prefab.GetType().Name;

        if ( bulletWaitPool.ContainsKey( name ) == false )
        {
            bulletUsePool.Add( name, new List<Bullet>() );
            bulletWaitPool.Add( name, new List<Bullet>() );

            usePool.Add( name, new GameObject( name + "UsePool" ) );
            usePool[name].transform.parent = this.transform;

            waitPool.Add( name, new GameObject( name + "WaitPool" ) );
            waitPool[name].transform.parent = this.transform;
        }

        for ( int count = 0; count < allocateCount; count++ )
        {
            Bullet bullet = Instantiate<Bullet>( _prefab );
            bullet.transform.parent = waitPool[name].transform;
            bullet.gameObject.SetActive( false );
            bulletWaitPool[name].Add( bullet );
        }
    }

    public Bullet Spawn( Bullet _bullet, Vector2 _pos, Vector2 _dir )
    {
        string name = _bullet.GetType().Name;
        // 리스트에 총알이 없다면 새로 생성
        if ( bulletWaitPool.ContainsKey( name ) == false || bulletWaitPool[name].Count <= 0 )
        {
            Allocate( _bullet );
        }

        Bullet bullet = bulletWaitPool[name][0];
        bullet.Initialize( _pos, _dir );
        bullet.transform.parent = usePool[name].transform;
        bulletUsePool[name].Add( bullet );
        bulletWaitPool[name].Remove( bullet );
        bullet.gameObject.SetActive( true );

        return bullet;
    }

    public void Despawn( Bullet _bullet )
    {
        string name = _bullet.GetType().Name;

        _bullet.transform.position = new Vector2( 0, -1000.0f );
        _bullet.transform.parent = waitPool[name].transform;
        bulletWaitPool[name].Add( _bullet );
        bulletUsePool[name].Remove( _bullet );
        _bullet.gameObject.SetActive( false );
    }
}
