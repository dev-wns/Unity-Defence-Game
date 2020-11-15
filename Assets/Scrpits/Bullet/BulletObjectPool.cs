using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : Singleton<BulletObjectPool>
{
    //public Bullet prefab;
    // ĳ���� ���� �Ѿ��� �ٸ��� ������
    // �Ѿ� �������� �÷��̾��ʿ��� �������.

    // �Ҵ� �� ��ü Ǯ
    private Dictionary<string, Stack<Bullet>> pool = new Dictionary<string, Stack<Bullet>>();

    // Ǯ�� �� ������Ʈ�� �ڽĿ�����Ʈ�� ���� ������ �θ� ������Ʈ
    private Dictionary<string, GameObject> usePool = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> waitPool = new Dictionary<string, GameObject>();

    // ������ ����
    private int allocateCount;
    private int increaseCount;

    private void Start()
    {
        allocateCount = 10;
    }

    private void Allocate( Bullet _prefab )
    {
        string name = _prefab.GetType().Name;

        if ( pool.ContainsKey( name ) == false )
        {
            pool.Add( name, new Stack<Bullet>() );

            usePool.Add( name, new GameObject( name + "UsePool" ) );
            usePool[name].transform.SetParent( this.transform );

            waitPool.Add( name, new GameObject( name + "WaitPool" ) );
            waitPool[name].transform.SetParent( this.transform );
        }

        for ( int count = 0; count < allocateCount; count++ )
        {
            Bullet bullet = Instantiate<Bullet>( _prefab );
            bullet.transform.SetParent( waitPool[name].transform );
            bullet.name = _prefab.name + increaseCount++.ToString();
            bullet.gameObject.SetActive( false );
            pool[name].Push( bullet );
        }
    }

    public Bullet Spawn( Bullet _bullet, Vector2 _pos, Vector2 _dir )
    {
        string name = _bullet.GetType().Name;
        // ����Ʈ�� �Ѿ��� ���ٸ� ���� ����
        if ( pool.ContainsKey( name ) == false || pool[name].Count <= 0 )
        {
            Allocate( _bullet );
        }

        Bullet bullet = pool[name].Pop();
        bullet.Initialize( _pos, _dir );
        bullet.transform.SetParent( usePool[name].transform );
        bullet.gameObject.SetActive( true );

        return bullet;
    }

    public void Despawn( Bullet _bullet )
    {
        string name = _bullet.GetType().Name;

        _bullet.transform.position = new Vector2( 0.0f, -1000.0f );
        _bullet.transform.SetParent( waitPool[name].transform );
        _bullet.gameObject.SetActive( false );
        pool[name].Push( _bullet );
    }
}
