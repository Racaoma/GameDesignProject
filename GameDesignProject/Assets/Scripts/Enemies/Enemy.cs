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
    public int hitPoints;
    public float speed;
    public int spawnCost;
    public Action<GameObject> onDeathAction;

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
        currentCondition = Condition.None;
        GameObject child = this.transform.GetChild(0).gameObject;
        conditionAnimator = child.GetComponent<Animator>();
        conditionSpriteRenderer = child.GetComponent<SpriteRenderer>();
    }

    //Kill Enemy
    public void killEnemy()
    {
        currentCondition = Condition.None;
        this.transform.position = Vector2.zero;
        if (onDeathAction != null) onDeathAction(this.gameObject);
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
        //Check if Dead
        if (hitPoints == 0) killEnemy();
        else
        {
            //Update Conditions
            if (currentCondition != Condition.None)
            {
                if (remainingConditionTime <= 0f) clearConditions();
                else remainingConditionTime -= Time.deltaTime;
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
}
