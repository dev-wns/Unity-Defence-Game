using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigUI : MonoBehaviour
{
    public GameObject config_pannel;
    public GameObject settings_pannel;

    public void OnResume( bool _isResume )
    {
        config_pannel.SetActive( _isResume );
        Time.timeScale = _isResume ? 0.0f : 1.0f;
    }

    public void OnShowSettings()
    {

    }

    public void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 에디터에선 작동 안함
#endif
    }
}
