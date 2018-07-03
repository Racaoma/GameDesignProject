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
    public AudioClip flashFreezeClip;
    public AudioClip drawfailClip;
    public AudioClip corruptClip;
    public AudioClip darkbellClip;
    public AudioClip devourerClip;
    public AudioClip comboFailClip;

    //Audio Source Reference
    public AudioSource audioSource;
    public AudioSource musicSource;

    //Interval Variables
    private bool fadeMusicBool;

    //Start
    private void Start()
    {
        fadeMusicBool = false;
    }

    //Play Sound Method
    public void playSound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    //Play Sound Loop
    public void playSoundLoop(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    //Fade Main Music
    public void fadeMusic()
    {
        fadeMusicBool = true;
    }

    //UpdateMethod
    private void Update()
    {
        if (fadeMusicBool) musicSource.volume -= 0.35f * Time.deltaTime;
    }
}
