using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastGrid : MonoBehaviour
{
    //Spheres
    public GameObject[] spheres;

    //On Destroy Method
    public void OnDestroy()
    {
        //List Active Spheres
        bool[] activeSpheres = new bool[18];
        for(int i = 0; i < spheres.Length; i++)
        {
            activeSpheres[i] = spheres[i].GetComponent<Animator>().enabled;
        }

        //Check Drawed Rune
        SpellDatabase.checkRune(activeSpheres);
    }

    //If Mouse Exit Area
    void OnMouseExit()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
