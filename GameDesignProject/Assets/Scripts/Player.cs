using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //References
    public GameObject castRunes;
    public SpellRangeOverlay spellRangeOverlay;
    public ControllerManager controllerManager;

    //Internal References
    private Animator animator;

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
        animator = this.GetComponent<Animator>();
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
        spellRangeOverlay.disableSpellOverlay();
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
            //Animate
            animator.SetInteger("Spell", 0);
            animator.SetTrigger("Cast");

            for (int i = 0; i < affectedArea.Length; i++)
            {
                Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[i], new Vector2(0.95f, 0.95f), 0f);
                for (int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.tag == "Enemy") collisions[j].gameObject.GetComponent<Enemy>().setCondition(Condition.Frozen, 3f);
                }
            }
        }
        //Fire Blast
        else if (preparedSpell.name == SpellName.FireBlast)
        {
            //Animate
            animator.SetInteger("Spell", 1);
            animator.SetTrigger("Cast");

            for (int i = 0; i < affectedArea.Length; i++)
            {
                if (affectedArea[i] == (Vector2) this.transform.position) continue;

                //Spawn Fire
                controllerManager.getSpellEffectController().spawnEffect(preparedSpell, affectedArea[i]);

                //Check for Affected Enemies
                Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[i], new Vector2(0.95f, 0.95f), 0f);
                for (int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.tag == "Enemy") collisions[j].gameObject.GetComponent<Enemy>().takeDamage(preparedSpell);
                }
            }
        }

           
        //Reset Prepared Spell
        preparedSpell = null;

        //Reset Spell Range Overlay
        spellRangeOverlay.disableSpellOverlay();

        //Reset Cursor
        controllerManager.getCursorChangerController().resetMouse();
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
            controllerManager.getCursorChangerController().changeMouse(preparedSpell, hasMana);

            //Set Spell Range Overlay
            spellRangeOverlay.setSpellOverlay(preparedSpell.areaType, preparedSpell.spellRange, Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //Check Mouse Click & Check if You have Enough Mana
            if (Input.GetMouseButtonDown(0) && hasMana) castSpell();
        }
    }
}
