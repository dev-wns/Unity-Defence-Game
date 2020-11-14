using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : Singleton<BulletObjectPool>
{
    public Bullet prefabBullet;

    // 사용중인 풀
    public Dictionary<string, List<Bullet>> bulletUsePool = new Dictionary<string, List<Bullet>>();
    // 대기중인 풀
    public Dictionary<string, List<Bullet>> bulletWaitPool = new Dictionary<string, List<Bullet>>();

    private Dictionary<string, GameObject> usePool  = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> waitPool = new Dictionary<string, GameObject>();

    // 생성할 개수
    private int allocateCount = 10;

    public void Allocate( Bullet prefab )
    {
        string name = prefab.GetType().Name;

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
            Bullet bullet = Instantiate<Bullet>( prefab );
            bullet.transform.parent = waitPool[name].transform;
            bullet.gameObject.SetActive( false );
            bulletWaitPool[name].Add( bullet );
        }
    }

    public Bullet Spawn( Bullet b, Vector2 position, Vector2 direction )
    {
        string name = b.GetType().Name;
        // 리스트에 총알이 없다면 새로 생성
        if ( bulletWaitPool.ContainsKey( name ) == false || bulletWaitPool[name].Count <= 0 )
        {
            Allocate( b );
        }

        Bullet bullet = bulletWaitPool[name][0];
        bulletUsePool[name].Add( bullet );
        bulletWaitPool[name].Remove( bullet );
        bullet.transform.parent = usePool[name].transform;
        bullet.SetBullet( position, direction );
        bullet.gameObject.SetActive( true );

        return bullet;
    }

    public void Despawn( Bullet bullet )
    {
        string name = bullet.GetType().Name;

        bullet.transform.position = new Vector2( 0, -1000.0f );
        bulletWaitPool[name].Add( bullet );
        bulletUsePool[name].Remove( bullet );
        bullet.transform.parent = waitPool[name].transform;
        bullet.gameObject.SetActive( false );
    }
}
