using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float playerDefaultDamage;
    [SerializeField]
    private LinkedList<Enemy> enemies = new LinkedList<Enemy>();

    public LinkedList<Enemy> GetEnemies()
    {
        return enemies;
    }

    private void Awake()
    {
        playerDefaultDamage = 10.0f;
    }

    void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) == true )
        {
            EnemyObjectPool.Instance.Spawn( new Vector2( Random.Range( -500, 500 ), 960 ) );
        }
    }
}
