using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) == true )
        {
            BulletObjectPool.Instance.Spawn( 10.0f, new Vector3( 0, 4.5f, 0 ), Vector2.down );
        }
    }
}
