using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellName
{
    None,
    FlashFreeze,
    FireBlast,
    LightningStrike,
    Hurricane,
    SummonBeasts,
    MeteorShower
};

public enum SpellAreaType
{
    Square,
    Circle,
    Cross,
    Line
};

public class Spell
{
    public SpellName name;
    public SpellAreaType areaType;
    public int spellRange;
    public int manaCost;
    public int damage;
    public bool[] spellRune;

    public Spell(SpellName name, SpellAreaType areaType, int spellRange, int manaCost, int damage, bool[] spellRune)
    {
        this.name = name;
        this.areaType = areaType;
        this.spellRune = spellRune;
        this.manaCost = manaCost;
        this.damage = damage;
        this.spellRange = spellRange;
    }
}
