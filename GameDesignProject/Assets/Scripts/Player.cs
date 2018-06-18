using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //CastRunes Reference
    public GameObject castRunes;

    //Spell Range Overlay Reference
    public SpellRangeOverlay spellRangeOverlay;

    //Prepared Spell
    private SpellName preparedSpell = SpellName.None;

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

    //Prepare Spell
    public void prepareSpell(SpellName spell)
    {
        if(spell != SpellName.None) Debug.Log("Prepared " + spell);
        preparedSpell = spell;
    }

    //On Mouse Click
    private void OnMouseDown()
    {
        if (preparedSpell != SpellName.None) Debug.Log("Canceled " + preparedSpell);
        preparedSpell = SpellName.None;
        castRunes.SetActive(true);
    }

    //Set Spell Range Overlay
    public void setSpellRangeOverlay()
    {
        //Set Spell Range Overlay
        switch (preparedSpell)
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

        //Flash Freeze
        if(preparedSpell == SpellName.FlashFreeze) 
        {
            for (int i = 0; i < affectedArea.Length; i++)
            {
                Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[i], Vector2.one, 0f);
                for(int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.tag == "Enemy") collisions[j].gameObject.GetComponent<Enemy>().setCondition(Conditions.Frozen, 3f);
                }
            }
        }
    }

    //Update Method
    private void Update()
    {
        //If Mouse is Released
        if (Input.GetMouseButtonUp(0) && castRunes.activeInHierarchy) castRunes.GetComponent<CastRunes>().disableCastRunes();
        else if(preparedSpell != SpellName.None)
        {
            //Set Spell Range Overlay
            setSpellRangeOverlay();

            //Check Mouse Click
            if (Input.GetMouseButtonDown(0))
            {
                //Cast Spell
                castSpell();

                //Reset Prepared Spell
                preparedSpell = SpellName.None;

                //Reset Spell Range Overlay
                spellRangeOverlay.disableSpellOverlay();
            }
        }
    }
}
