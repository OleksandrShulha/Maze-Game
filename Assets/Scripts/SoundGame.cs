using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGame : MonoBehaviour
{
   public AudioSource audioSource;
   public AudioClip kayTake, appleShot, looseSound, winSound, doorOpen, bompActivetion, stunMonstr, deadMonst, fireMonstr;


    public void PlaykayTakeSound()
    {
        audioSource.PlayOneShot(kayTake);
    }

    public void PlayAppleShot()
    {
        audioSource.PlayOneShot(appleShot);
    }

    public void PlayLoose()
    {
        audioSource.PlayOneShot(looseSound);
    }

    public void PlayWin()
    {
        audioSource.PlayOneShot(winSound);
    }

    public void PlayDoorOpen()
    {
        audioSource.PlayOneShot(doorOpen);
    }

    public void PlayBompActivetion()
    {
        audioSource.PlayOneShot(bompActivetion);
    }

    public void PlayStunMonstr()
    {
        audioSource.PlayOneShot(stunMonstr);
    }

    public void PlayDeadMonstn()
    {
        audioSource.PlayOneShot(deadMonst);
    }

    public void PlayFireMonstr()
    {
        audioSource.PlayOneShot(fireMonstr);
    }
}
