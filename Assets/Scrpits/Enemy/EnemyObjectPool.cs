using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    private static EnemyObjectPool instance = null;

    public Enemy prefabEnemy;

    // ������� ������Ʈ ����Ʈ
    public List<Enemy> enemyUsePool = new List<Enemy>();
    // ������� ������Ʈ ����Ʈ
    public List<Enemy> enemyWaitPool = new List<Enemy>();

    // �� ���� ����
    public int enemyCreateCount;

    public GameObject UsePool;
    public GameObject WaitPool;

    public List<Enemy> GetEnemyUsePool()
    {
        return enemyUsePool;
    }

    private void Awake()
    {
        enemyCreateCount = 1;

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

    public static EnemyObjectPool Instance
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
        for ( int count = 0; count < enemyCreateCount; count++ )
        {
            Enemy enemy = Instantiate<Enemy>( prefabEnemy );
            enemy.transform.parent = WaitPool.transform;
            enemy.gameObject.SetActive( false );
            enemyWaitPool.Add( enemy );
        }
    }

    public Enemy Spawn( Vector3 position )
    {
        // ����Ʈ�� �Ѿ��� ���ٸ� ���� ����
        if ( enemyWaitPool.Count <= 0 )
            CreateBullet();

        Enemy enemy = enemyWaitPool[0];
        enemy.transform.position = position;
        enemyUsePool.Add( enemy );
        enemyWaitPool.Remove( enemy );
        enemy.transform.parent = UsePool.transform;
        enemy.gameObject.SetActive( true );

        return enemy;
    }

    public void Despawn( Enemy enemy )
    {
        enemy.transform.position = new Vector3( 0, 0, 0 );
        enemyWaitPool.Add( enemy );
        enemyUsePool.Remove( enemy );
        enemy.transform.parent = WaitPool.transform;
        enemy.gameObject.SetActive( false );
    }
}
