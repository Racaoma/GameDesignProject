using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectController : MonoBehaviour
{
    //Spell Effects References
    public GameObject fireEffect;
    public GameObject hurricane;
    public GameObject lightning;

    //Spawn Effect Method
    public void spawnEffect(Spell spell, Vector2 position)
    {
        switch(spell.name)
        {
            case SpellName.FireBlast:
                Instantiate(fireEffect, position, Quaternion.identity);
                break;
            case SpellName.LightningStrike:
                Instantiate(lightning, new Vector2(position.x, position.y + 4.5f), Quaternion.identity);
                break;
            case SpellName.Hurricane:
                Instantiate(hurricane, new Vector2(position.x, position.y + 1f), Quaternion.identity);
                break;
        }
    }
}
