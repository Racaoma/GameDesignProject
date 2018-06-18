using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellName
{
    None,
    FlashFreeze,
    FireBlast,
    LightningStrike
};

public enum SpellAreaType
{
    Square,
    Circle,
    Cross,
    Line
};

public struct Spell
{
    public SpellName name;
    public SpellAreaType areaType;
    public int spellRange;
    public bool[] spellRune;

    public Spell(SpellName name, SpellAreaType areaType, int spellRange, bool[] spellRune)
    {
        this.name = name;
        this.areaType = areaType;
        this.spellRune = spellRune;
        this.spellRange = spellRange;
    }
}

public static class SpellDatabase
{
    //Spell
    public static Spell flashFreezeSpell = new Spell(SpellName.FlashFreeze, SpellAreaType.Circle, 3, new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, false, false, false, false });
    public static Spell fireBlastRuneSpell = new Spell(SpellName.FireBlast, SpellAreaType.Circle, 3, new bool[] { false, true, true, true, true, true, false, true, false, false, false, false, false, false, false, false, false });
    public static Spell lightningStrikeSpell = new Spell(SpellName.LightningStrike, SpellAreaType.Square, 1, new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, false, true, true, true, true });

    //Spell List
    public static Spell[] spells = { flashFreezeSpell, fireBlastRuneSpell, lightningStrikeSpell };

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
