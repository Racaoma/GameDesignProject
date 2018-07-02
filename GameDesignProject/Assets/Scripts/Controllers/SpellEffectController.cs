using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivePrecipitation
{
    None,
    Rain,
    ThunderStorm,
    HailStorm,
    Typhoon
};

public class SpellEffectController : MonoBehaviour
{
    //Spell Effects References
    public GameObject fireEffect;
    public GameObject tornado;
    public GameObject lightning;
    public GameObject cleanseEffect;
    public GameObject shockEffect;
    public GameObject superbolt;
    public GameObject hurricane;
    public GameObject fireStorm;

    //Rain Control Variables
    public GameObject rain;
    private ActivePrecipitation currentActivePrecipitation;
    private Animator rainAnimator;
    private float rainDuration;
    private float remainingRainDuration;
    private float thunderStormDuration;
    private float puddleSpawnInterval;
    private float lightningSpawnInterval;
    private float currentIntervalPoint_Puddles;
    private float currentIntervalPoint_Lightning;

    //Start Method
    private void Start()
    {
        rainAnimator = rain.GetComponent<Animator>();
        rainAnimator.enabled = false;
        currentActivePrecipitation = ActivePrecipitation.None;
        rainDuration = 0f;
        thunderStormDuration = 0f;
        puddleSpawnInterval = 0f;
        lightningSpawnInterval = 0f;
        currentIntervalPoint_Puddles = 0f;
        currentIntervalPoint_Lightning = 0f;
        remainingRainDuration = 0f;
    }

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
            case SpellName.Superbolt:
                Instantiate(superbolt, new Vector2(position.x, position.y + 4.5f), Quaternion.identity);
                break;
            case SpellName.Hurricane:
                Instantiate(hurricane, new Vector2(position.x, position.y + 1f), Quaternion.identity);
                break;
            case SpellName.FireStorm:
                Instantiate(fireStorm, new Vector2(position.x, position.y + 1f), Quaternion.identity);
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
        if(currentActivePrecipitation == ActivePrecipitation.None)
        {
            currentActivePrecipitation = ActivePrecipitation.Rain;
            this.rainDuration = rainDuration;
            this.puddleSpawnInterval = puddleSpawnInterval;
            remainingRainDuration = rainDuration;
            currentIntervalPoint_Puddles = puddleSpawnInterval;
            rainAnimator.enabled = true;
            rain.SetActive(true);
            rainAnimator.SetInteger("Type", 0);

            //Add Time to All Puddles
            List<Environment> puddles = ControllerManager.Instance.getEnvironmentController().getAllPuddles();
            for (int x = 0; x < puddles.Count; x++)
            {
                puddles[x].addTime(rainDuration);
            }
        }
        else if(currentActivePrecipitation == ActivePrecipitation.ThunderStorm || currentActivePrecipitation == ActivePrecipitation.Typhoon || currentActivePrecipitation == ActivePrecipitation.Rain)
        {
            currentActivePrecipitation = ActivePrecipitation.Rain;
            this.rainDuration = rainDuration;
            remainingRainDuration = rainDuration;

            //Add Time to All Puddles
            List<Environment> puddles = ControllerManager.Instance.getEnvironmentController().getAllPuddles();
            for (int x = 0; x < puddles.Count; x++)
            {
                puddles[x].addTime(rainDuration);
            }
        }
    }

    //Activate Thunder Storm
    public void activateThunderStorm(float rainDuration, float puddleSpawnInterval, float lightningSpawnInterval)
    {
        if (currentActivePrecipitation != ActivePrecipitation.HailStorm)
        {
            activateRain(rainDuration, puddleSpawnInterval);
            currentActivePrecipitation = ActivePrecipitation.ThunderStorm;
            thunderStormDuration = rainDuration;
            this.lightningSpawnInterval = lightningSpawnInterval;
            currentIntervalPoint_Lightning = lightningSpawnInterval;
        }
    }

    //Activate Hail Storm
    public void activateHailStorm(float duration, float damageInterval)
    {
        if (currentActivePrecipitation == ActivePrecipitation.None || currentActivePrecipitation == ActivePrecipitation.HailStorm)
        {
            currentActivePrecipitation = ActivePrecipitation.HailStorm;
            rainDuration = duration;
            remainingRainDuration = rainDuration;
            puddleSpawnInterval = damageInterval;
            currentIntervalPoint_Puddles = damageInterval;
            rainAnimator.enabled = true;
            rain.SetActive(true);
            rainAnimator.SetInteger("Type", 1);
        }
    }

    //Update Method
    private void Update()
    {
        if(currentActivePrecipitation != ActivePrecipitation.None)
        {
            if(currentActivePrecipitation == ActivePrecipitation.HailStorm)
            {
                //Check for Damage Interval
                if (currentIntervalPoint_Puddles <= 0f)
                {
                    List<GameObject> enemies = ControllerManager.Instance.getEnemySpawner().getActiveEnemies();
                    for(int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].GetComponent<Enemy>().takeDamage(SpellDatabase.hailStormSpell.damage);
                    }
                    currentIntervalPoint_Puddles = puddleSpawnInterval;
                }
                else currentIntervalPoint_Puddles -= Time.deltaTime;
            }
            else
            {
                //Check for Puddle Creation Interval
                if (currentIntervalPoint_Puddles <= 0f)
                {
                    Vector2 randomPosition = new Vector2(Random.Range(-7.9f, 7.9f), Random.Range(-4.9f, 3.9f));
                    ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(randomPosition, EnvironmentCondition.Puddle, rainDuration);
                    currentIntervalPoint_Puddles = puddleSpawnInterval;
                }
                else currentIntervalPoint_Puddles -= Time.deltaTime;

                //Check for Thunder Storm
                if (thunderStormDuration > 0f)
                {
                    if (currentIntervalPoint_Lightning <= 0f)
                    {
                        ControllerManager.Instance.getScreenFlashController().flashScreen(3f);
                        ControllerManager.Instance.getScreenShakeController().screenShake(0.1f, 0.5f);
                        Vector2 randomPosition = new Vector2(Random.Range(-7.9f, 7.9f), Random.Range(-4.9f, 3.9f));
                        spawnEffect(SpellDatabase.lightningStrikeSpell, randomPosition);
                        currentIntervalPoint_Lightning = lightningSpawnInterval;
                    }
                    else currentIntervalPoint_Lightning -= Time.deltaTime;
                    thunderStormDuration -= Time.deltaTime;
                }
            }

            //Check for Rain Time
            if (remainingRainDuration <= 1f) rainAnimator.SetTrigger("Advance");
            if (remainingRainDuration <= 0f)
            {
                rain.SetActive(false);
                rainAnimator.enabled = false;
                currentActivePrecipitation = ActivePrecipitation.None;
            }
            else remainingRainDuration -= Time.deltaTime;
        }
    }
}
