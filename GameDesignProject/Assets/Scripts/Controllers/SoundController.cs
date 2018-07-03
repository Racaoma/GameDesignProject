using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    //Sound Clip References
    public AudioClip thunderClip1;
    public AudioClip thunderClip2;
    public AudioClip fireClip;
    public AudioClip ablazeClip;
    public AudioClip extinguishClip;
    public AudioClip frezingClip;
    public AudioClip shatteringClip;
    public AudioClip blizzardClip;
    public AudioClip cleanseClip;
    public AudioClip purifyClip;
    public AudioClip runeSphereActivateClip;
    public AudioClip rainClip;
    public AudioClip hailClip;

    //Audio Source Reference
    public AudioSource audioSource;

    //Play Sound Method
    public void playSound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
