using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundSystem 
{
    public static void PlaySound(AudioClip clip, bool loop, AudioSource source)
    {
        source.clip = clip;
        source.loop = loop;
        source.Play();
    }


    public static void StopPlayingSound(AudioClip clip,  AudioSource source)
    {
        source.clip = clip;
        source.Stop();
    }


    public static void PlaySound(AudioClip clip, bool loop,float volume , AudioSource source)
    {
        source.volume = volume;
        source.clip = clip;
        source.loop = loop;
        source.Play();
    }


}
