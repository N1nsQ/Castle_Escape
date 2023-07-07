using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolumeChange : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnMusicOut();
    }

    private void OnDisable()
    {
        EventManager.OnMusicIn();
    }
}
