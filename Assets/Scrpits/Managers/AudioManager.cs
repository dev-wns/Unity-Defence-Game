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

    private AudioSource audio_source;

    private void Awake()
    {
        audio_source = GetComponent<AudioSource>();
        
        if ( bgms.Count <= 0 )
        {
            Debug.LogWarning( "bgms is empty." );
            return;
        }

        audio_source.clip = bgms[ Random.Range( 0, bgms.Count ) ];
        audio_source.Play();
    }

    public void PlaySound( AudioClip _sound )
    {
        audio_source.PlayOneShot( _sound );
    }

    public void PlaySound( List<AudioClip> _sounds )
    {
        if ( _sounds.Count <= 0 )
        {
            return;
        }

        PlaySound( _sounds[ Random.Range( 0, _sounds.Count ) ] );
    }
}
