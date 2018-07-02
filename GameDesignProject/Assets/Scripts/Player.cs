using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Internal References
    private Animator animator;
    private GameObject textBalloon;
    private GameObject alert;
    private bool closeEnemy;

    //Singleton Instance Variable
    private static Player instance;
    public static Player Instance
    {
        get
        {
            return instance;
        }
    }

    //On Object Awake
    private void Awake()
    {
        //Check Singleton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    //On Object Destroy (Safeguard)
    public void OnDestroy()
    {
        instance = null;
    }

    //Start Method
    private void Start()
    {
        animator = this.GetComponent<Animator>();
        textBalloon = this.transform.GetChild(0).gameObject;
        alert = this.transform.GetChild(1).gameObject;
        closeEnemy = false;
    }

    //On Mouse Click
    private void OnMouseDown()
    {
        if(enabled)
        {
            SpellRangeOverlay.Instance.disableSpellOverlay();
            CastRunes.Instance.enableCastRunes();
        }
    }

    //Set Mana Text Balloon
    public void setManaTextBalloon(bool active)
    {
       textBalloon.SetActive(active);
    }

    //Casting Animation
    public void setCastingAnimation(int spellID)
    {
        animator.SetInteger("Spell", spellID);
        animator.SetTrigger("Cast");
    }

    //Fixed Update
    private void FixedUpdate()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(new Vector2(this.transform.position.x + 0.51f, this.transform.position.y), Vector2.right, 3f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject.CompareTag("Enemy"))
            {
                if(!closeEnemy)
                {
                    closeEnemy = true;
                    animator.ResetTrigger("EnemiesCleared");
                    animator.SetTrigger("Devourer");
                    alert.SetActive(true);
                }
                
                if (hit[i].distance < 1f && !ControllerManager.Instance.getDevourerController().isActive())
                {
                    alert.SetActive(false);
                    ControllerManager.Instance.getGrimmoireController().resetPreparedSpells();
                    CastRunes.Instance.disableCastRunes();
                    ControllerManager.Instance.getDevourerController().activateDevourer();
                    ControllerManager.Instance.getManaController().spendMana(ControllerManager.Instance.getManaController().getCurrentMana());
                    ControllerManager.Instance.getCorruptionController().gainCorruption(20);
                }
            }
        }

        //Return from Devourer Animation
        if (closeEnemy && hit.Length == 0)
        {
            animator.ResetTrigger("Cast");
            animator.ResetTrigger("Devourer");
            animator.SetTrigger("EnemiesCleared");
            alert.SetActive(false);
            closeEnemy = false;
        }
    }
}
