using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChangerController : MonoBehaviour
{
    //Mouse Cursor Sprites Reference
    public Texture2D flashFreezeCursor; 
    public Texture2D flashFreezeCursorNoMana;
    public Texture2D fireBlastCursor;
    public Texture2D fireBlastCursorNoMana;
    public Texture2D lightningStrikeCursor;
    public Texture2D lightningStrikeCursorNoMana;
    public Texture2D tornadoCursor;
    public Texture2D tornadoCursorNoMana;
    public Texture2D summonRainCursor;
    public Texture2D summonRainCursorNoMana;
    public Texture2D cleanseCursor;
    public Texture2D cleanseCursorNoMana;
    public Texture2D deepFreezeCursor;
    public Texture2D deepFreezeCursorNoMana;
    public Texture2D hellFireCursor;
    public Texture2D hellFireCursorNoMana;
    public Texture2D superboltCursor;
    public Texture2D superboltCursorNoMana;
    public Texture2D hurricaneCursor;
    public Texture2D hurricaneCursorNoMana;
    public Texture2D cloudburstCursor;
    public Texture2D cloudburstCursorNoMana;
    public Texture2D purifyCursor;
    public Texture2D purifyCursorNoMana;
    public Texture2D thunderStormCursor;
    public Texture2D thunderStormCursorNoMana;
    public Texture2D hailStormCursor;
    public Texture2D hailStormCursorNoMana;
    public Texture2D typhoonCursor;
    public Texture2D typhoonCursorNoMana;
    public Texture2D supercellCursor;
    public Texture2D supercellCursorNoMana;
    public Texture2D fireStormCursor;
    public Texture2D fireStormCursorNoMana;
    public Texture2D blizzardCursor;
    public Texture2D blizzardCursorNoMana;

    public void changeMouse(Spell spell, bool hasMana)
    {
        switch(spell.name)
        {
            case SpellName.FlashFreeze:
                if(hasMana) Cursor.SetCursor(flashFreezeCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(flashFreezeCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.FireBlast:
                if (hasMana) Cursor.SetCursor(fireBlastCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(fireBlastCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.LightningStrike:
                if (hasMana) Cursor.SetCursor(lightningStrikeCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(lightningStrikeCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.Tornado:
                if (hasMana) Cursor.SetCursor(tornadoCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(tornadoCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.SummonRain:
                if (hasMana) Cursor.SetCursor(summonRainCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(summonRainCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.Cleanse:
                if (hasMana) Cursor.SetCursor(cleanseCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(cleanseCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.DeepFreeze:
                if (hasMana) Cursor.SetCursor(deepFreezeCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(deepFreezeCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.Hellfire:
                if (hasMana) Cursor.SetCursor(hellFireCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(hellFireCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.Superbolt:
                if (hasMana) Cursor.SetCursor(superboltCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(superboltCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.Hurricane:
                if (hasMana) Cursor.SetCursor(hurricaneCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(hurricaneCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.Cloudburst:
                if (hasMana) Cursor.SetCursor(cloudburstCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(cloudburstCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.Purify:
                if (hasMana) Cursor.SetCursor(purifyCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(purifyCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.ThunderStorm:
                if (hasMana) Cursor.SetCursor(thunderStormCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(thunderStormCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.HailStorm:
                if (hasMana) Cursor.SetCursor(hailStormCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(hailStormCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.Typhoon:
                if (hasMana) Cursor.SetCursor(typhoonCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(typhoonCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.SuperCell:
                if (hasMana) Cursor.SetCursor(supercellCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(supercellCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.FireStorm:
                if (hasMana) Cursor.SetCursor(fireStormCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(fireStormCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.Blizzard:
                if (hasMana) Cursor.SetCursor(blizzardCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(blizzardCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    public void resetMouse()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
