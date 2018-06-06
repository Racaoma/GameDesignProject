using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject castGrid;
    private GameObject currentGrid;

    // Update is called once per frame
    void Update () 
    {
        //On Mouse Click
		if(Input.GetMouseButtonDown(0))
        {
            //Get Position of Mouse
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0f;

            //Create Grid
            currentGrid = Instantiate(castGrid, worldPosition, Quaternion.identity);
        }

        //On Mouse Release
        if(Input.GetMouseButtonUp(0))
        {
            Destroy(currentGrid.gameObject);
        }
	}
}
