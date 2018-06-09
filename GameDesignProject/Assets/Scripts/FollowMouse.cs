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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.Lerp(this.transform.position, mousePosition, moveSpeed);
    }
}
