﻿using System;
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
    Slowed,
    Shocked
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
    private float stunGrace;

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
        stunGrace = 0f;
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
        ControllerManager.Instance.getCorruptionController().gainCorruption(spawnCost);
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
                conditionAnimator.runtimeAnimatorController = ControllerManager.Instance.getConditionController().frozenAnimation;
                break;
            case Condition.Stunned:
                conditionAnimator.runtimeAnimatorController = ControllerManager.Instance.getConditionController().stunnedAnimation;
                break;
            case Condition.Shocked:
                conditionAnimator.runtimeAnimatorController = ControllerManager.Instance.getConditionController().shockedAnimation;
                break;
            case Condition.Ablaze:
                break;
        }
    }

    //Clear Conditions Method
    public void clearConditions()
    {
        //Check for After-Effects
        if (currentCondition == Condition.Frozen) ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(this.transform.position, EnvironmentCondition.Puddle);

        //Reset Conditions
        currentCondition = Condition.None;
        conditionAnimator.runtimeAnimatorController = null;
        conditionSpriteRenderer.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if Enemy Has Arrive at Destination
        if (((Vector2)this.transform.position - target) == Vector2.zero)
        {
            retireEnemy();
            return;
        }

        //Update Conditions
        if (currentCondition != Condition.None)
        {
            if (remainingConditionTime <= 0f) clearConditions();
            else remainingConditionTime -= Time.deltaTime;
        }

        //Update Damage & Condition Grace
        if (stunGrace > 0f) stunGrace -= Time.deltaTime;
        if (damageGrace > 0f)
        {
            damageGrace -= Time.deltaTime;
            if (damageGrace <= 0f) lastDamageSource = SpellName.None;
        }

        //Check Movement Impairing Conditions
        float targetSpeed = speed;
        if (currentCondition != Condition.Frozen && currentCondition != Condition.Stunned && currentCondition != Condition.Shocked)
        {
            //Check Environment
            EnvironmentCondition condition = ControllerManager.Instance.getEnvironmentController().getEnvironmentCondition(this.transform.position);
            if (condition == EnvironmentCondition.Ice && stunGrace == 0f)
            {
                setCondition(Condition.Stunned, ControllerManager.Instance.getConditionController().stunnedDuration);
                stunGrace = ControllerManager.Instance.getConditionController().stunnedDuration + 2f;
                return;
            }
            else if(condition == EnvironmentCondition.Shock)
            {
                setCondition(Condition.Shocked, ControllerManager.Instance.getConditionController().shockedDuration);
                return;
            }

            //Check Slowed Conditions
            if (currentCondition == Condition.Slowed) targetSpeed *= 0.5f;

            //Get Desired Movement Position
            Vector2 positionAfterMovement = Vector2.MoveTowards(this.transform.position, target, targetSpeed * Time.deltaTime);

            //Check for Collisions
            bool canMove = true;
            Collider2D[] collisions = Physics2D.OverlapBoxAll(positionAfterMovement, Vector2.one, 0f);
            for (int j = 0; j < collisions.Length; j++)
            {
                if ((collisions[j].gameObject != this.gameObject) && (collisions[j].gameObject.CompareTag("Enemy")))
                {
                    //Check for Condition Propagation
                    Condition collisionCondition = collisions[j].gameObject.GetComponent<Enemy>().currentCondition;
                    if (collisionCondition == Condition.Ablaze) setCondition(Condition.Ablaze, ControllerManager.Instance.getConditionController().ablazeDuration);
                    else if(collisionCondition == Condition.Shocked) setCondition(Condition.Shocked, ControllerManager.Instance.getConditionController().shockedDuration);

                    //Break
                    canMove = false;
                    break;
                }
            }

            //Finally... Move!
            if (canMove) this.transform.position = positionAfterMovement;
        }
    }
}
