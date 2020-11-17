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

    private readonly short life_time = 500; // 0.5
    private readonly float vertical_offset = 50.0f;
    private float horizontal_offset;

    private Transform origin_transform;
    public Transform current_transform
    {
        get
        {
            return origin_transform;
        }
        set
        {
            if ( !ReferenceEquals( value, null ) )
            {
                origin_transform = value;
            }
        }
    }

    public void Initialize( Vector2 _pos, int _damage )
    {
        current_transform.position = new Vector2( _pos.x + horizontal_offset,
                                                  _pos.y + vertical_offset );

        text.text = _damage.ToString();
        alpha.a = 1.0f;
        timer.Restart();
    }

    private void Awake()
    {
        horizontal_offset = Random.Range( -50.0f, 50.0f );
        current_transform = transform;
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        alpha = text.color;
        move_speed = 150.0f;
        alpha_fade_speed = 5.0f;
    }

    private void Update()
    {
        if ( timer.ElapsedMilliseconds >= life_time )
        {
            DamageTextPool.Instance.Despawn( this );
        }

        current_transform.Translate( current_transform.up * move_speed * Time.deltaTime );
        alpha.a = Mathf.Lerp( alpha.a, 0.0f, alpha_fade_speed * Time.deltaTime );
        text.color = alpha;
    }
}
