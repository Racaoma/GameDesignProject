using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AngleType
{
    None,
    NearRight,
    Acute
};

public class CastGrid : MonoBehaviour
{
    //Variables
    public float gridSize = 6f;
    private Vector2 lastPosition;
    private AngleType lastAngle;

    //Angle Error Margin
    private float angleErrorMargin = 15f;

    //Points List
    private List<Vector2> points;

    //Start Method
    void Start()
    {
        lastAngle = AngleType.None;
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

        if(Mathf.Abs(currentPosition.x - lastPosition.x) >= 0.2f || Mathf.Abs(currentPosition.y - lastPosition.y) >= 0.2f)
        {
            //Get Angle
            Vector2 atVector = currentPosition - lastPosition;
            float ang = Mathf.Abs(Mathf.Atan2(atVector.y, atVector.x) * Mathf.Rad2Deg) % 180f;

            Debug.Log("POINT: " + currentPosition + " ANGLE: " + ang);

            //Add Point to List
            if ((lastAngle == AngleType.None || lastAngle == AngleType.Acute) && ((ang < angleErrorMargin || ang > (180f - angleErrorMargin)) || (ang > (90f - angleErrorMargin) && ang < (90f + angleErrorMargin))))
            {
                //Debug.Log("POINT: " + currentPosition + " ANGLE: " + ang);
                Debug.Log("ADDED");
                points.Add(currentPosition);
                lastAngle = AngleType.NearRight;
            }
            else if(ang >= angleErrorMargin && ang <= (180f - angleErrorMargin))
            {
                //Debug.Log("POINT: " + currentPosition + " ANGLE: " + ang);
                Debug.Log("ADDED");
                points.Add(currentPosition);
                lastAngle = AngleType.Acute;
            }

            //Update Last Position
            lastPosition = currentPosition;
        }
    }

    //DEBUG
    public void OnDestroy()
    {
        //Add Last Point
        Vector2 currentPosition = Input.mousePosition;
        currentPosition = Camera.main.ScreenToWorldPoint(currentPosition);
        currentPosition = currentPosition - (Vector2)this.transform.position;
        points.Add(currentPosition);

        //DEBUG
        for (int i = 0; i < points.Count; i++)
        {
            //Debug.Log(points[i]);
        }
    }

    //If Mouse Exit Area
    void OnMouseExit()
    {
        Destroy(this.gameObject);
    }
}
