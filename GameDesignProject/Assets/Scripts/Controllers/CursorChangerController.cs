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
        }
    }

    public void resetMouse()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
