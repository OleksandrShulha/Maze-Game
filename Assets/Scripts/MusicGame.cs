using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGame : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.Play();
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

}
