using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class GameManager : Singleton<GameManager>
{
    private short game_round = 1;

    // 오브젝트 풀
    public EnemyObjectPool enemy_object_pool;
    public BulletObjectPool bullet_object_pool;
    public DamageTextPool damage_text_pool;

    // 활성화 되어있는 적 객체
    private LinkedList<Enemy> enemies = new LinkedList<Enemy>();

    private Stopwatch spawn_timer = new Stopwatch();
    private Stopwatch round_timer = new Stopwatch();

    private void Awake()
    {
        spawn_timer.Start();
        round_timer.Start();
    }

    private void Start()
    {
        damage_text_pool.Allocate();
    }

    public short GetRound()
    {
        return game_round;
    }

    public LinkedList<Enemy> GetEnemies()
    {
        return enemies;
    }

    public float GetDamage()
    {
        return 10.0f; // Random.Range( 1.0f, 1000.0f );
    }

    void Update()
    {
        // 초당 적 하나 생성
        if ( spawn_timer.ElapsedMilliseconds >= 1000 )
        {
            enemy_object_pool.Spawn();
            spawn_timer.Restart();
        }
    }
}
