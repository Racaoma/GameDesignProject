using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlashController : MonoBehaviour
{
    //Variables
    public Image flash;
    private float decay;

    //Flash!
    public void flashScreen(float decay)
    {
        this.decay = decay;
        flash.color = new Color(1f, 1f, 1f, 0.9f);
    }

	// Update is called once per frame
	void Update ()
    {
		if(flash.color.a > 0)
        {
            flash.color = new Color(1f, 1f, 1f, flash.color.a - (decay * Time.deltaTime));
        }
	}
}
