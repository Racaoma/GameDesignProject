using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //References
    public GameObject castRunes;
    public SpellRangeOverlay spellRangeOverlay;
    public MouseChanger mouseChanger;

    //Prepared Spell
    private Spell preparedSpell;

    //Singleton Instance Variable
    private static Player instance;
    public static Player Instance
    {
        get
        {
            return instance;
        }
    }

    //On Object Awake
    private void Awake()
    {
        //Check Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //On Object Destroy (Safeguard)
    public void OnDestroy()
    {
        instance = null;
    }

    //Start Method
    private void Start()
    {
        preparedSpell = null;
    }

    //Prepare Spell
    public void prepareSpell(Spell spell)
    {
        preparedSpell = spell;
    }

    //On Mouse Click
    private void OnMouseDown()
    {
        preparedSpell = null;
        castRunes.SetActive(true);
    }

    //Set Spell Range Overlay
    public void setSpellRangeOverlay()
    {
        //Set Spell Range Overlay
        switch (preparedSpell.name)
        {
            case SpellName.FlashFreeze:
                spellRangeOverlay.setSpellOverlay(SpellDatabase.flashFreezeSpell.areaType, SpellDatabase.flashFreezeSpell.spellRange, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                break;
            case SpellName.FireBlast:
                spellRangeOverlay.setSpellOverlay(SpellDatabase.fireBlastRuneSpell.areaType, SpellDatabase.fireBlastRuneSpell.spellRange, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                break;
            case SpellName.LightningStrike:
                spellRangeOverlay.setSpellOverlay(SpellDatabase.lightningStrikeSpell.areaType, SpellDatabase.lightningStrikeSpell.spellRange, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                break;
        }
    }

    //Cast Spell Logic
    public void castSpell()
    {
        //Get Affected Positions
        Vector2[] affectedArea = spellRangeOverlay.getAffectedArea();

        //Spend Mana
        ManaController.Instance.spendMana(preparedSpell.manaCost);

        //Flash Freeze
        if (preparedSpell.name == SpellName.FlashFreeze) 
        {
            for (int i = 0; i < affectedArea.Length; i++)
            {
                Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[i], Vector2.one, 0f);
                for(int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.tag == "Enemy") collisions[j].gameObject.GetComponent<Enemy>().setCondition(Condition.Frozen, 3f);
                }
            }
        }

        //Reset Prepared Spell
        preparedSpell = null;

        //Reset Spell Range Overlay
        spellRangeOverlay.disableSpellOverlay();

        //Reset Cursor
        mouseChanger.resetMouse();
    }

    //Update Method
    private void Update()
    {
        //If Mouse is Released
        if (Input.GetMouseButtonUp(0) && castRunes.activeInHierarchy) castRunes.GetComponent<CastRunes>().disableCastRunes();
        else if(preparedSpell != null)
        {
            //Check Mana Levels
            bool hasMana = ManaController.Instance.getCurrentMana() >= preparedSpell.manaCost;

            //Update Cursor
            mouseChanger.changeMouse(preparedSpell, hasMana);

            //Set Spell Range Overlay
            setSpellRangeOverlay();

            //Check Mouse Click & Check if You have Enough Mana
            if (Input.GetMouseButtonDown(0) && hasMana) castSpell();
        }
    }
}
