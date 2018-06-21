using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEffectController : MonoBehaviour
{
    //Variables
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool effectUnderway;

    //Setup Effect
    public void setEffect(Spell spell)
    {
        switch (spell.name)
        {
            case SpellName.FireBlast:
                animator.SetInteger("Effect", 0);
                animator.SetTrigger("ChangeEffect");
                break;
            case SpellName.LightningStrike:
                //TODO
                break;
            case SpellName.Hurricane:
                //TODO
                break;
            case SpellName.SummonBeasts:
                //TODO
                break;
            case SpellName.MeteorShower:
                //TODO
                break;
        }

        //Update Internal Variable
        effectUnderway = true;
    }

    //Start Method
    private void Start()
    {
        animator = this.transform.GetComponent<Animator>();
        spriteRenderer = this.transform.GetComponent<SpriteRenderer>();
        effectUnderway = false;
    }

    //Update Method
    private void Update()
    {
        if(effectUnderway)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime == 1f)
            {
                spriteRenderer.sprite = null;
                effectUnderway = false;
            }
        }
    }
}
