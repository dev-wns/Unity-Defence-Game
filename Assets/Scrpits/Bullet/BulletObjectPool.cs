using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour
{
    // 캐릭터 마다 총알이 다르기 때문에
    // 총알 프리팹을 플레이어쪽에서 들고있음.

    // 할당 된 전체 풀
    private Dictionary<string, Stack<Bullet>> pool = new Dictionary<string, Stack<Bullet>>();

    // 풀링 된 오브젝트를 자식오브젝트로 연결 시켜줄 부모 오브젝트
    private Dictionary<string, GameObject> use_pool = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> wait_pool = new Dictionary<string, GameObject>();

    // 생성할 개수
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
        // 리스트에 총알이 없다면 새로 생성
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
