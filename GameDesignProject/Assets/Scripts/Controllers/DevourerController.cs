using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevourerController : MonoBehaviour
{
    //Variables
    public GameObject devourer;
    public float speed;
    private Animator devourerAnimator;
    private bool rising;

    //Start & End Path
    private Vector2 endPath;
    private Vector2 startPath;
    private Vector2 deactivationPoint;

    private void Start()
    {
        rising = true;
        startPath = devourer.transform.position;
        endPath = new Vector2(devourer.transform.position.x + 14f, this.transform.position.y);
        deactivationPoint = new Vector2(devourer.transform.position.x + 12.5f, this.transform.position.y);
        devourerAnimator = devourer.GetComponent<Animator>();
    }

    //Activate Devourer
    public void activateDevourer()
    {
        devourer.SetActive(true);
    }

    //Deactive Devourer
    private void deactiveDevourer()
    {
        devourer.SetActive(false);
        devourer.transform.position = startPath;
        rising = true;
    }

    //Update Method
    private void Update()
    {
        if(devourer.activeInHierarchy)
        {
            if (rising)
            {
                if(devourerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) rising = false;
            }
            else
            {
                devourer.transform.position = Vector2.MoveTowards(devourer.transform.position, endPath, speed * Time.deltaTime);
                if ((deactivationPoint - (Vector2)devourer.transform.position).magnitude <= 0.1f) devourerAnimator.SetTrigger("End");
                else if ((endPath - (Vector2)devourer.transform.position).magnitude <= 0.1f) deactiveDevourer();
            }
        }
    }
}
