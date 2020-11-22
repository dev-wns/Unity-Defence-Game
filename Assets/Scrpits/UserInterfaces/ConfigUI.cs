using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigUI : MonoBehaviour
{
    public GameObject config_pannel;
    public GameObject settings_pannel;

    public delegate void OnPauseEvent( bool _is_pause );
    public static event OnPauseEvent on_pause_event;

    private void OnPause( bool _is_pause )
    {
        config_pannel.SetActive( _is_pause );
        Time.timeScale = _is_pause ? 0.0f : 1.0f;
        on_pause_event?.Invoke( _is_pause );
    }

    private void OnShowSettings()
    {

    }

    private void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 에디터에선 작동 안함
#endif
    }
}
