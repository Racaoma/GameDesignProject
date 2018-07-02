using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevourerGameOver : MonoBehaviour
{
    //Variables
    private Vector2 target;
    private float speed;
    private bool rising;

    // Use this for initialization
    void Start ()
    {
        rising = true;
        target = new Vector2(-9.5f, this.transform.position.y);
        speed = ControllerManager.Instance.getDevourerController().speed / 2;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (rising)
        {
            if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) rising = false;
        }
        else
        {
            //Check if Has Arrive at Destination
            if (((Vector2)this.transform.position - target) == Vector2.zero) Destroy(this.gameObject);
            else this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        }
    }
}
