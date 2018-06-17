using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastRunes : MonoBehaviour
{
    //Lists
    public GameObject[] spheresList;
    public GameObject[] pathList;

    //Last Visited Sphere
    private int lastSphere = 10;

    //Disable CastRunes Method
    public void disableCastRunes()
    {
        checkCast();
        resetCastRunes();
        gameObject.SetActive(false);
    }

    //Activate Sphere Path
    public bool activatePath(GameObject sphere)
    {
        //Get Sphere ID
        int sphereID = sphere.GetComponent<RuneSphere>().sphereID;

        //If Stayed on Sphere
        if (sphereID == lastSphere) return false;

        //Moved Between Spheres
        if(sphereID == 1)
        {
            if (lastSphere == 3) pathList[1].SetActive(true);
            else if (lastSphere == 4) pathList[0].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if(sphereID == 2)
        {
            if (lastSphere == 3) pathList[2].SetActive(true);
            else if (lastSphere == 6) pathList[5].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 3)
        {
            if (lastSphere == 1) pathList[1].SetActive(true);
            else if (lastSphere == 2) pathList[2].SetActive(true);
            else if (lastSphere == 4) pathList[3].SetActive(true);
            else if (lastSphere == 6) pathList[6].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 4)
        {
            if (lastSphere == 1) pathList[0].SetActive(true);
            else if (lastSphere == 3) pathList[3].SetActive(true);
            else if (lastSphere == 5) pathList[4].SetActive(true);
            else if (lastSphere == 7) pathList[7].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 5)
        {
            if (lastSphere == 4) pathList[4].SetActive(true);
            else if (lastSphere == 8) pathList[8].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 6)
        {
            if (lastSphere == 2) pathList[5].SetActive(true);
            else if (lastSphere == 3) pathList[6].SetActive(true);
            else if (lastSphere == 7) pathList[9].SetActive(true);
            else if (lastSphere == 9) pathList[11].SetActive(true);
            else if (lastSphere == 10) pathList[12].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 7)
        {
            if (lastSphere == 4) pathList[7].SetActive(true);
            else if (lastSphere == 6) pathList[9].SetActive(true);
            else if (lastSphere == 8) pathList[10].SetActive(true);
            else if (lastSphere == 10) pathList[13].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 8)
        {
            if (lastSphere == 5) pathList[8].SetActive(true);
            else if (lastSphere == 7) pathList[10].SetActive(true);
            else if (lastSphere == 10) pathList[14].SetActive(true);
            else if (lastSphere == 11) pathList[15].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 9)
        {
            if (lastSphere == 6) pathList[11].SetActive(true);
            else if (lastSphere == 10) pathList[16].SetActive(true);
            else if (lastSphere == 12) pathList[18].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 10)
        {
            if (lastSphere == 6) pathList[12].SetActive(true);
            else if (lastSphere == 7) pathList[13].SetActive(true);
            else if (lastSphere == 8) pathList[14].SetActive(true);
            else if (lastSphere == 9) pathList[16].SetActive(true);
            else if (lastSphere == 11) pathList[17].SetActive(true);
            else if (lastSphere == 12) pathList[19].SetActive(true);
            else if (lastSphere == 13) pathList[20].SetActive(true);
            else if (lastSphere == 14) pathList[21].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 11)
        {
            if (lastSphere == 8) pathList[15].SetActive(true);
            else if (lastSphere == 10) pathList[17].SetActive(true);
            else if (lastSphere == 14) pathList[22].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 12)
        {
            if (lastSphere == 9) pathList[18].SetActive(true);
            else if (lastSphere == 10) pathList[19].SetActive(true);
            else if (lastSphere == 13) pathList[23].SetActive(true);
            else if (lastSphere == 16) pathList[25].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 13)
        {
            if (lastSphere == 10) pathList[20].SetActive(true);
            else if (lastSphere == 12) pathList[23].SetActive(true);
            else if (lastSphere == 14) pathList[24].SetActive(true);
            else if (lastSphere == 15) pathList[27].SetActive(true);
            else if (lastSphere == 18) pathList[26].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 14)
        {
            if (lastSphere == 10) pathList[21].SetActive(true);
            else if (lastSphere == 11) pathList[22].SetActive(true);
            else if (lastSphere == 13) pathList[24].SetActive(true);
            else if (lastSphere == 15) pathList[28].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 15)
        {
            if (lastSphere == 13) pathList[27].SetActive(true);
            else if (lastSphere == 14) pathList[28].SetActive(true);
            else if (lastSphere == 17) pathList[29].SetActive(true);
            else if (lastSphere == 18)
            {
                pathList[30].SetActive(true);
                pathList[31].SetActive(true);
            }
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 16)
        {
            if (lastSphere == 12) pathList[25].SetActive(true);
            else if (lastSphere == 18) pathList[33].SetActive(true);
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 17)
        {
            if (lastSphere == 15) pathList[29].SetActive(true);
            else if (lastSphere == 18)
            {
                pathList[30].SetActive(true);
                pathList[32].SetActive(true);
            }
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }
        else if (sphereID == 18)
        {
            if (lastSphere == 13) pathList[26].SetActive(true);
            else if (lastSphere == 15)
            {
                pathList[30].SetActive(true);
                pathList[31].SetActive(true);
            }
            else if (lastSphere == 16) pathList[33].SetActive(true);
            else if (lastSphere == 17)
            {
                pathList[30].SetActive(true);
                pathList[32].SetActive(true);
            }
            else
            {
                if (sphere.GetComponent<Animator>().isActiveAndEnabled) lastSphere = sphereID;
                return false;
            }
        }

        //Update Last Sphere
        lastSphere = sphereID;

        //Finally...
        return true;
    }

    //Check Cast
    private void checkCast()
    {
        bool[] activeSpheres = new bool[spheresList.Length];
        for(int i = 0; i < spheresList.Length; i++)
        {
            activeSpheres[i] = spheresList[i].GetComponent<Animator>().isActiveAndEnabled;
        }

        //Pass Results to Player
        Player.Instance.prepareSpell(SpellDatabase.checkRune(activeSpheres));
    }

    //Reset CastRunes Method
    public void resetCastRunes()
    {
        //Reset Spheres
        lastSphere = 10;
        for (int i = 0; i < spheresList.Length; i++)
        {
            spheresList[i].GetComponent<RuneSphere>().resetSphere();
        }

        //Reset Paths
        for (int i = 0; i < pathList.Length; i++)
        {
            pathList[i].SetActive(false);
        }
    }
}
