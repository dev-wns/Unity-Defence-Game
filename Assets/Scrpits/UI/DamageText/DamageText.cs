using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Color alpha;
    private Timer timer = new Timer();

    private float moveSpeed;
    private float alphaSpeed;
    private float destroyDuration;

    private float horizontalOffset;
    private float verticalOffset;
    private int damage;

    public void Initialize( Vector2 _pos, int _damage )
    {
        timer.Initialize( destroyDuration );
        transform.position = new Vector2( _pos.x + 540.0f - horizontalOffset,
                                          _pos.y + 960.0f + verticalOffset );

        damage = _damage;
        text.text = damage.ToString();
        alpha.a = 1.0f;
    }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        horizontalOffset = Random.Range( -50.0f, 50.0f );
        verticalOffset = 50.0f;

        alpha = text.color;
        moveSpeed = 150.0f;
        alphaSpeed = 2.0f;
        destroyDuration = 1.5f;
    }

    private void Update()
    {
        if ( timer.Update() == false )
        {
            DamageTextPool.Instance.Despawn( this );
        }

        transform.Translate( this.transform.up * moveSpeed * Time.deltaTime );
        alpha.a = Mathf.Lerp( alpha.a, 0.0f, alphaSpeed * Time.deltaTime );
        text.color = alpha;
    }
}
