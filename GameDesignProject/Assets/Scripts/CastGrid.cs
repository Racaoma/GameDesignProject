using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastGrid : MonoBehaviour
{
    //Variables
    private Vector2 lastPosition;
    private float lastAngle;
    private bool firstAdd;

    //Angle Error Margin
    private float angleErrorMargin = 20f;

    //Points List
    private List<Vector2> points;

    //Start Method
    void Start()
    {
        firstAdd = true;
        lastAngle = 0f;
        lastPosition = Vector2.zero;
        points = new List<Vector2>();
        points.Add(Vector2.zero);
    }

    //While Mouse is in Area
    private void OnMouseOver()
    {
        Vector2 currentPosition = Input.mousePosition;
        currentPosition = Camera.main.ScreenToWorldPoint(currentPosition);
        currentPosition = currentPosition - (Vector2) this.transform.position;

        //Check if Point is Sufficiently Distant from Last Point
        if (Mathf.Abs(currentPosition.x - lastPosition.x) >= 0.1f || Mathf.Abs(currentPosition.y - lastPosition.y) >= 0.1f)
        {
            //Get Angle
            Vector2 atVector = currentPosition - lastPosition;
            float currentAngle = Mathf.Abs(Mathf.Atan2(atVector.y, atVector.x) * Mathf.Rad2Deg) % 180f;

            //Check if There is a Significant Change in Angles
            if ((Mathf.Abs(currentAngle - lastAngle) >= angleErrorMargin) && ((currentAngle + lastAngle + angleErrorMargin) % 180 >= angleErrorMargin))
            {
                //Check for Angle Errors
                if (!((lastPosition.x == currentPosition.x || lastPosition.y == currentPosition.y) && (currentAngle % 90f != 0)))
                {
                    //If Not First Change, Add Point to List
                    if (!firstAdd)
                    {
                        //Debug.Log("LAST POINT: " + lastPosition + " LAST ANGLE: " + lastAngle + "------- CUR POINT: " + currentPosition + " CUR ANGLE: " + currentAngle);
                        points.Add(lastPosition);
                    }
                    else firstAdd = false;
                }
            }

            //Update Last Angle
            lastAngle = currentAngle;
        }

        //Update Last Position
        lastPosition = currentPosition;
    }

    //On Destroy Method
    public void OnDestroy()
    {
        //Add Last Point
        Vector2 currentPosition = Input.mousePosition;
        currentPosition = Camera.main.ScreenToWorldPoint(currentPosition);
        currentPosition = currentPosition - (Vector2)this.transform.position;
        points.Add(currentPosition);

        //Check Drawed Rune
        SpellDatabase.checkRune(points);
    }

    //If Mouse Exit Area
    void OnMouseExit()
    {
        Destroy(this.gameObject);
    }
}
