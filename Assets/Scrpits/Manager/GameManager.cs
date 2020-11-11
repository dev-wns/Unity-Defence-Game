using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public float playerDefaultDamage;

    private void Awake()
    {
        if ( instance == null )
        {
            instance = this;
            DontDestroyOnLoad( this.gameObject );
        }
        else
        {
            Destroy( this.gameObject );
        }
    }

    private void Start()
    {
        playerDefaultDamage = 10.0f;
    }

    public static GameManager Instance
    {
        get
        {
            if ( instance == null )
                return null;

            return instance;
        }
    }

    void Update()
    {
        if ( Input.GetMouseButtonDown( 0 ) == true )
        {
            EnemyObjectPool.Instance.Spawn( new Vector3( Random.Range( -500, 500 ), 960, 0 ) );
        }
    }
}
