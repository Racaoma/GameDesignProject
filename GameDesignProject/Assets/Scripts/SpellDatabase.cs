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
    public static Spell blindingFlashSpell = new Spell(SpellName.BlindingFlash, new bool[] { true, true, true, true, true, false, false, false, false, false, false, false, false, true, true, true, true, false });
    public static Spell fireBlastRuneSpell = new Spell(SpellName.FireBlast, new bool[] { true, false, false, false, false, true, true, false, false, false, false, true, true, true, true, false, false, false });
    public static Spell lightningStrikeSpell = new Spell(SpellName.LightningStrike, new bool[] { true, false, false, true, false, false, false, false, true, true, true, false, false, false, false, true, false, true });

    //Spell List
    public static Spell[] spells = { blindingFlashSpell, fireBlastRuneSpell, lightningStrikeSpell };

    //Compare Rune Method
    public static bool compareRune(Spell spellToCompare, bool[] drawedRune)
    {
        for(int i = 0; i < spellToCompare.spellRune.Length; i++)
        {
            if (spellToCompare.spellRune[i] != drawedRune[i]) return false;
        }

        return true;
    }

    //Compare All Spells
    public static SpellName checkRune(bool[] drawedRune)
    {
        for(int i = 0; i < spells.Length; i++)
        {
            if (compareRune(spells[i], drawedRune))
            {
                Debug.Log(spells[i].name);
                return spells[i].name;
            }
        }

        //Drawing Miss!
        Debug.Log("Miss!");
        return SpellName.None;
    }
}
