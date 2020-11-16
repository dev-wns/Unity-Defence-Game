using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class DamageText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Color alpha;
    private Stopwatch timer = new Stopwatch();

    private float move_speed;
    private float alpha_fade_speed;
    private float life_time;

    private float horizontal_offset;
    private float vertical_offset;

    public void Initialize( Vector2 _pos, int _damage )
    {
        transform.position = new Vector2( _pos.x + horizontal_offset,
                                          _pos.y + vertical_offset );

        text.text = _damage.ToString();
        alpha.a = 1.0f;
        timer.Restart();
    }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        horizontal_offset = Random.Range( -50.0f, 50.0f );
        vertical_offset = 50.0f;

        alpha = text.color;
        move_speed = 150.0f;
        life_time = 0.5f;
        alpha_fade_speed = 5.0f;
    }

    private void Update()
    {
        if ( timer.ElapsedMilliseconds >= life_time * 1000.0f )
        {
            DamageTextPool.Instance.Despawn( this );
        }

        transform.Translate( transform.up * move_speed * Time.deltaTime );
        alpha.a = Mathf.Lerp( alpha.a, 0.0f, alpha_fade_speed * Time.deltaTime );
        text.color = alpha;
    }
}
