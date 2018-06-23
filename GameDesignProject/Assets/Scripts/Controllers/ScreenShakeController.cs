using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    //Variables
    private Vector3 originalPos;
    private float shakeDuration;
    private float shakeAmmount;
    private bool shake;

    //Start Method
    private void Start()
    {
        originalPos = Camera.main.transform.localPosition;
    }

    //Set Screen Shake
    public void screenShake(float shakeAmmount, float shakeDuration)
    {
        shake = true;
        this.shakeAmmount = shakeAmmount;
        this.shakeDuration = shakeDuration;
    }

    //Update Method
    private void Update()
    {
        if (shake)
        {
            if (shakeDuration > 0)
            {
                Camera.main.transform.localPosition = originalPos + (Random.insideUnitSphere * shakeAmmount);
                shakeDuration -= Time.deltaTime;
            }
            else
            {
                shake = false;
                Camera.main.transform.localPosition = originalPos;
            }
        }
    }
}
