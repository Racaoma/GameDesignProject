using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ActivePrecipitation
{
    None,
    Rain,
    ThunderStorm,
    HailStorm,
    Blizzard
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
    private AudioSource audioSource;
    private Image rainSprite;
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
        rainSprite = rain.GetComponent<Image>();
        audioSource = rain.GetComponent<AudioSource>();
        rainAnimator = rain.GetComponent<Animator>();
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
                Instantiate(lightning, new Vector2(position.x, position.y + 5f), Quaternion.identity);
                break;
            case SpellName.Tornado:
                Instantiate(tornado, new Vector2(position.x, position.y + 1f), Quaternion.identity);
                break;
            case SpellName.Cleanse:
                Instantiate(cleanseEffect, cleanseEffect.transform.position, Quaternion.identity);
                break;
            case SpellName.Superbolt:
                Instantiate(superbolt, new Vector2(position.x, position.y + 5f), Quaternion.identity);
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
            rainAnimator.SetInteger("Type", 0);
            rainAnimator.SetTrigger("Start");
            rainAnimator.ResetTrigger("Advance");
            rainSprite.color = new Color(1f, 1f, 1f, 1f);
            audioSource.clip = ControllerManager.Instance.getSoundController().rainClip;
            audioSource.Play();

            //Add Time to All Puddles
            List<Environment> puddles = ControllerManager.Instance.getEnvironmentController().getAllPuddles();
            for (int x = 0; x < puddles.Count; x++)
            {
                puddles[x].addTime(rainDuration);
            }
        }
        else if(currentActivePrecipitation == ActivePrecipitation.ThunderStorm || currentActivePrecipitation == ActivePrecipitation.Rain)
        {
            currentActivePrecipitation = ActivePrecipitation.Rain;
            this.rainDuration = rainDuration;
            remainingRainDuration = rainDuration;
            if (puddleSpawnInterval < this.puddleSpawnInterval) this.puddleSpawnInterval = puddleSpawnInterval;

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
        if (currentActivePrecipitation != ActivePrecipitation.HailStorm && currentActivePrecipitation != ActivePrecipitation.Blizzard)
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
            rainAnimator.SetInteger("Type", 1);
            rainAnimator.SetTrigger("Start");
            rainAnimator.ResetTrigger("Advance");
            rainSprite.color = new Color(1f, 1f, 1f, 1f);
            audioSource.clip = ControllerManager.Instance.getSoundController().hailClip;
            audioSource.Play();
        }
    }

    //Activate Blizzard
    public void activateBlizzard(float duration, float slowInterval)
    {
        if (currentActivePrecipitation == ActivePrecipitation.None || currentActivePrecipitation == ActivePrecipitation.Blizzard)
        {
            if (currentActivePrecipitation == ActivePrecipitation.None)
            {
                audioSource.clip = ControllerManager.Instance.getSoundController().blizzardClip;
                audioSource.Play();
            }
            currentActivePrecipitation = ActivePrecipitation.Blizzard;
            rainDuration = duration;
            remainingRainDuration = rainDuration;
            puddleSpawnInterval = slowInterval;
            currentIntervalPoint_Puddles = slowInterval;
            rainAnimator.SetInteger("Type", 2);
            rainAnimator.SetTrigger("Start");
            rainAnimator.ResetTrigger("Advance");
            rainSprite.color = new Color(1f, 1f, 1f, 1f);
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
            else if(currentActivePrecipitation == ActivePrecipitation.Blizzard)
            {
                //Check for Slow Interval
                if (currentIntervalPoint_Puddles <= 0f)
                {
                    List<GameObject> enemies = ControllerManager.Instance.getEnemySpawner().getActiveEnemies();
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        enemies[i].GetComponent<Enemy>().setCondition(Condition.Slowed, 1f, 0.5f);
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
                        Vector2 affectedArea = SpellRangeOverlay.Instance.getAffectedAreaByPoint(randomPosition);
                        spawnEffect(SpellDatabase.lightningStrikeSpell, affectedArea);

                        ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().thunderClip2);
                        currentIntervalPoint_Lightning = lightningSpawnInterval;

                        //Check for Puddles
                        EnvironmentCondition affectedTile = ControllerManager.Instance.getEnvironmentController().getEnvironmentCondition(affectedArea);
                        if (affectedTile == EnvironmentCondition.Puddle || affectedTile == EnvironmentCondition.PuddleAndShock)
                        {
                            Vector2[] affectedTiles = ControllerManager.Instance.getEnvironmentController().getConnectedPuddles(affectedArea);
                            for (int i = 0; i < affectedTiles.Length; i++)
                            {
                                ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(affectedTiles[i], EnvironmentCondition.Shock);
                            }
                        }

                        //Check for Affected Enemies
                        Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea, new Vector2(0.95f, 0.95f), 0f);
                        for (int j = 0; j < collisions.Length; j++)
                        {
                            if (collisions[j].gameObject.CompareTag("Enemy"))
                            {
                                collisions[j].gameObject.GetComponent<Enemy>().takeDamage(SpellDatabase.lightningStrikeSpell.damage);
                            }
                        }
                    }
                    else currentIntervalPoint_Lightning -= Time.deltaTime;
                    thunderStormDuration -= Time.deltaTime;
                }
            }

            //Check Sounds
            if (!audioSource.isPlaying && remainingRainDuration >= 3f) audioSource.Play();

            //Check for Rain Time
            if (remainingRainDuration <= 1f) rainAnimator.SetTrigger("Advance");
            if (remainingRainDuration <= 0f)
            {
                rainSprite.color = new Color(1f, 1f, 1f, 0f);
                currentActivePrecipitation = ActivePrecipitation.None;
            }
            else remainingRainDuration -= Time.deltaTime;
        }
        else
        {
            //No Precipitation
            if (audioSource.isPlaying)
            {
                if (audioSource.volume == 0f)
                {
                    audioSource.Stop();
                    audioSource.volume = 1f;
                }
                else audioSource.volume -= 0.3f * Time.deltaTime;
            }
            else if (audioSource.volume < 1f) audioSource.volume = 1f;
        }
    }
}
