using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;


public class GameManager : Singleton<GameManager>
{
    public short game_round { get; private set; }
    public LinkedList<Enemy> enemy_enable_list { get; private set; }
    private Stopwatch spawn_timer = new Stopwatch();

     private void Awake()
    {
        enemy_enable_list = new LinkedList<Enemy>();
        spawn_timer.Start();
        game_round = 1;
    }

    private void Start()
    {
        Screen.SetResolution( 1280, 720, true ); // 720p
        Application.targetFrameRate = 60;
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
