using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class RuneSphere : MonoBehaviour
{
    //Sphere ID
    public int sphereID;

    //Components References
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CastRunes castRunes;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        castRunes = GetComponentInParent<CastRunes>();
    }

    //Trigger Sphere Method
    public void triggerSphere()
    {
        if (castRunes.activatePath(this.gameObject)) animator.enabled = true;
    }

    //Reset Method
    public void resetSphere()
    {
        animator.enabled = false;
        spriteRenderer.sprite = null;
    }
}
