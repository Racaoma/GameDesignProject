using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Conditions
{
    None,
    Ablaze,
    Frozen,
    Stunned
};

public class Enemy : MonoBehaviour
{
    //Variables
    public int hitPoints;
    public float speed;
    public int spawnCost;
    public Action<GameObject> onDeathAction;

    //Conditions
    private Conditions currentCondition = Conditions.None;
    private float remainingConditionTime;

    //Conditions Animations
    public RuntimeAnimatorController freeze;

    //Kill Enemy
    public void killEnemy()
    {
        currentCondition = Conditions.None;
        this.transform.position = Vector2.zero;
        if (onDeathAction != null) onDeathAction(this.gameObject);
    }

    //Enemy Arrived at Destination
    public void retireEnemy()
    {
        //TODO: Something
        killEnemy();
    }

    //Action Safeguard
    private void OnDestroy()
    {
        onDeathAction = null;
    }

    //Set Condition Method
    public void setCondition(Conditions condition, float time)
    {
        //Set Condition
        currentCondition = condition;
        remainingConditionTime = time;

        //Change Animation
        this.transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = freeze;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if Dead
        if (hitPoints == 0) killEnemy();
        else
        {
            //Update Conditions
            if (currentCondition != Conditions.None)
            {
                if (remainingConditionTime <= 0f)
                {
                    currentCondition = Conditions.None;
                    this.transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController = null;
                    this.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                }
                else remainingConditionTime -= Time.deltaTime;
            }

            //Check Movement Impairing Conditions
            if(currentCondition != Conditions.Frozen && currentCondition != Conditions.Stunned)
            {
                //Move!
                Vector2 target = new Vector2(-9.5f, this.transform.position.y);
                if (((Vector2)this.transform.position - target) == Vector2.zero) retireEnemy();
                else this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
            }
        }
    }
}
