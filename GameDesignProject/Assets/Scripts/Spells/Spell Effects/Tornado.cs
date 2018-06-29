using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    //Balancing Variables
    public float damageInterval;
    public float duration;
    public float range;

    //Internal Variables
    public SpriteRenderer ringAreaSpriteRenderer;
    private Vector2 pivotCenter;
    private float currentIntervalPoint;
    private float remainingDuration;

    //Start Method
    private void Start()
    {
        ringAreaSpriteRenderer.transform.localScale = Vector2.one * range;
        currentIntervalPoint = 0f;
        remainingDuration = duration;
        pivotCenter = this.transform.GetChild(0).transform.position;
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
                    if (collisions[j].gameObject.CompareTag("Enemy"))
                    {
                        Enemy enemy = collisions[j].gameObject.GetComponent<Enemy>();
                        enemy.setCondition(Condition.Slowed, 1f);
                        enemy.takeDamage(SpellDatabase.tornadoSpell);
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
