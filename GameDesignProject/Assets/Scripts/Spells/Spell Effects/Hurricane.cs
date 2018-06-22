﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurricane : MonoBehaviour
{
    //Balancing Variables
    public float damageInterval;
    public float duration;
    public float range;

    //Internal Variables
    public SpriteRenderer ringAreaSpriteRenderer;
    private Spell hurricaneSpellRef;
    private Vector2 pivotCenter;
    private float currentIntervalPoint;
    private float remainingDuration;

    //Start Method
    private void Start()
    {
        ringAreaSpriteRenderer.transform.localScale = Vector2.one * range;
        hurricaneSpellRef = SpellDatabase.hurricaneSpell;
        currentIntervalPoint = 0f;
        remainingDuration = duration;
        pivotCenter = new Vector2(this.transform.position.x + 0.1f, this.transform.position.y - 0.3f);
    }

    //Update Method
    private void Update()
    {
        if (remainingDuration <= 0f) Destroy(this.gameObject);
        else
        {
            //Check Damage Interval
            if (currentIntervalPoint <= 0f)
            {
                //Trigger Damage & Slow
                Collider2D[] collisions = Physics2D.OverlapCircleAll(pivotCenter, range/2f);
                for (int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.tag == "Enemy")
                    {
                        Enemy enemy = collisions[j].gameObject.GetComponent<Enemy>();
                        enemy.setCondition(Condition.Slowed, 1f);
                        enemy.takeDamage(SpellDatabase.hurricaneSpell);
                    }
                }

                //Update Damage Interval
                currentIntervalPoint = damageInterval;
            }
            else currentIntervalPoint -= Time.deltaTime;

            //Update Remaining Time
            remainingDuration -= Time.deltaTime;
        }
    }
}