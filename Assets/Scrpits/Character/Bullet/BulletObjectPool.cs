using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : Singleton<BulletObjectPool>
{
    public Bullet prefabBullet;

    // ������� ������Ʈ ����Ʈ
    public List<Bullet> bulletUsePool = new List<Bullet>();
    // ������� ������Ʈ ����Ʈ
    public List<Bullet> bulletWaitPool = new List<Bullet>();

    // �Ѿ� �����ϴ� ����
    public int allocateCount;

    public GameObject UsePool;
    public GameObject WaitPool;

    private void Awake()
    {
        allocateCount = 10;
    }

    private void Start()
    {
        Allocate();
    }

    private void Allocate()
    {
        for ( int count = 0; count < allocateCount; count++ )
        {
            Bullet bullet = Instantiate<Bullet>( prefabBullet );
            bullet.transform.parent = WaitPool.transform;
            bullet.gameObject.SetActive( false );
            bulletWaitPool.Add( bullet );
        }
    }

    public Bullet Spawn( float damage, Vector3 position, Vector3 direction )
    {
        // ����Ʈ�� �Ѿ��� ���ٸ� ���� ����
        if ( bulletWaitPool.Count <= 0 )
            Allocate();

        Bullet bullet = bulletWaitPool[0];
        bulletUsePool.Add( bullet );
        bulletWaitPool.Remove( bullet );
        bullet.transform.parent = UsePool.transform;
        bullet.SetBullet( damage, position, direction );
        bullet.gameObject.SetActive( true );

        return bullet;
    }

    public void Despawn( Bullet bullet )
    {
        bullet.transform.position = new Vector3( 0, -1000.0f, 0 );
        bulletWaitPool.Add( bullet );
        bulletUsePool.Remove( bullet );
        bullet.transform.parent = WaitPool.transform;
        bullet.gameObject.SetActive( false );
    }
}
