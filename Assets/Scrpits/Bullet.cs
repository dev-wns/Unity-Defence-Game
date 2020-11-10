using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 생성될 포지션
    public Vector3 position;

    public float speed = 100.0f;
    public float timer = 0.0f;
    public float lifeTime = 2.0f;
    public Vector2 direction;

    private void Update()
    {
        this.timer += Time.deltaTime;

        if ( this.timer >= lifeTime )
            BulletObjectPool.Instance.Despawn( this );

        this.transform.Translate( direction * speed * Time.deltaTime );
    }

    public void SetBullet( float _speed, Vector3 _position, Vector2 _direction )
    {
        if ( _speed < 0.0f || _speed > 1000.0f )
            Debug.Log( "비정상적인 값 Bullet Speed : " + _speed );

        this.speed = _speed;
        this.direction = _direction;
        this.position = _position;
        this.timer = 0.0f;
    }
}
