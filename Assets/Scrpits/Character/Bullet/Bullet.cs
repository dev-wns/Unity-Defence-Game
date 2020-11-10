using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;
    public float timer;
    public float lifeTime;
    public Vector2 direction;

    public GameObject deathLine;

    private void Awake()
    {
        speed = 1000.0f;
        timer = 0.0f;
        lifeTime = 3.0f;
    }

    private void Update()
    {
        this.timer += Time.deltaTime;

        if ( this.timer >= lifeTime )
            BulletObjectPool.Instance.Despawn( this );

        this.transform.Translate( direction * speed * Time.deltaTime );
    }

    public void SetBullet( float _damage, Vector3 _position, Vector2 _direction )
    {
        this.damage = _damage;
        this.direction = _direction;
        this.transform.position = _position;
        this.timer = 0.0f;
    }
}
