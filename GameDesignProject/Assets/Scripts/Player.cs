using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //CastRunes Reference
    public CastRunes castRunes;

    //On Mouse Click
    private void OnMouseDown()
    {
        castRunes.gameObject.SetActive(true);
    }

    //Update Method
    private void Update()
    {
        //Reset CastRunes if Mouse is Released
        if(Input.GetMouseButtonUp(0) && castRunes.gameObject.activeInHierarchy) castRunes.resetCastRunes();
    }
}
