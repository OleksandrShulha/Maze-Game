using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGame : MonoBehaviour
{
    public AudioSource audioSourceMusic, audioSourceSound;

    private void Start()
    {
        if (PlayerPrefs.GetInt("MusicOn") ==1)
        {
            audioSourceMusic.Play();
            audioSourceMusic.volume = PlayerPrefs.GetFloat("MusicVolume");

        }
        else
        {
            audioSourceMusic.Stop();
        }

        if (PlayerPrefs.GetInt("SoundOn") == 1)
        {
            audioSourceSound.volume = PlayerPrefs.GetFloat("SoundVolume");

        }
        else
        {
            audioSourceSound.volume = 0;
        }

    }

    public void PlayMusic()
    {
        audioSourceMusic.Play();
    }
    public void StopMusic()
    {
        audioSourceMusic.Stop();
    }

}
