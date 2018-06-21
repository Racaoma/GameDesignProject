using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChanger : MonoBehaviour
{
    //Mouse Cursor Sprites Reference
    public Texture2D flashFreezeCursor; 
    public Texture2D flashFreezeCursorNoMana;

    public Texture2D fireBlastCursor;
    public Texture2D fireBlastCursorNoMana;

    public Texture2D lightningStrikeCursor;
    public Texture2D lightningStrikeCursorNoMana;

    public void changeMouse(Spell spell, bool hasMana)
    {
        switch(spell.name)
        {
            case SpellName.FlashFreeze:
                if(hasMana) Cursor.SetCursor(flashFreezeCursor, Vector2.zero, CursorMode.Auto);
                //else Cursor.SetCursor(flashFreezeCursorNoMana, Vector2.zero, CursorMode.Auto);
                break;
            case SpellName.FireBlast:
                break;
            case SpellName.LightningStrike:
                break;
        }
    }

    public void resetMouse()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
