using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    public GameObject target;
    public GameObject prefabEffect;

    public float timer;
    // �Ѿ� �Ҹ� �� ���� �ð����
    public float lifeTime;

    private void Awake()
    {
        timer = 0.0f;
        lifeTime = 0.5f;
    }

    private void Update()
    {
        if ( target.activeSelf == false )
        {
            timer += Time.deltaTime;

            if ( timer >= lifeTime )
                Destroy( this );
        }

        this.transform.position = target.transform.position;
    }
}
