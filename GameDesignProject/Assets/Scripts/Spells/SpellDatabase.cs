using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpellDatabase
{
    //Basic Spells
    public static Spell flashFreezeSpell = new Spell(SpellName.FlashFreeze, SpellAreaType.Circle, 3, 25, 0, new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, false, false, false, false });
    public static Spell fireBlastSpell = new Spell(SpellName.FireBlast, SpellAreaType.Circle, 3, 30, 15, new bool[] { false, true, true, true, true, true, false, true, false, false, false, false, false, false, false, false, false });
    public static Spell lightningStrikeSpell = new Spell(SpellName.LightningStrike, SpellAreaType.Circle, 1, 50, 100, new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, false, true, true, true, true });
    public static Spell tornadoSpell = new Spell(SpellName.Tornado, SpellAreaType.Circle, 1, 50, 5, new bool[] { true, false, true, true, false, true, true, false, false, false, false, true, true, true, false, false, true });
    public static Spell summonRainSpell = new Spell(SpellName.SummonRain, SpellAreaType.Random, 10, 50, 0, new bool[] { false, true, true, true, true, true, true, true, true, false, true, true, false, false, false, false, false });
    public static Spell cleanseSpell = new Spell(SpellName.Cleanse, SpellAreaType.All, 1, 50, 10, new bool[] { false, false, false, false, false, false, true, false, true, true, false, true, false, false, false, false, true });

    //Improved Spells
    public static Spell deepFreezeSpell = new Spell(SpellName.DeepFreeze, SpellAreaType.Circle, 3, 40, 0, null);    //Longer Freeze
    public static Spell hellFireSpell = new Spell(SpellName.Hellfire, SpellAreaType.Circle, 3, 45, 15, null);       //Burning Condition
    public static Spell superboltSpell = new Spell(SpellName.Superbolt, SpellAreaType.Circle, 1, 70, 100, null);    //Stun Adjacent
    public static Spell hurricaneSpell = new Spell(SpellName.Hurricane, SpellAreaType.Circle, 1, 70, 10, null);     //Bigger, More Damaging
    public static Spell cloudBurstSpell = new Spell(SpellName.Cloudburst, SpellAreaType.Random, 20, 65, 0, null);   //Generate More Puddles
    public static Spell purifySpell = new Spell(SpellName.Purify, SpellAreaType.All, 1, 65, 20, null);              //Greater Heal

    //Combined Spells
    public static Spell thunderStormSpell = new Spell(SpellName.ThunderStorm, SpellAreaType.Random, 5, 80, 100, null);  //5 Random Lightning Strikes
    public static Spell hailStormSpell = new Spell(SpellName.ThunderStorm, SpellAreaType.All, 1, 60, 1, null);          //Low Damage to All Enemies
    public static Spell typhoonSpell = new Spell(SpellName.Typhoon, SpellAreaType.Circle, 3, 80, 5, null);              //Combined Spells
    public static Spell superCellSpell = new Spell(SpellName.SuperCell, SpellAreaType.Circle, 1, 70, 5, null);          //Combined Spells
    public static Spell fireStormSpell = new Spell(SpellName.FireStorm, SpellAreaType.Circle, 1, 70, 5, null);          //Burning Condition
    public static Spell blizzardSpell = new Spell(SpellName.Blizzard, SpellAreaType.All, 1, 70, 5, null);               //Greater Slow

    //Spell List to Compare (Basic Spells)
    public static Spell[] spells = { flashFreezeSpell, fireBlastSpell, lightningStrikeSpell, tornadoSpell, summonRainSpell, cleanseSpell };

    //Compare Rune Method
    public static bool compareRune(Spell spellToCompare, bool[] drawedRune)
    {
        //If Any Discrepancy, Return False
        for(int i = 0; i < spellToCompare.spellRune.Length; i++)
        {
            if (spellToCompare.spellRune[i] != drawedRune[i]) return false;
        }

        //Finally...
        return true;
    }

    //Compare All Spells
    public static Spell checkRune(bool[] drawedRune)
    {
        for(int i = 0; i < spells.Length; i++)
        {
            if (compareRune(spells[i], drawedRune)) return spells[i];
        }

        //Drawing Miss!
        return null;
    }
}
