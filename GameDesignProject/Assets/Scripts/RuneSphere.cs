using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class RuneSphere : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver()
    {
        animator.enabled = true;
    }

    //Reset Method
    public void resetSphere()
    {
        animator.enabled = false;
        spriteRenderer.sprite = null;
    }
}
