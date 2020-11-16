using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class GameManager : Singleton<GameManager>
{
    private short game_round = 1;

    // Ȱ��ȭ �Ǿ��ִ� �� ��ü
    private LinkedList<Enemy> enemies = new LinkedList<Enemy>();

    private Stopwatch spawn_timer = new Stopwatch();
    private Stopwatch round_timer = new Stopwatch();

    private void Awake()
    {
        spawn_timer.Start();
        round_timer.Start();
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
        // �ʴ� �� �ϳ� ����
        if ( spawn_timer.ElapsedMilliseconds >= 1000 )
        {
            EnemyObjectPool.Instance.Spawn();
            spawn_timer.Restart();
        }
    }
}
