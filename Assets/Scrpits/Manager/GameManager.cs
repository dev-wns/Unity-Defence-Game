using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float playerDefaultDamage;

    private void Start()
    {
        playerDefaultDamage = 10.0f;
    }

    void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) == true )
        {
            EnemyObjectPool.Instance.Spawn( new Vector3( Random.Range( -500, 500 ), 960, 0 ) );
            // EnemyObjectPool.Instance.Spawn( new Vector3( 0, 0, 0 ) );
        }
    }
}
