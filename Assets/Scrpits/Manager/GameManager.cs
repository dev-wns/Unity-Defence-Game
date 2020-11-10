using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) == true )
        {
            EnemyObjectPool.Instance.Spawn( new Vector3( Random.Range( -500, 500 ), 960, 0 ) );
        }
    }
}
