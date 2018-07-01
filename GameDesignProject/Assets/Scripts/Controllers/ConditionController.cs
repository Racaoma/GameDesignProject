using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionController : MonoBehaviour
{
    //Conditions Animations
    public RuntimeAnimatorController frozenAnimation;
    public RuntimeAnimatorController stunnedAnimation;
    public RuntimeAnimatorController shockedAnimation;
    public RuntimeAnimatorController ablazeAnimation;

    //Balance Variables
    public float frozenDuration;
    public float stunnedDuration;
    public float shockedDuration;
    public float ablazeDuration;
    public float ablazeDamageInterval;
    public int ablazeDamage;
}
