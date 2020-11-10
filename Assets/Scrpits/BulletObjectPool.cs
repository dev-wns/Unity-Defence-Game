using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    private static BulletObjectPool instance = null;

    public Bullet prefabBullet;
    
    // 사용중인 오브젝트 리스트
    public List<Bullet> bulletUsePool = new List<Bullet>();
    // 대기중인 오브젝트 리스트
    public List<Bullet> bulletWaitPool = new List<Bullet>();

    // 총알 생성하는 개수
    public int bulletCreateCount = 10;

    public GameObject UsePool;
    public GameObject WaitPool;

    private void Awake()
    {
        if ( instance == null )
        {
            instance = this;
            DontDestroyOnLoad( this.gameObject );
        }
        else
        {
            Destroy( this.gameObject );
        }
    }

    public static BulletObjectPool Instance
    {
        get
        {
            if ( instance == null )
                return null;

            return instance;
        }
    }

    private void Start()
    {
        CreateBullet();
    }

    private void CreateBullet()
    {
        for ( int count = 0; count < bulletCreateCount; count++ )
        {
            Bullet bullet = Instantiate<Bullet>( prefabBullet );
            bullet.transform.parent = WaitPool.transform;
            bullet.gameObject.SetActive( false );
            bulletWaitPool.Add( bullet );
        }
    }

    public Bullet Spawn( float speed, Vector3 position, Vector2 direction )
    {
        // 리스트에 총알이 없다면 새로 생성
        if ( bulletWaitPool.Count <= 0 )
            CreateBullet();

        Bullet bullet = bulletWaitPool[0];
        bulletUsePool.Add( bullet );
        bulletWaitPool.Remove( bullet );
        bullet.transform.parent = UsePool.transform;
        bullet.SetBullet( speed, position, direction );
        bullet.gameObject.SetActive( true );

        return bullet;
    }

    public void Despawn( Bullet bullet )
    {
        bullet.transform.position = new Vector3( 0, 0, 0 );
        bulletWaitPool.Add( bullet );
        bulletUsePool.Remove( bullet );
        bullet.transform.parent = WaitPool.transform;
        bullet.gameObject.SetActive( false );
    }

}
