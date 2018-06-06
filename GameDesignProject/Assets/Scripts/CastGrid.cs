using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastGrid : MonoBehaviour
{
    //Variables
    public float gridSize = 6f;
    private Vector2 lastPosition = Vector2.zero;

    //While Mouse is in Area
    private void OnMouseOver()
    {
        Vector2 currentPosition = Input.mousePosition;
        currentPosition = Camera.main.ScreenToWorldPoint(currentPosition);
        currentPosition = currentPosition - (Vector2) this.transform.position;

        if((currentPosition - lastPosition).magnitude >= 0.5f)
        {
            //Get Angle
            Vector2 atVector = currentPosition - lastPosition;
            float ang = Mathf.Atan2(atVector.y, atVector.x) * Mathf.Rad2Deg;

            //Debug.Log("FROM: " + lastPosition + " TO: " + currentPosition + " ANGLES: " + ang);








            //Update Last Position
            lastPosition = currentPosition;
        }
    }

    //If Mouse Exit Area
    void OnMouseExit()
    {
        Destroy(this.gameObject);
    }
}
