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
    private float remainingConditionTime;

	// Use this for initialization
	void Start ()
    {
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
            if (currentEnvironmentCondition == EnvironmentCondition.Puddle || currentEnvironmentCondition == EnvironmentCondition.Ice || currentEnvironmentCondition == EnvironmentCondition.PuddleAndShock)
            {
                if(currentEnvironmentCondition == EnvironmentCondition.PuddleAndShock) Destroy(this.transform.GetChild(0).gameObject);
                currentEnvironmentCondition = EnvironmentCondition.None;
                this.GetComponent<SpriteRenderer>().sprite = null;
            }
        }
        else if (condition == EnvironmentCondition.Ice)
        {
            if (currentEnvironmentCondition == EnvironmentCondition.Puddle || currentEnvironmentCondition == EnvironmentCondition.PuddleAndShock)
            {
                if (currentEnvironmentCondition == EnvironmentCondition.PuddleAndShock) Destroy(this.transform.GetChild(0).gameObject);
                currentEnvironmentCondition = EnvironmentCondition.Ice;
                this.transform.GetComponent<SpriteRenderer>().sprite = ControllerManager.Instance.getEnvironmentController().iceSprite;
                remainingConditionTime = ControllerManager.Instance.getEnvironmentController().iceDuration + additionalTime;
            }
        }
        else if (condition == EnvironmentCondition.Puddle)
        {
            if (currentEnvironmentCondition == EnvironmentCondition.None)
            {
                currentEnvironmentCondition = EnvironmentCondition.Puddle;
                this.transform.GetComponent<SpriteRenderer>().sprite = ControllerManager.Instance.getEnvironmentController().puddleSprite;
                this.transform.GetComponent<Animator>().enabled = true;
                remainingConditionTime = ControllerManager.Instance.getEnvironmentController().puddleDuration + additionalTime;
            }
            if (currentEnvironmentCondition == EnvironmentCondition.Shock)
            {
                currentEnvironmentCondition = EnvironmentCondition.PuddleAndShock;
                this.transform.GetComponent<SpriteRenderer>().sprite = ControllerManager.Instance.getEnvironmentController().puddleSprite;
                this.transform.GetComponent<Animator>().enabled = true;
                remainingConditionTime = ControllerManager.Instance.getEnvironmentController().shockDuration;
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
		if(remainingConditionTime > 0)
        {
            remainingConditionTime -= Time.deltaTime;
            if(remainingConditionTime <= 0f)
            {
                if(currentEnvironmentCondition == EnvironmentCondition.Puddle)
                {
                    currentEnvironmentCondition = EnvironmentCondition.None;
                    this.transform.GetComponent<SpriteRenderer>().sprite = null;
                    remainingConditionTime = 0f;
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
                    this.transform.GetComponent<SpriteRenderer>().sprite = ControllerManager.Instance.getEnvironmentController().puddleSprite;
                    remainingConditionTime = ControllerManager.Instance.getEnvironmentController().puddleDuration;
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

                //Disable Animation
                this.transform.GetComponent<Animator>().enabled = false;
            }
        }
	}
}
