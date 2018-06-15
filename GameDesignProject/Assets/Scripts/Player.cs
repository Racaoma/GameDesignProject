using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //CastRunes Reference
    public GameObject castRunes;

    //Prepared Spell
    private SpellName preparedSpell = SpellName.None;

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

    //Prepare Spell
    public void prepareSpell(SpellName spell)
    {
        if(spell != SpellName.None) Debug.Log("Prepared " + spell);
        preparedSpell = spell;
    }

    //On Mouse Click
    private void OnMouseDown()
    {
        if (preparedSpell != SpellName.None) Debug.Log("Canceled " + preparedSpell);
        preparedSpell = SpellName.None;
        castRunes.SetActive(true);
    }

    //Update Method
    private void Update()
    {
        //If Mouse is Released
        if (Input.GetMouseButtonUp(0) && castRunes.activeInHierarchy) castRunes.GetComponent<CastRunes>().disableCastRunes();

        //Check Mouse Click
        else if (Input.GetMouseButtonDown(0) && preparedSpell != SpellName.None)
        {
            //TODO: Lauch Spell
            Debug.Log("Casted " + preparedSpell);

            //Reset Prepared Spell
            preparedSpell = SpellName.None;
        }
    }
}
