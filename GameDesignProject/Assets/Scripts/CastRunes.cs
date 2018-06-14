using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastRunes : MonoBehaviour
{
    //Spheres
    public GameObject[] spheresList;

    //Singleton
    private static CastRunes instance;

    //On Object Awake
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //On Object Destroy (Safeguard)
    public void OnDestroy()
    {
        instance = null;
    }

    //If Mouse Exit Area
    void OnMouseExit()
    {
        resetCastRunes();
    }

    //Reset CastRunes Method
    public void resetCastRunes()
    {
        //Reset Spheres
        for(int i = 0; i < spheresList.Length; i++)
        {
            spheresList[i].GetComponent<RuneSphere>().resetSphere();
        }

        //Disable CastRunes
        gameObject.SetActive(false);
    }

    
}
