using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigUI : MonoBehaviour
{
    public float game_speed = 1.0f;

    public GameObject config_pannel;
    public GameObject settings_pannel;
    public GameObject buttons_pannel;

    public UnityEngine.UI.Slider bgm_slider;
    public UnityEngine.UI.Slider se_slider;
    public UnityEngine.UI.Slider speed_slider;

    public delegate void OnPauseEvent( bool _is_pause );
    public static event OnPauseEvent on_pause_event;

    public delegate void OnChangeBgmEvent( float _value );
    public static event OnChangeBgmEvent on_change_bgm_volume;

    public delegate void OnChangeSEEvent( float _value );
    public static event OnChangeSEEvent on_change_se_volume;

    private void Awake()
    {
        Time.timeScale = game_speed;
    }

    public void OnPause( bool _is_pause )
    {
        config_pannel.SetActive( _is_pause );
        Time.timeScale = _is_pause ? 0.0f : game_speed;
        on_pause_event?.Invoke( _is_pause );

        OnShowSettings( false );
    }

    public void OnShowSettings( bool _is_show )
    {
        settings_pannel.SetActive( _is_show );
        buttons_pannel.SetActive( !_is_show );

        if ( _is_show )
        {
            bgm_slider.value = AudioManager.Instance.bgm_volume;
            se_slider.value = AudioManager.Instance.se_volume;
            speed_slider.value = game_speed;
        }
    }

    public void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 에디터에선 작동 안함
#endif
    }

    public void OnChangeBgmVolume()
    {
        on_change_bgm_volume?.Invoke( bgm_slider.value );
    }

    public void OnChangeSEVolume()
    {
        on_change_se_volume?.Invoke( se_slider.value );
    }

    public void OnChangeGameSpeed()
    {
        // slider 초기화시 이벤트 발생 무시
        if ( Mathf.Approximately( game_speed, speed_slider.value ) )
        {
            return;
        }

        Time.timeScale = game_speed = speed_slider.value;
    }
}
