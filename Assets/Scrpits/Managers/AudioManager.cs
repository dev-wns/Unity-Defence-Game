using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( AudioSource ) )]
public class AudioManager : Singleton<GameManager>
{
    private AudioSource audio_source;
    [SerializeField]
    private List<AudioClip> bgms;

    private void Awake()
    {
        audio_source = GetComponent<AudioSource>();
        audio_source.clip = bgms[ Random.Range( 0, bgms.Count - 1 ) ];
        audio_source.Play();
    }
}
