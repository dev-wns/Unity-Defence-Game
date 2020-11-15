using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : Singleton<BulletObjectPool>
{
    //public Bullet prefab;
    // 캐릭터 마다 총알이 다르기 때문에
    // 총알 프리팹을 플레이어쪽에서 들고있음.

    // 할당 된 전체 풀
    private Dictionary<string, Stack<Bullet>> pool = new Dictionary<string, Stack<Bullet>>();

    // 풀링 된 오브젝트를 자식오브젝트로 연결 시켜줄 부모 오브젝트
    private Dictionary<string, GameObject> usePool = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> waitPool = new Dictionary<string, GameObject>();

    // 생성할 개수
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
        // 리스트에 총알이 없다면 새로 생성
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
