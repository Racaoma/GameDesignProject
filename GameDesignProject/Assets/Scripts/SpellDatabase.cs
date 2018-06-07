using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Spell
{
    None,
    FireBlast,
    LightningStrike
};

public static class SpellDatabase
{
    //Grid Size
    public static float gridSize = 6f;
    public static float errorMargin = 2f;

    //Spell Runes
    public static Vector2[] fireBlastRune = { Vector2.zero, new Vector2(-gridSize/2,-gridSize/2), new Vector2(-gridSize/2, gridSize/2), new Vector2(gridSize/2, gridSize/2), new Vector2(gridSize/2, -gridSize/2) };
    public static Vector2[] lightningStrikeRune = { Vector2.zero, new Vector2(-gridSize/2, gridSize/2), new Vector2(gridSize/2, gridSize/2), new Vector2(-gridSize/2, -gridSize/2), new Vector2(gridSize/2, -gridSize/2) };
    //TODO: Continue

    //Compare Rune Method
    public static Spell checkRune(List<Vector2> drawed)
    {
        //Rune Precision Variables
        float fireBlastPrecision = 0f;
        float lightningStrikePrecision = 0f;

        //Spell Iterators
        int fireblastIterator = 0;
        int lightningStrikeIterator = 0;

        //Test Distance to Preset Runes
        for (int i = 0; i < drawed.Count; i++)
        {
            //FireBlast
            float distanceFireBlast = (drawed[i] - fireBlastRune[fireblastIterator]).magnitude;
            if (distanceFireBlast <= errorMargin)
            {
                fireBlastPrecision += distanceFireBlast;
                fireblastIterator++;
            }

            //Lightning Strike
            float distanceLightningStrike = (drawed[i] - lightningStrikeRune[lightningStrikeIterator]).magnitude;
            if ((drawed[i] - lightningStrikeRune[lightningStrikeIterator]).magnitude <= errorMargin)
            {
                lightningStrikePrecision += distanceLightningStrike;
                lightningStrikeIterator++;
            }

            //Check if Drawed Rune Correspond to Any Spell
            if (fireblastIterator == fireBlastRune.Length)
            {
                Debug.Log("FIREBLAST!");
                return Spell.FireBlast;
            }
            else if(lightningStrikeIterator == lightningStrikeRune.Length)
            {
                Debug.Log("LIGHTNING STRIKE!");
                return Spell.LightningStrike;
            }
        }

        //Drawing Miss!
        return Spell.None;
    }
}
