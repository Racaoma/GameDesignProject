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
    public float frozenDuration = 5f;
    public float stunnedDuration = 3f;
    public float shockedDuration = 3f;
    public float ablazeDuration = 3f;
}
