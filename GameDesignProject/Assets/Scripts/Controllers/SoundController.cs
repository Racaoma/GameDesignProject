using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    //Sound Clip References
    public AudioClip thunderClip;

    //Audio Source Reference
    public AudioSource audioSource;

    //Play Sound Method
    public void playSound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
