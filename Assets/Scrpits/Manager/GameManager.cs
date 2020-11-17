using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;


public class GameManager : Singleton<GameManager>
{
    private short game_round = 1;
    private Stopwatch spawn_timer = new Stopwatch();
    public LinkedList<Enemy> enemy_enable_list = new LinkedList<Enemy>();

    public void PushEnemy( Enemy _enemy )
    {
        if ( _enemy == null && enemy_enable_list.Find( _enemy ) != null )
        {
            return;
        }

        enemy_enable_list.AddLast( _enemy );
    }

    public void PopEnemy( Enemy _enemy )
    {
        if ( _enemy == null )
        {
            return;
        }

        enemy_enable_list.Remove( _enemy );
    }

    private void Awake()
    {
        spawn_timer.Start();
    }

    public short GetRound()
    {
        return game_round;
    }

    void Update()
    {
        if ( spawn_timer.ElapsedMilliseconds >= 1000 )
        {
            EnemyObjectPool.Instance.Spawn();
            spawn_timer.Restart();
        }
    }
}
