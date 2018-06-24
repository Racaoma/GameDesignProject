﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //References
    public GameObject castRunes;
    public SpellRangeOverlay spellRangeOverlay;

    //Internal References
    private Animator animator;
    private GameObject textBalloon;
    private GameObject alert;

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
        textBalloon = this.transform.GetChild(0).gameObject;
        alert = this.transform.GetChild(1).gameObject;
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
        ControllerManager.Instance.getManaController().spendMana(preparedSpell.manaCost);

        //Remove Text Balloon
        textBalloon.SetActive(false);

        //Spell Logic
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
                    if (collisions[j].gameObject.tag == "Enemy") collisions[j].gameObject.GetComponent<Enemy>().setCondition(Condition.Frozen, 5f);
                }
            }
        }
        else if (preparedSpell.name == SpellName.FireBlast)
        {
            //Animate
            animator.SetInteger("Spell", 1);
            animator.SetTrigger("Cast");
            ControllerManager.Instance.getScreenShakeController().screenShake(0.1f, 0.2f);

            for (int i = 0; i < affectedArea.Length; i++)
            {
                if (affectedArea[i] == (Vector2) this.transform.position) continue;

                //Spawn Fire
                ControllerManager.Instance.getSpellEffectController().spawnEffect(preparedSpell, affectedArea[i]);

                //Check for Affected Enemies
                Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[i], new Vector2(0.95f, 0.95f), 0f);
                for (int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.tag == "Enemy") collisions[j].gameObject.GetComponent<Enemy>().takeDamage(preparedSpell);
                }
            }
        }
        else if(preparedSpell.name == SpellName.Hurricane)
        {
            //Animate
            animator.SetInteger("Spell", 3);
            animator.SetTrigger("Cast");

            //Spawn Hurricane
            ControllerManager.Instance.getSpellEffectController().spawnEffect(preparedSpell, new Vector2(affectedArea[0].x - 0.1f, affectedArea[0].y + 0.3f));
        }
           
        //Reset Prepared Spell
        preparedSpell = null;

        //Reset Spell Range Overlay
        spellRangeOverlay.disableSpellOverlay();

        //Reset Cursor
        ControllerManager.Instance.getCursorChangerController().resetMouse();
    }

    //Fixed Update
    private void FixedUpdate()
    {
        bool enemyInRange = false;
        RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(this.transform.position.x + 0.51f, this.transform.position.y), Vector2.right, 3f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject.tag == "Enemy")
            {
                enemyInRange = true;
                if (hit[i].distance < 1f) ControllerManager.Instance.getDevourerController().activateDevourer();
            }
        }

        //Setup Alert
        alert.SetActive(enemyInRange);
    }

    //Update Method
    private void Update()
    {
        //If Mouse is Released
        if (Input.GetMouseButtonUp(0) && castRunes.activeInHierarchy) castRunes.GetComponent<CastRunes>().disableCastRunes();
        else if(preparedSpell != null)
        {
            //Check Mana Levels
            bool hasMana = ControllerManager.Instance.getManaController().getCurrentMana() >= preparedSpell.manaCost;

            //Update Text Ballon
            textBalloon.SetActive(!hasMana);

            //Update Cursor
            ControllerManager.Instance.getCursorChangerController().changeMouse(preparedSpell, hasMana);

            //Set Spell Range Overlay
            spellRangeOverlay.setSpellOverlay(preparedSpell.areaType, preparedSpell.spellRange, Camera.main.ScreenToWorldPoint(Input.mousePosition));

            //Check Mouse Click & Check if You have Enough Mana
            if (Input.GetMouseButtonDown(0) && hasMana) castSpell();
        }
    }
}
