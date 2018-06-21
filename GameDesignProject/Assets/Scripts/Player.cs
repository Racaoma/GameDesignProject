using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //References
    public GameObject castRunes;
    public SpellRangeOverlay spellRangeOverlay;
    public CursorChangerController mouseChanger;
    public GameObject scenario;

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
        animator = this.GetComponent<Animator>();
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
                Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[i], Vector2.one, 0f);
                for (int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.tag == "Enemy") collisions[j].gameObject.GetComponent<Enemy>().setCondition(Condition.Frozen, 3f);
                }
            }
        }
        //Flash Freeze
        else if (preparedSpell.name == SpellName.FireBlast)
        {
            //Animate
            animator.SetInteger("Spell", 1);
            animator.SetTrigger("Cast");

            for (int i = 0; i < affectedArea.Length; i++)
            {
                //Set Up Effect in Tile
                int verticalCell = Mathf.FloorToInt(affectedArea[i].y + 3.75f) + 1;
                int horizontalCell = Mathf.FloorToInt(affectedArea[i].x + 8.25f) + 1;
                scenario.transform.GetChild((verticalCell * 18) + horizontalCell).gameObject.GetComponent<TileEffectController>().setEffect(preparedSpell);
                Debug.Log(affectedArea[i] + " - " + ((verticalCell * 18) + horizontalCell));

                //Check for Affected Enemies
                Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[i], Vector2.one, 0f);
                for (int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.tag == "Enemy") collisions[j].gameObject.GetComponent<Enemy>().takeDamage(preparedSpell.damage);
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
            spellRangeOverlay.setSpellOverlay(preparedSpell.areaType, preparedSpell.spellRange, Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //Check Mouse Click & Check if You have Enough Mana
            if (Input.GetMouseButtonDown(0) && hasMana) castSpell();
        }
    }
}
