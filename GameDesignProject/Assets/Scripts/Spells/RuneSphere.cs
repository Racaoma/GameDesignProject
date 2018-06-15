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

    private void FixedUpdate()
    {
        Collider2D result = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1 << this.gameObject.layer);
        if (result != null && result.gameObject.GetComponent<RuneSphere>().sphereID == this.sphereID)
        {
            if(castRunes.activatePath(sphereID)) animator.enabled = true;
        }
    }

    //Reset Method
    public void resetSphere()
    {
        animator.enabled = false;
        spriteRenderer.sprite = null;
    }
}
