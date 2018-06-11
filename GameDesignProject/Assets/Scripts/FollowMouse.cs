using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    //Trail Speed
    public float moveSpeed;

    // Update is called once per frame
    void Update ()
    {
        //Check if Mouse is Close Enought to Sphere
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ((mousePosition - (Vector2)this.transform.position).magnitude <= 0.3f)
        {
            //Check Colliders
            Collider2D[] colliders = Physics2D.OverlapPointAll(mousePosition);
            for (int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].gameObject.layer == 9)
                {
                    transform.position = Vector2.Lerp(this.transform.position, mousePosition, moveSpeed);
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "RuneSphere") collision.GetComponent<Animator>().enabled = true;
    }
}
