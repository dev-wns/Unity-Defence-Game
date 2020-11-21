using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Slider health_userinterface;
    public LinkedList<Enemy> enemy_enable_list { get; private set; }
    public short game_round { get; private set; }

    public float health { get; private set; }
    private float origin_health = 100.0f;
    [SerializeField]
    private float derease_health;

    private void OnHitPlayer( Enemy _enemy )
    {
        health -= _enemy.damage;
    }

    private void Awake()
    {
        enemy_enable_list = new LinkedList<Enemy>();
        game_round = 1;
        health = derease_health = origin_health;
    }

    private void Start()
    {
        Enemy.player_hit_event += OnHitPlayer;
        StartCoroutine( DecreaseHealth() );
        StartCoroutine( SpawnEnemy() );
    }

    private IEnumerator DecreaseHealth()
    {
        while( true )
        {
            if ( derease_health < 0 )
            {
                health = derease_health = origin_health;
                health_userinterface.value = 1.0f;
            }

            if ( health < derease_health )
            {
                derease_health -= 0.1f + ( ( ( derease_health - health ) / origin_health ) * 200.0f * Time.deltaTime );
                health_userinterface.value = derease_health / origin_health;
            }

            yield return null;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while ( true )
        {
            EnemyObjectPool.Instance.Spawn();
            yield return YieldCache.WaitForSeconds( 1.0f );
        }
    }
}
