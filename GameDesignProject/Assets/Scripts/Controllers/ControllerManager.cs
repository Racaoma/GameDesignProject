using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    //Controller References
    private CursorChangerController cursorChangerController;
    private SpellEffectController spellEffectController;

    private void Start()
    {
        cursorChangerController = this.GetComponent<CursorChangerController>();
        spellEffectController = this.GetComponent<SpellEffectController>();
    }

    public CursorChangerController getCursorChangerController()
    {
        return cursorChangerController;
    }

    public SpellEffectController getSpellEffectController()
    {
        return spellEffectController;
    }
}
