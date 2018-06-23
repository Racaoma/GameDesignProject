using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    //Controller References
    private CursorChangerController cursorChangerController;
    private SpellEffectController spellEffectController;
    private ManaController manaController;
    private ScreenShakeController screenShakeController;
    private TextBalloonController textBalloonController;

    //Start Method
    private void Start()
    {
        cursorChangerController = this.GetComponent<CursorChangerController>();
        spellEffectController = this.GetComponent<SpellEffectController>();
        manaController = this.GetComponent<ManaController>();
        screenShakeController = this.GetComponent<ScreenShakeController>();
        textBalloonController = this.GetComponent<TextBalloonController>();
    }

    //Get Methods
    public CursorChangerController getCursorChangerController()
    {
        return cursorChangerController;
    }

    public SpellEffectController getSpellEffectController()
    {
        return spellEffectController;
    }

    public ManaController getManaController()
    {
        return manaController;
    }

    public ScreenShakeController getScreenShakeController()
    {
        return screenShakeController;
    }

    public TextBalloonController getTextBalloonController()
    {
        return textBalloonController;
    }
}
