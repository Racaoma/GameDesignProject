using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBalloonController : MonoBehaviour
{
    //Variables
    public Image textBalloon;
    public bool hasMana;
    public float speed;

	// Update is called once per frame
	void Update ()
    {
        //Get Current Fill Ammount
        float currentFillAmmount = textBalloon.fillAmount;

        //Update Text Balloon
        if (hasMana && currentFillAmmount > 0f)
        {
            textBalloon.fillAmount = Mathf.Max(textBalloon.fillAmount - (speed * Time.deltaTime), 0f);
        }
        else if(!hasMana && currentFillAmmount < 1f)
        {
            textBalloon.fillAmount = Mathf.Min(textBalloon.fillAmount + (speed * Time.deltaTime), 1f);
        }
	}
}
