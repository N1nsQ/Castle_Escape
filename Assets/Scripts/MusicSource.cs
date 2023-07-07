using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSource : MonoBehaviour
{
    AudioSource musicSource;
    private void OnEnable()
    {
        EventManager.MusicIn += VolumeUp;
        EventManager.MusicOut += VolumeDown;
        musicSource = GetComponent<AudioSource>();
    }

    private void VolumeUp()
    {
        StartCoroutine(FadeAudioSource.StartFade(musicSource, 2, 1));
    }

    private void VolumeDown()
    {
        StartCoroutine(FadeAudioSource.StartFade(musicSource, 1, 0.1f));
    }

    private void OnDisable()
    {
        EventManager.MusicIn -= VolumeUp;
        EventManager.MusicOut -= VolumeDown;
    }
}
