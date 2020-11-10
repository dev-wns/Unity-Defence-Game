using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    private static BulletObjectPool instance = null;

    public Bullet prefabBullet;
    
    // ������� ������Ʈ ����Ʈ
    public List<Bullet> bulletUsePool = new List<Bullet>();
    // ������� ������Ʈ ����Ʈ
    public List<Bullet> bulletWaitPool = new List<Bullet>();

    // �Ѿ� �����ϴ� ����
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
        // ����Ʈ�� �Ѿ��� ���ٸ� ���� ����
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
