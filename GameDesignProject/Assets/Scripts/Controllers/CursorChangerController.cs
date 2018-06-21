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

    public Texture2D hurricaneCursor;
    public Texture2D hurricaneCursorNoMana;

    public Texture2D summonBeastsCursor;
    public Texture2D summonBeastsCursorNoMana;

    public Texture2D meteorRainCursor;
    public Texture2D meteorRainCursorNoMana;

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
            case SpellName.Hurricane:
                if (hasMana) Cursor.SetCursor(hurricaneCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(hurricaneCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.SummonBeasts:
                if (hasMana) Cursor.SetCursor(summonBeastsCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(summonBeastsCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.MeteorShower:
                if (hasMana) Cursor.SetCursor(meteorRainCursor, Vector2.zero, CursorMode.Auto);
                else Cursor.SetCursor(meteorRainCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    public void resetMouse()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
