using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    // ĳ���� ���� �Ѿ��� �ٸ��� ������
    // �Ѿ� �������� �÷��̾��ʿ��� �������.

    // �Ҵ� �� ��ü Ǯ
    private Dictionary<string, Stack<Bullet>> pool = new Dictionary<string, Stack<Bullet>>();

    // Ǯ�� �� ������Ʈ�� �ڽĿ�����Ʈ�� ���� ������ �θ� ������Ʈ
    private Dictionary<string, GameObject> use_pool = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> wait_pool = new Dictionary<string, GameObject>();

    // ������ ����
    private int allocate_count;
    private int increase_count;

    private void Start()
    {
        allocate_count = 10;
    }

    private void Allocate( Bullet _prefab )
    {
        string name = _prefab.GetType().Name;

        if ( pool.ContainsKey( name ) == false )
        {
            pool.Add( name, new Stack<Bullet>() );

            use_pool.Add( name, new GameObject( name + "UsePool" ) );
            use_pool[name].transform.SetParent( this.transform );

            wait_pool.Add( name, new GameObject( name + "WaitPool" ) );
            wait_pool[name].transform.SetParent( this.transform );
        }

        for ( int count = 0; count < allocate_count; count++ )
        {
            Bullet bullet = Instantiate<Bullet>( _prefab );
            bullet.transform.SetParent( wait_pool[name].transform );
            bullet.name = _prefab.name + increase_count++.ToString();
            bullet.gameObject.SetActive( false );
            pool[name].Push( bullet );
        }
    }

    public Bullet Spawn( Bullet _bullet )
    {
        string name = _bullet.GetType().Name;
        // ����Ʈ�� �Ѿ��� ���ٸ� ���� ����
        if ( pool.ContainsKey( name ) == false || pool[name].Count <= 0 )
        {
            Allocate( _bullet );
        }

        Bullet bullet = pool[name].Pop();
        bullet.transform.SetParent( use_pool[name].transform );
        bullet.gameObject.SetActive( true );

        return bullet;
    }

    public void Despawn( Bullet _bullet )
    {
        string name = _bullet.GetType().Name;
        _bullet.transform.SetParent( wait_pool[name].transform );
        _bullet.gameObject.SetActive( false );
        pool[name].Push( _bullet );
    }
}
