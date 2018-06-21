using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectController : MonoBehaviour
{
    //Spell Effects References
    public GameObject fireEffect;

    //Spawn Effect Method
    public void spawnEffect(Spell spell, Vector2 position)
    {
        switch(spell.name)
        {
            case SpellName.FireBlast:
                Instantiate(fireEffect, position, Quaternion.identity);
                break;
        }
    }
}
