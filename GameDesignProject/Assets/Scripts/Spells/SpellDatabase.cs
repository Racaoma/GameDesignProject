using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpellDatabase
{
    //Spell
    public static Spell flashFreezeSpell = new Spell(SpellName.FlashFreeze, SpellAreaType.Circle, 3, 25, 0, new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, true, false, false, false, false });
    public static Spell fireBlastSpell = new Spell(SpellName.FireBlast, SpellAreaType.Circle, 3, 40, 15, new bool[] { false, true, true, true, true, true, false, true, false, false, false, false, false, false, false, false, false });
    public static Spell lightningStrikeSpell = new Spell(SpellName.LightningStrike, SpellAreaType.Square, 1, 50, 100, new bool[] { false, false, false, false, false, false, false, false, false, false, false, true, false, true, true, true, true });
    public static Spell hurricaneSpell; //TODO
    public static Spell summonBeastsSpell; //TODO
    public static Spell meteorRainSpell; //TODO

    //Spell List
    public static Spell[] spells = { flashFreezeSpell, fireBlastSpell, lightningStrikeSpell };

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
