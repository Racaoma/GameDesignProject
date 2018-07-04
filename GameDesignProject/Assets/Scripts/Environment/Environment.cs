using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnvironmentCondition
{
    None,
    Puddle,
    Ice,
    Fire,
    Shock,
    PuddleAndShock,
    IceAndShock
};

public class Environment : MonoBehaviour
{
    //Variables
    public EnvironmentCondition currentEnvironmentCondition;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float remainingConditionTime;

	// Use this for initialization
	void Start ()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        currentEnvironmentCondition = EnvironmentCondition.None;
        remainingConditionTime = 0f;
    }

    //Set New Time to Environment Condition
    public void addTime(float time)
    {
        remainingConditionTime += time;
    }

    //Set Environment Condition
    public void setEnvironmentCondition(EnvironmentCondition condition, float additionalTime = 0f)
    {
        if (condition == EnvironmentCondition.Fire)
        {
            if (currentEnvironmentCondition == EnvironmentCondition.Puddle || currentEnvironmentCondition == EnvironmentCondition.Ice)
            {
                animator.SetTrigger("Evaporate");
                currentEnvironmentCondition = EnvironmentCondition.None;
            }
            else if (currentEnvironmentCondition == EnvironmentCondition.PuddleAndShock || currentEnvironmentCondition == EnvironmentCondition.IceAndShock)
            {
                animator.SetTrigger("Evaporate");
                currentEnvironmentCondition = EnvironmentCondition.Shock;
            }
        }
        else if (condition == EnvironmentCondition.Ice)
        {
            if (currentEnvironmentCondition == EnvironmentCondition.Puddle || currentEnvironmentCondition == EnvironmentCondition.PuddleAndShock)
            {
                currentEnvironmentCondition = EnvironmentCondition.Ice;
                spriteRenderer.sprite = ControllerManager.Instance.getEnvironmentController().iceSprite;
                remainingConditionTime = ControllerManager.Instance.getEnvironmentController().iceDuration + additionalTime;
                animator.SetTrigger("Solodify");
            }
        }
        else if (condition == EnvironmentCondition.Puddle)
        {
            if (currentEnvironmentCondition == EnvironmentCondition.None)
            {
                currentEnvironmentCondition = EnvironmentCondition.Puddle;
                remainingConditionTime = ControllerManager.Instance.getEnvironmentController().puddleDuration + additionalTime;
                animator.SetTrigger("Create");
            }
            if (currentEnvironmentCondition == EnvironmentCondition.Shock)
            {
                currentEnvironmentCondition = EnvironmentCondition.PuddleAndShock;
                remainingConditionTime = ControllerManager.Instance.getEnvironmentController().shockDuration;
                animator.SetTrigger("Create");
            }
        }
        else if(condition == EnvironmentCondition.Shock)
        {
            if (currentEnvironmentCondition == EnvironmentCondition.Puddle) currentEnvironmentCondition = EnvironmentCondition.PuddleAndShock;
            else if(currentEnvironmentCondition == EnvironmentCondition.Ice) currentEnvironmentCondition = EnvironmentCondition.IceAndShock;
            else currentEnvironmentCondition = EnvironmentCondition.Shock;

            GameObject obj = ControllerManager.Instance.getSpellEffectController().spawnShockEffect(this.transform.position);
            obj.transform.parent = this.gameObject.transform;
            remainingConditionTime = ControllerManager.Instance.getEnvironmentController().shockDuration + additionalTime;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //Update Sprite & Shock Animation
        if (currentEnvironmentCondition == EnvironmentCondition.None) spriteRenderer.sprite = null;
        else if ((currentEnvironmentCondition != EnvironmentCondition.PuddleAndShock && currentEnvironmentCondition != EnvironmentCondition.IceAndShock && currentEnvironmentCondition != EnvironmentCondition.Shock) && (this.transform.childCount > 0)) Destroy(this.transform.GetChild(0).gameObject);

        //Update Condition
        if (remainingConditionTime > 0)
        {
            remainingConditionTime -= Time.deltaTime;
            if(remainingConditionTime <= 0f)
            {
                if(currentEnvironmentCondition == EnvironmentCondition.Puddle)
                {
                    currentEnvironmentCondition = EnvironmentCondition.None;
                    spriteRenderer.sprite = null;
                    remainingConditionTime = 0f;
                    animator.SetTrigger("Evaporate");
                }
                else if (currentEnvironmentCondition == EnvironmentCondition.PuddleAndShock)
                {
                    currentEnvironmentCondition = EnvironmentCondition.Puddle;
                    Destroy(this.transform.GetChild(0).gameObject);
                    remainingConditionTime = ControllerManager.Instance.getEnvironmentController().puddleDuration;
                }
                else if(currentEnvironmentCondition == EnvironmentCondition.Ice)
                {
                    currentEnvironmentCondition = EnvironmentCondition.Puddle;
                    spriteRenderer.sprite = ControllerManager.Instance.getEnvironmentController().puddleSprite;
                    remainingConditionTime = ControllerManager.Instance.getEnvironmentController().puddleDuration;
                    animator.SetTrigger("Melt");
                }
                else if (currentEnvironmentCondition == EnvironmentCondition.IceAndShock)
                {
                    currentEnvironmentCondition = EnvironmentCondition.Ice;
                    Destroy(this.transform.GetChild(0).gameObject);
                    remainingConditionTime = ControllerManager.Instance.getEnvironmentController().iceDuration;
                }
                else if (currentEnvironmentCondition == EnvironmentCondition.Shock)
                {
                    currentEnvironmentCondition = EnvironmentCondition.None;
                    Destroy(this.transform.GetChild(0).gameObject);
                    remainingConditionTime = 0f;
                }
            }
        }
	}
}
