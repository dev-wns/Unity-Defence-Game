using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( AudioSource ) )]
public class AudioManager : Singleton<AudioManager>
{
#pragma warning disable CS0649 // 초기화 경고 무시
    [SerializeField]
    private List<AudioClip> bgms;
#pragma warning restore CS0649

    public float bgm_volume = 0.5f;
    public float se_volume = 0.5f;

    private AudioSource audio_source;

    private void Awake()
    {
        ConfigUI.on_change_bgm_volume += OnChangeBgmVolume;
        ConfigUI.on_change_se_volume += OnChangeSEVolume;

        audio_source = GetComponent<AudioSource>();
        
        if ( bgms.Count <= 0 )
        {
            Debug.LogWarning( "bgms is empty." );
            return;
        }

        audio_source.volume = bgm_volume;
        audio_source.clip = bgms[ Random.Range( 0, bgms.Count ) ];
        audio_source.Play();
    }

    public void PlaySound( AudioClip _sound )
    {
        audio_source.PlayOneShot( _sound, se_volume );
    }

    public void PlaySound( List<AudioClip> _sounds )
    {
        if ( _sounds.Count <= 0 )
        {
            return;
        }

        PlaySound( _sounds[ Random.Range( 0, _sounds.Count ) ] );
    }

    public void OnChangeBgmVolume( float _value )
    {
        audio_source.volume = bgm_volume = _value;
    }

    public void OnChangeSEVolume( float _value )
    {
        se_volume = _value;
    }
}
