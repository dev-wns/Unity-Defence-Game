using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( AudioSource ) )]
public class AudioManager : Singleton<AudioManager>
{
    private AudioSource audio_source;
    [SerializeField]
    private List<AudioClip> bgms;

    private void Awake()
    {
        audio_source = GetComponent<AudioSource>();
        
        if ( bgms.Count <= 0 )
        {
            Debug.LogWarning( "bgms is empty." );
            return;
        }

        audio_source.clip = bgms[ Random.Range( 0, bgms.Count - 1 ) ];
        audio_source.Play();
    }

    public void PlaySound( AudioClip sound )
    {
        audio_source.PlayOneShot( sound );
    }

    public void PlaySound( List<AudioClip> sounds )
    {
        if ( sounds.Count <= 0 )
        {
            return;
        }

        PlaySound( sounds[ Random.Range( 0, sounds.Count - 1 ) ] );
    }
}
