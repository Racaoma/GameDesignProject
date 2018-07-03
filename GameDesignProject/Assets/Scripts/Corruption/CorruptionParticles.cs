using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionParticles : MonoBehaviour
{
    //Variables
    private Vector2 target;
    private float speed;
    private float lingeringTime;
    private int corruption;

	// Use this for initialization
	void Start ()
    {
        target = Player.Instance.transform.position;
        speed = 1.5f;
        lingeringTime = 0.5f;
        corruption = 1;
    }

    //Set Corruption
    public void setCorruption(int corruptionVal)
    {
        corruption = corruptionVal;
    }

    // Update is called once per frame
    void Update ()
    {
        if (((Vector2)this.transform.position - target) == Vector2.zero)
        {
            lingeringTime -= Time.deltaTime;
            if (lingeringTime <= 0f)
            {
                ControllerManager.Instance.getCorruptionController().gainCorruption(corruption);
                Destroy(this.gameObject);
            }
        }
        else this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
	}
}
