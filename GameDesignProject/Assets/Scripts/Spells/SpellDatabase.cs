using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellName
{
    None,
    BlindingFlash,
    FireBlast,
    LightningStrike
};

public struct Spell
{
    public SpellName name;
    public bool[] spellRune;

    public Spell(SpellName name, bool[] spellRune)
    {
        this.name = name;
        this.spellRune = spellRune;
    }
}

public static class SpellDatabase
{
    //Spell
    public static Spell blindingFlashSpell = new Spell(SpellName.BlindingFlash, new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, false, false, false, false });
    public static Spell fireBlastRuneSpell = new Spell(SpellName.FireBlast, new bool[] { false, true, true, true, true, true, false, true, false, false, false, false, false, false, false, false, false });
    public static Spell lightningStrikeSpell = new Spell(SpellName.LightningStrike, new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, false, true, true, true, true });

    //Spell List
    public static Spell[] spells = { blindingFlashSpell, fireBlastRuneSpell, lightningStrikeSpell };

    //Compare Rune Method
    public static bool compareRune(Spell spellToCompare, bool[] drawedRune)
    {
        //If Any Discrepancy, Return False
        for(int i = 0; i < spellToCompare.spellRune.Length; i++)
        {
            //Debug.Log(spellToCompare.name + " - Sphere " + i + " - (Drawed,Ref) - (" + drawedRune[i] + ", " + spellToCompare.spellRune[i] + ")");
            if (spellToCompare.spellRune[i] != drawedRune[i]) return false;
        }

        //Finally...
        return true;
    }

    //Compare All Spells
    public static SpellName checkRune(bool[] drawedRune)
    {
        for(int i = 0; i < spells.Length; i++)
        {
            if (compareRune(spells[i], drawedRune)) return spells[i].name;
        }

        //Drawing Miss!
        Debug.Log("Miss!");
        return SpellName.None;
    }
}
