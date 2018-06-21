using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Condition
{
    None,
    Stunned,
    Frozen,
    Ablaze
}

public class Enemy : MonoBehaviour
{
    //Variables
    public int maxHitPoints;
    public float speed;
    public int spawnCost;
    public Action<GameObject> onDeathAction;

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

    //Reset Internal Variables Method
    private void resetInternalVariables()
    {
        currentHitPoints = maxHitPoints;
        lastDamageSource = SpellName.None;
        damageGrace = 0f;
        currentCondition = Condition.None;
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

        //Change Animation
        conditionAnimator.runtimeAnimatorController = frozenAnimation;
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
        if(currentCondition != Condition.Frozen && currentCondition != Condition.Stunned)
        {
            //Move!
            Vector2 target = new Vector2(-9.5f, this.transform.position.y);
            if (((Vector2)this.transform.position - target) == Vector2.zero) retireEnemy();
            else this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        }
    }
}
