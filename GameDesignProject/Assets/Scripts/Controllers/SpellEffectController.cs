using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellEffectController : MonoBehaviour
{
    //Spell Effects References
    public GameObject fireEffect;
    public GameObject tornado;
    public GameObject lightning;
    public GameObject cleanseEffect;
    public GameObject shockEffect;

    //Rain Control
    public GameObject rain;
    private float rainDuration;
    private float puddleSpawnInterval;
    private float currentIntervalPoint;
    private float remainingRainDuration;

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
            case SpellName.Tornado:
                Instantiate(tornado, new Vector2(position.x, position.y + 1f), Quaternion.identity);
                break;
            case SpellName.Cleanse:
                Instantiate(cleanseEffect, cleanseEffect.transform.position, Quaternion.identity);
                break;
        }
    }

    //Spawn Shock Effect
    public GameObject spawnShockEffect(Vector2 position)
    {
        return Instantiate(shockEffect, position, Quaternion.identity);
    }

    //Activate Rain
    public void activateRain(float rainDuration, float puddleSpawnInterval)
    {
        this.rainDuration = rainDuration;
        this.puddleSpawnInterval = puddleSpawnInterval;
        remainingRainDuration = rainDuration;
        currentIntervalPoint = puddleSpawnInterval;
        rain.SetActive(true);

        //Add Time to All Puddles
        List<Environment> puddles = ControllerManager.Instance.getEnvironmentController().getAllPuddles();
        for(int x = 0; x < puddles.Count; x++)
        {
            puddles[x].setTime(rainDuration + ControllerManager.Instance.getEnvironmentController().puddleDuration);
        }
    }

    //Update Method
    private void Update()
    {
        if(rain.activeInHierarchy)
        {
            //Check for Puddle Creation Interval
            if (currentIntervalPoint <= 0f)
            {
                Vector2 randomPosition = new Vector2(Random.Range(-7.9f, 7.9f), Random.Range(-4.9f, 3.9f));
                ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(randomPosition, EnvironmentCondition.Puddle, rainDuration);
                currentIntervalPoint = puddleSpawnInterval;
            }
            else currentIntervalPoint -= Time.deltaTime;

            //Check for Rain Time
            if (remainingRainDuration <= 0f)
            {
                rain.SetActive(false);
            }
            else remainingRainDuration -= Time.deltaTime;
        }
    }
}
