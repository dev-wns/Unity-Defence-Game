using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Color alpha;
    private Timer timer = new Timer();

    private float move_speed;
    private float alpha_speed;
    private float destroy_duration;

    private float horizontal_offset;
    private float vertical_offset;

    public void Initialize( Vector2 _pos, int _damage )
    {
        timer.Initialize( destroy_duration );
        transform.position = new Vector2( _pos.x + horizontal_offset,
                                          _pos.y + vertical_offset );

        text.text = _damage.ToString();
        alpha.a = 1.0f;
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
        alpha_speed = 2.0f;
        destroy_duration = 0.7f;
    }

    private void Update()
    {
        if ( timer.Update() == false )
        {
            DamageTextPool.Instance.Despawn( this );
        }

        transform.Translate( this.transform.up * move_speed * Time.deltaTime );
        alpha.a = Mathf.Lerp( alpha.a, 0.0f, alpha_speed * Time.deltaTime );
        text.color = alpha;
    }
}
