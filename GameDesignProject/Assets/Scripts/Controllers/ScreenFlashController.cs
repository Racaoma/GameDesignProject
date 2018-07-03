using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlashController : MonoBehaviour
{
    //Variables
    public Image flash;
    private float minimumAlpha;
    private float whiteOutDuration;
    private float decay;
    private bool blackFlash;

    //Start
    private void Start()
    {
        minimumAlpha = 0f;
        whiteOutDuration = 0f;
        blackFlash = false;
        flash.color = new Color(1f, 1f, 1f, 0f);
    }

    //Flash!
    public void flashScreen(float decay)
    {
        this.decay = decay;
        flash.color = new Color(1f, 1f, 1f, 0.9f);
    }

    //Flash Black
    public void flashBlackScreen(float decay)
    {
        blackFlash = true;
        this.decay = decay;
        whiteOutDuration = 0;
        flash.color = new Color(0f, 0f, 0f, 0.0f);
    }

    //Whiteout
    public void whiteOut(float duration, float decay)
    {
        this.decay = decay;
        whiteOutDuration = duration;
        minimumAlpha = 0.3f;
    }

    // Update is called once per frame
    void Update ()
    {
        if (whiteOutDuration > 0f)
        {
            flash.color = new Color(1f, 1f, 1f, Mathf.Min(flash.color.a + (0.25f * Time.deltaTime), minimumAlpha));
            whiteOutDuration -= Time.deltaTime;
        }
        else minimumAlpha = 0f;

        if (blackFlash)
        {
            flash.color = new Color(0f, 0f, 0f, flash.color.a + (decay * 2 * Time.deltaTime));
            if (flash.color.a >= 0.9f) blackFlash = false;
        }
        else if (flash.color.a > 0 && whiteOutDuration <= 0f) flash.color = new Color(flash.color.r, flash.color.g, flash.color.b, flash.color.a - (decay * Time.deltaTime));
	}
}
