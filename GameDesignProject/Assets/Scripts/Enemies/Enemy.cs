using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Condition
{
    None,
    Stunned,
    Frozen,
    Ablaze,
    Slowed
}

public class Enemy : MonoBehaviour
{
    //Variables
    public Image healthBar;
    public int maxHitPoints;
    public float speed;
    public int spawnCost;
    public Action<GameObject> onDeathAction;
    private Vector2 target;

    //Damage Control
    private int currentHitPoints;
    private float damageGrace;
    private SpellName lastDamageSource;

    //Conditions Animator & Sprite Renderer Reference
    public Animator conditionAnimator;
    public SpriteRenderer conditionSpriteRenderer;

    //Conditions
    private Condition currentCondition;
    private float remainingConditionTime;

    //Conditions Animations
    public RuntimeAnimatorController frozenAnimation;

    //Start Method
    private void Start()
    {
        resetInternalVariables();
        GameObject child = this.transform.GetChild(0).gameObject;
        conditionAnimator = child.GetComponent<Animator>();
        conditionSpriteRenderer = child.GetComponent<SpriteRenderer>();
    }

    //On Enable Method
    private void OnEnable()
    {
        target = new Vector2(-9.5f, this.transform.position.y);
    }

    //Reset Internal Variables Method
    private void resetInternalVariables()
    {
        healthBar.fillAmount = 1f;
        currentHitPoints = maxHitPoints;
        lastDamageSource = SpellName.None;
        damageGrace = 0f;
        clearConditions();
    }

    //Kill Enemy
    public void killEnemy()
    {
        resetInternalVariables();
        this.transform.position = Vector2.zero;
        if (onDeathAction != null) onDeathAction(this.gameObject);
    }

    //Take Damage
    public void takeDamage(Spell spell)
    {
        if(lastDamageSource != spell.name)
        {
            damageGrace = 1f;
            lastDamageSource = spell.name;
            currentHitPoints -= spell.damage;
            healthBar.fillAmount = (float) currentHitPoints / (float) maxHitPoints;
            if (currentHitPoints <= 0) killEnemy();
        }
    }

    //Enemy Arrived at Destination
    public void retireEnemy()
    {
        //TODO: reduce score/lifes
        killEnemy();
    }

    //Action Safeguard
    private void OnDestroy()
    {
        onDeathAction = null;
    }

    //Set Condition Method
    public void setCondition(Condition condition, float time)
    {
        //Set Condition
        currentCondition = condition;
        remainingConditionTime = time;

        //Change Animation
        switch (condition)
        {
            case Condition.Frozen:
                conditionAnimator.runtimeAnimatorController = frozenAnimation;
                break;
            case Condition.Stunned:
                break;
            case Condition.Ablaze:
                break;
        }
    }

    //Clear Conditions Method
    public void clearConditions()
    {
        currentCondition = Condition.None;
        conditionAnimator.runtimeAnimatorController = null;
        conditionSpriteRenderer.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Update Conditions
        if (currentCondition != Condition.None)
        {
            if (remainingConditionTime <= 0f) clearConditions();
            else remainingConditionTime -= Time.deltaTime;
        }

        //Update Damage Grace
        if(damageGrace > 0f)
        {
            damageGrace -= Time.deltaTime;
            if (damageGrace <= 0f) lastDamageSource = SpellName.None;
        }

        //Check Movement Impairing Conditions
        float targetSpeed = speed;
        if (currentCondition != Condition.Frozen && currentCondition != Condition.Stunned)
        {
            //Check Slowed Conditions
            if (currentCondition == Condition.Slowed) targetSpeed *= 0.5f;

            //Move!
            if (((Vector2)this.transform.position - target) == Vector2.zero) retireEnemy();
            else
            {
                //Get Desired Movement Position
                Vector2 positionAfterMovement = Vector2.MoveTowards(this.transform.position, target, targetSpeed * Time.deltaTime);

                //Check for Collisions
                bool canMove = true;
                Collider2D[] collisions = Physics2D.OverlapBoxAll(positionAfterMovement, Vector2.one, 0f);
                for (int j = 0; j < collisions.Length; j++)
                {
                    if ((collisions[j].gameObject != this.gameObject) && (collisions[j].gameObject.tag == "Enemy"))
                    {
                        canMove = false;
                        break;
                    }
                }

                //Finally...
               if(canMove) this.transform.position = positionAfterMovement;
            }
        }
    }
}
