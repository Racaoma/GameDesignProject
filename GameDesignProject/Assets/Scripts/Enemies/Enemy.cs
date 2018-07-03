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
    Slowed,
    Shocked
}

public class Enemy : MonoBehaviour
{
    //Variables
    public Image healthBar;
    public int maxHitPoints;
    public float speed;
    public float speedFactor;
    public int spawnCost;
    public Action<GameObject> onDeathAction;
    private Vector2 target;
    private AudioSource audioSource;

    //Damage Control
    private int currentHitPoints;

    //Conditions Animator & Sprite Renderer Reference
    public Animator conditionAnimator;
    public SpriteRenderer conditionSpriteRenderer;

    //Conditions
    private Condition currentCondition;
    private float remainingConditionTime;
    private float currentIntervalPoint;
    private float stunGrace;

    //Start Method
    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
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
        speedFactor = 1f;
        currentIntervalPoint = 0f;
        healthBar.fillAmount = 1f;
        currentHitPoints = maxHitPoints;
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
    public bool takeDamage(int damage)
    {   
        currentHitPoints -= damage;
        healthBar.fillAmount = (float) currentHitPoints / (float) maxHitPoints;
        if (currentHitPoints <= 0)
        {
            killEnemy();
            return true;
        }
        else return false;
    }

    //Enemy Arrived at Destination
    public void retireEnemy()
    {
        if(!ControllerManager.Instance.getCorruptionController().isGameOver())
        {
            ControllerManager.Instance.getCorruptionController().gainCorruption(spawnCost);
            ControllerManager.Instance.getCorruptionController().spawnCorruptionParticles(this.transform.position);
        }

        //Finally...
        killEnemy();
    }

    //Action Safeguard
    private void OnDestroy()
    {
        onDeathAction = null;
    }

    //Play Sound
    private void playSound(AudioClip audio)
    {
        audioSource.clip = audio;
        audioSource.Play();
    }

    //Set Condition Method
    public void setCondition(Condition condition, float time, float speedFactor = 1f)
    {
        //Check for Condition Anullments
        if (currentCondition == Condition.Ablaze && condition == Condition.Frozen)
        {
            playSound(ControllerManager.Instance.getSoundController().extinguishClip);
            ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(this.transform.position, EnvironmentCondition.Puddle);
            clearConditions();
            return;
        }
        else if(currentCondition == Condition.Frozen && condition == Condition.Ablaze)
        {
            ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(this.transform.position, EnvironmentCondition.Puddle);
            clearConditions();
            return;
        }

        //Set Condition
        if(time > 0f)
        {
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
                    playSound(ControllerManager.Instance.getSoundController().ablazeClip);
                    conditionAnimator.runtimeAnimatorController = ControllerManager.Instance.getConditionController().ablazeAnimation;
                    currentIntervalPoint = ControllerManager.Instance.getConditionController().ablazeDamageInterval;
                    break;
                case Condition.Slowed:
                    this.speedFactor = speedFactor;
                    break;
            }
        }
    }

    //Clear Conditions Method
    public void clearConditions()
    {
        //Check for After-Effects
        if (remainingConditionTime <= 0f && currentCondition == Condition.Frozen) ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(this.transform.position, EnvironmentCondition.Puddle);
        else if(remainingConditionTime <= 0f && currentCondition == Condition.Ablaze) audioSource.PlayOneShot(ControllerManager.Instance.getSoundController().extinguishClip);

        //Reset Conditions
        audioSource.clip = null;
        speedFactor = 1f;
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
            //Ablaze Damage
            if (currentCondition == Condition.Ablaze)
            {
                currentIntervalPoint -= Time.deltaTime;
                if (currentIntervalPoint <= 0f)
                {
                    if(takeDamage(ControllerManager.Instance.getConditionController().ablazeDamage)) return;
                    currentIntervalPoint = ControllerManager.Instance.getConditionController().ablazeDamageInterval;
                }
            }

            //Update Remaining Condition Time
            remainingConditionTime -= Time.deltaTime;
            if (remainingConditionTime <= 0f) clearConditions();
        }

        //Update Stun Grace
        if (stunGrace > 0f) stunGrace -= Time.deltaTime;

        //Check Movement Impairing Conditions
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
            else if(condition == EnvironmentCondition.Shock || condition == EnvironmentCondition.PuddleAndShock)
            {
                setCondition(Condition.Shocked, ControllerManager.Instance.getConditionController().shockedDuration);
                return;
            }

            //Get Desired Movement Position
            Vector2 positionAfterMovement = Vector2.MoveTowards(this.transform.position, target, speed * speedFactor * Time.deltaTime);

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
