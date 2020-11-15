using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // 활성화 되어있는 적 객체
    private LinkedList<Enemy> enemies = new LinkedList<Enemy>();

    public LinkedList<Enemy> GetEnemies()
    {
        return enemies;
    }

    public float GetDamage()
    {
        return Random.Range( 1.0f, 1000.0f );
    }

    void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) == true )
        {
            EnemyObjectPool.Instance.Spawn( new Vector2( Random.Range( -500.0f, 500.0f ), 960.0f ) );
        }
    }
}
