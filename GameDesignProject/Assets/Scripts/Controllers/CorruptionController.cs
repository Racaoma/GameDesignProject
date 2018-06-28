using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionController : MonoBehaviour
{
    //Varibles
    private int corruption;
    public Text corruptionText;

    //Start Method
    private void Start()
    {
        corruption = 0;
    }

    //Get Current Corruption
    public int getCorruption()
    {
        return corruption;
    }

    //Get Corruption Bonuses
    public int getCorruptionBonuses()
    {
        if (corruption < 20) return 0;
        else if (corruption >= 20 && corruption < 40) return 5;
        else if (corruption >= 40 && corruption < 60) return 10;
        else if (corruption >= 60 && corruption < 80) return 15;
        else return 20;
    }

    //Gain Corruption
    public void gainCorruption(int value)
    {
        if(value >= 0) Mathf.Min(value, 100);
        else corruption = Mathf.Max(value, 0);

        //Debug
        Debug.Log("Current Corruption: " + corruption);
    }
}
