﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrimmoireController : MonoBehaviour
{
    //References
    public Image preparedSpell1_Image;
    public Image preparedSpell2_Image;
    public Image preparedSpellFinal_Image;
    public Text preparedSpellText;

    //Spell Icons
    public Sprite defaultRune1;
    public Sprite defaultRune2;
    public Sprite defaultRune3;
    public Sprite flashFreezeIcon;
    public Sprite fireBlastIcon;
    public Sprite lightningStrikeIcon;
    public Sprite tornadoIcon;
    public Sprite summonRainIcon;
    public Sprite cleanseIcon;
    public Sprite deepFreezeIcon;
    public Sprite hellFireIcon;
    public Sprite superboltIcon;
    public Sprite hurricaneIcon;
    public Sprite cloudburstIcon;
    public Sprite purifyIcon;
    public Sprite thunderStormIcon;
    public Sprite hailStormIcon;
    public Sprite typhoonIcon;
    public Sprite superCellIcon;
    public Sprite fireStormIcon;
    public Sprite blizzardIcon;

    //Variables
    private Spell preparedSpell1;
    private Spell preparedSpell2;

    //Cancel Both Prepared Spell
    public void resetPreparedSpells()
    {
        preparedSpell1 = null;
        preparedSpell2 = null;
        updateGrimmoireFeedback(null);
        SpellRangeOverlay.Instance.disableSpellOverlay();
        ControllerManager.Instance.getCursorChangerController().resetMouse();
        Player.Instance.setManaTextBalloon(false);
    }

    //Cancel Current Prepared Spell
    public void resetCurrentPreparedSpell()
    {
        if (preparedSpell2 != null)
        {
            preparedSpell2 = null;
            preparedSpell2_Image.sprite = defaultRune2;
        }
        else resetPreparedSpells();
    }

    //Reset Grimmoire
    private void resetGrimmoireIcons()
    {
        preparedSpell1_Image.sprite = defaultRune1;
        preparedSpell2_Image.sprite = defaultRune2;
    }

    //Update Prepared Spell Text
    public void updateGrimmoireFeedback(Spell preparedSpell)
    {
        if (preparedSpell == null)
        {
            preparedSpell1_Image.sprite = defaultRune1;
            preparedSpell2_Image.sprite = defaultRune2;
            preparedSpellFinal_Image.sprite = defaultRune3;
            preparedSpellText.text = "No Spell Prepared";
        }
        else
        {
            int corruptionBonus = ControllerManager.Instance.getCorruptionController().getCorruptionBonuses();
            int manaCost = preparedSpell.manaCost - corruptionBonus;

            if (preparedSpell.name == SpellName.FlashFreeze)
            {
                preparedSpell1_Image.sprite = flashFreezeIcon;
                preparedSpell2_Image.sprite = defaultRune2;
                preparedSpellFinal_Image.sprite = flashFreezeIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Flash Freeze\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Flash Freeze\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.FireBlast)
            {
                preparedSpell1_Image.sprite = fireBlastIcon;
                preparedSpell2_Image.sprite = defaultRune2;
                preparedSpellFinal_Image.sprite = fireBlastIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Fire Blast\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Fire Blast\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.LightningStrike)
            {
                preparedSpell1_Image.sprite = lightningStrikeIcon;
                preparedSpell2_Image.sprite = defaultRune2;
                preparedSpellFinal_Image.sprite = lightningStrikeIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Lightning Strike\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Lightning Strike\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.Tornado)
            {
                preparedSpell1_Image.sprite = tornadoIcon;
                preparedSpell2_Image.sprite = defaultRune2;
                preparedSpellFinal_Image.sprite = tornadoIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Tornado\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Tornado\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.SummonRain)
            {
                preparedSpell1_Image.sprite = summonRainIcon;
                preparedSpell2_Image.sprite = defaultRune2;
                preparedSpellFinal_Image.sprite = summonRainIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Summon Rain\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Summon Rain\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.Cleanse)
            {
                preparedSpell1_Image.sprite = cleanseIcon;
                preparedSpell2_Image.sprite = defaultRune2;
                preparedSpellFinal_Image.sprite = cleanseIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Cleanse\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Cleanse\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.DeepFreeze)
            {
                preparedSpell1_Image.sprite = flashFreezeIcon;
                preparedSpell2_Image.sprite = flashFreezeIcon;
                preparedSpellFinal_Image.sprite = deepFreezeIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Deep Freeze\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Deep Freeze\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.Hellfire)
            {
                preparedSpell1_Image.sprite = fireBlastIcon;
                preparedSpell2_Image.sprite = fireBlastIcon;
                preparedSpellFinal_Image.sprite = hellFireIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Hell Fire\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Hell Fire\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.Superbolt)
            {
                preparedSpell1_Image.sprite = lightningStrikeIcon;
                preparedSpell2_Image.sprite = lightningStrikeIcon;
                preparedSpellFinal_Image.sprite = superboltIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Superbolt\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Superbolt\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.Hurricane)
            {
                preparedSpell1_Image.sprite = tornadoIcon;
                preparedSpell2_Image.sprite = tornadoIcon;
                preparedSpellFinal_Image.sprite = hurricaneIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Hurricane\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Hurricane\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.Cloudburst)
            {
                preparedSpell1_Image.sprite = summonRainIcon;
                preparedSpell2_Image.sprite = summonRainIcon;
                preparedSpellFinal_Image.sprite = cloudburstIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Cloudburst\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Cloudburst\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.Purify)
            {
                preparedSpell1_Image.sprite = cleanseIcon;
                preparedSpell2_Image.sprite = cleanseIcon;
                preparedSpellFinal_Image.sprite = purifyIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Purify\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Purify\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.ThunderStorm)
            {
                if(preparedSpell1.name == SpellName.LightningStrike)
                {
                    preparedSpell1_Image.sprite = lightningStrikeIcon;
                    preparedSpell2_Image.sprite = summonRainIcon;
                }
                else
                {
                    preparedSpell1_Image.sprite = summonRainIcon;
                    preparedSpell2_Image.sprite = lightningStrikeIcon;
                }
                preparedSpellFinal_Image.sprite = thunderStormIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Thunder Storm\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Thunder Storm\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.HailStorm)
            {
                if (preparedSpell1.name == SpellName.SummonRain)
                {
                    preparedSpell1_Image.sprite = summonRainIcon;
                    preparedSpell2_Image.sprite = flashFreezeIcon;
                }
                else
                {
                    preparedSpell1_Image.sprite = flashFreezeIcon;
                    preparedSpell2_Image.sprite = summonRainIcon;
                }
                preparedSpellFinal_Image.sprite = hailStormIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Hail Storm\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Hail Storm\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.Typhoon)
            {
                if (preparedSpell1.name == SpellName.SummonRain)
                {
                    preparedSpell1_Image.sprite = summonRainIcon;
                    preparedSpell2_Image.sprite = tornadoIcon;
                }
                else
                {
                    preparedSpell1_Image.sprite = tornadoIcon;
                    preparedSpell2_Image.sprite = summonRainIcon;
                }
                preparedSpellFinal_Image.sprite = typhoonIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Typhoon\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Typhoon\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.SuperCell)
            {
                if (preparedSpell1.name == SpellName.LightningStrike)
                {
                    preparedSpell1_Image.sprite = lightningStrikeIcon;
                    preparedSpell2_Image.sprite = tornadoIcon;
                }
                else
                {
                    preparedSpell1_Image.sprite = tornadoIcon;
                    preparedSpell2_Image.sprite = lightningStrikeIcon;
                }
                preparedSpellFinal_Image.sprite = superCellIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Super Cell\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Super Cell\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.FireStorm)
            {
                if (preparedSpell1.name == SpellName.FireBlast)
                {
                    preparedSpell1_Image.sprite = fireBlastIcon;
                    preparedSpell2_Image.sprite = tornadoIcon;
                }
                else
                {
                    preparedSpell1_Image.sprite = tornadoIcon;
                    preparedSpell2_Image.sprite = fireBlastIcon;
                }
                preparedSpellFinal_Image.sprite = fireStormIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Fire Storm\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Fire Storm\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
            else if (preparedSpell.name == SpellName.Blizzard)
            {
                if (preparedSpell1.name == SpellName.FlashFreeze)
                {
                    preparedSpell1_Image.sprite = flashFreezeIcon;
                    preparedSpell2_Image.sprite = tornadoIcon;
                }
                else
                {
                    preparedSpell1_Image.sprite = tornadoIcon;
                    preparedSpell2_Image.sprite = flashFreezeIcon;
                }
                preparedSpellFinal_Image.sprite = blizzardIcon;
                if (corruptionBonus == 0) preparedSpellText.text = "Blizzard\nMana Cost = " + manaCost;
                else preparedSpellText.text = "Blizzard\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
        }
    } 

    //Prepare Spell Verifier
    public void prepareNewSpell(Spell newSpell)
    {
        if (newSpell != null)
        {
            if (preparedSpell1 == null) preparedSpell1 = newSpell;
            else
            {
                if (preparedSpell1.name == SpellName.FlashFreeze)
                {
                    if (newSpell.name == SpellName.FlashFreeze || newSpell.name == SpellName.Tornado || newSpell.name == SpellName.SummonRain) preparedSpell2 = newSpell;
                    else
                    {
                        ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().comboFailClip);
                        resetPreparedSpells();
                    }
                }
                else if (preparedSpell1.name == SpellName.FireBlast)
                {
                    if (newSpell.name == SpellName.FireBlast || newSpell.name == SpellName.Tornado) preparedSpell2 = newSpell;
                    else
                    {
                        ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().comboFailClip);
                        resetPreparedSpells();
                    }
                }
                else if (preparedSpell1.name == SpellName.LightningStrike)
                {
                    if (newSpell.name == SpellName.LightningStrike || newSpell.name == SpellName.Tornado || newSpell.name == SpellName.SummonRain) preparedSpell2 = newSpell;
                    else
                    {
                        ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().comboFailClip);
                        resetPreparedSpells();
                    }
                }
                else if (preparedSpell1.name == SpellName.Tornado)
                {
                    if (newSpell.name == SpellName.Tornado || newSpell.name == SpellName.FlashFreeze || newSpell.name == SpellName.FireBlast || newSpell.name == SpellName.LightningStrike || newSpell.name == SpellName.SummonRain) preparedSpell2 = newSpell;
                    else
                    {
                        ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().comboFailClip);
                        resetPreparedSpells();
                    }
                }
                else if (preparedSpell1.name == SpellName.SummonRain)
                {
                    if (newSpell.name == SpellName.SummonRain || newSpell.name == SpellName.FlashFreeze || newSpell.name == SpellName.LightningStrike || newSpell.name == SpellName.Tornado) preparedSpell2 = newSpell;
                    else
                    {
                        ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().comboFailClip);
                        resetPreparedSpells();
                    }
                }
                else if (preparedSpell1.name == SpellName.Cleanse)
                {
                    if (newSpell.name == SpellName.Cleanse) preparedSpell2 = newSpell;
                    else
                    {
                        ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().comboFailClip);
                        resetPreparedSpells();
                    }
                }
            }
        }
        else ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().drawfailClip);
    }

    //Get Prepared Spell According to Prepared Spell 1 & 2
    private Spell getPreparedSpell()
    {
        if (preparedSpell2 == null) return preparedSpell1;
        else
        {
            if (preparedSpell1.name == SpellName.FlashFreeze)
            {
                if (preparedSpell2.name == SpellName.FlashFreeze) return SpellDatabase.deepFreezeSpell;
                else if (preparedSpell2.name == SpellName.Tornado) return SpellDatabase.blizzardSpell;
                else if (preparedSpell2.name == SpellName.SummonRain) return SpellDatabase.hailStormSpell;
                else return null;
            }
            else if (preparedSpell1.name == SpellName.FireBlast)
            {
                if (preparedSpell2.name == SpellName.FireBlast) return SpellDatabase.hellFireSpell;
                else if (preparedSpell2.name == SpellName.Tornado) return SpellDatabase.fireStormSpell;
                else return null;
            }
            else if (preparedSpell1.name == SpellName.LightningStrike)
            {
                if (preparedSpell2.name == SpellName.LightningStrike) return SpellDatabase.superboltSpell;
                else if (preparedSpell2.name == SpellName.Tornado) return SpellDatabase.superCellSpell;
                else if (preparedSpell2.name == SpellName.SummonRain) return SpellDatabase.thunderStormSpell;
                else return null;
            }
            else if (preparedSpell1.name == SpellName.Tornado)
            {
                if (preparedSpell2.name == SpellName.Tornado) return SpellDatabase.hurricaneSpell;
                else if (preparedSpell2.name == SpellName.FlashFreeze) return SpellDatabase.blizzardSpell;
                else if (preparedSpell2.name == SpellName.FireBlast) return SpellDatabase.fireStormSpell;
                else if (preparedSpell2.name == SpellName.LightningStrike) return SpellDatabase.superCellSpell;
                else if (preparedSpell2.name == SpellName.SummonRain) return SpellDatabase.typhoonSpell;
                else return null;
            }
            else if (preparedSpell1.name == SpellName.SummonRain)
            {
                if (preparedSpell2.name == SpellName.SummonRain) return SpellDatabase.cloudBurstSpell;
                else if (preparedSpell2.name == SpellName.FlashFreeze) return SpellDatabase.hailStormSpell;
                else if (preparedSpell2.name == SpellName.LightningStrike) return SpellDatabase.thunderStormSpell;
                else if (preparedSpell2.name == SpellName.Tornado) return SpellDatabase.typhoonSpell;
                else return null;
            }
            else if (preparedSpell1.name == SpellName.Cleanse)
            {
                if (preparedSpell2.name == SpellName.Cleanse) return SpellDatabase.purifySpell;
                else return null;
            }
            else return null;
        }
    }

    //Cast Spell Logic
    public void castSpell(Spell preparedSpell)
    {
        //Get Affected Positions
        Vector2[] affectedArea = SpellRangeOverlay.Instance.getAffectedArea();

        //Spend Mana
        ControllerManager.Instance.getManaController().spendMana(preparedSpell.manaCost - ControllerManager.Instance.getCorruptionController().getCorruptionBonuses());

        //Remove Text Balloon
        Player.Instance.setManaTextBalloon(false);

        //Spell Logic
        #region Flash Freeze
        if (preparedSpell.name == SpellName.FlashFreeze)
        {
            //Animate
            Player.Instance.setCastingAnimation(0);
            ControllerManager.Instance.getScreenFlashController().flashScreen(2f);
            ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().flashFreezeClip);

            //Iterate Affected Area
            List<Enemy> affectedEnemies = new List<Enemy>();
            for (int i = 0; i < affectedArea.Length; i++)
            {
                //Check for Puddles
                ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(affectedArea[i], EnvironmentCondition.Ice);

                //Check for Enemies
                Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[i], new Vector2(0.95f, 0.95f), 0f);
                for (int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.CompareTag("Enemy"))
                    {
                        Enemy enemy = collisions[j].gameObject.GetComponent<Enemy>();
                        if (!affectedEnemies.Contains(enemy))
                        {
                            enemy.setCondition(Condition.Frozen, ControllerManager.Instance.getConditionController().frozenDuration);
                            affectedEnemies.Add(enemy);
                        }
                    }
                }
            }
        }
        #endregion
        #region Fire Blast
        else if (preparedSpell.name == SpellName.FireBlast)
        {
            //Animate
            Player.Instance.setCastingAnimation(1);
            ControllerManager.Instance.getScreenShakeController().screenShake(0.1f, 0.2f);
            ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().fireClip);

            //Iterate Affected Area
            List<Enemy> affectedEnemies = new List<Enemy>();
            for (int i = 0; i < affectedArea.Length; i++)
            {
                //Check for Puddles
                ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(affectedArea[i], EnvironmentCondition.Fire);

                //If Area is Mage, Ignore
                if (affectedArea[i] == (Vector2) Player.Instance.gameObject.transform.position) continue;

                //Spawn Fire
                ControllerManager.Instance.getSpellEffectController().spawnEffect(SpellDatabase.fireBlastSpell, affectedArea[i]);

                //Check for Affected Enemies
                Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[i], new Vector2(0.95f, 0.95f), 0f);
                for (int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.CompareTag("Enemy"))
                    {
                        Enemy enemy = collisions[j].gameObject.GetComponent<Enemy>();
                        if (!affectedEnemies.Contains(enemy))
                        {
                            enemy.setCondition(Condition.Ablaze, 0f);
                            enemy.takeDamage(SpellDatabase.fireBlastSpell.damage);
                            affectedEnemies.Add(enemy);
                        }
                    }
                }
            }
        }
        #endregion
        #region Lightning Strike
        else if (preparedSpell.name == SpellName.LightningStrike)
        {
            //Animate
            Player.Instance.setCastingAnimation(2);
            ControllerManager.Instance.getScreenShakeController().screenShake(0.3f, 0.5f);
            ControllerManager.Instance.getScreenFlashController().flashScreen(0.8f);
            ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().thunderClip2);

            //Spawn Lightning
            ControllerManager.Instance.getSpellEffectController().spawnEffect(SpellDatabase.lightningStrikeSpell, affectedArea[0]);

            //Check for Puddles
            EnvironmentCondition affectedTile = ControllerManager.Instance.getEnvironmentController().getEnvironmentCondition(affectedArea[0]);
            if (affectedTile == EnvironmentCondition.Puddle || affectedTile == EnvironmentCondition.PuddleAndShock)
            {
                Vector2[] affectedTiles = ControllerManager.Instance.getEnvironmentController().getConnectedPuddles(affectedArea[0]);
                for (int i = 0; i < affectedTiles.Length; i++)
                {
                    ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(affectedTiles[i], EnvironmentCondition.Shock);
                }
            }

            //Check for Affected Enemies
            Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[0], new Vector2(0.95f, 0.95f), 0f);
            for (int j = 0; j < collisions.Length; j++)
            {
                if (collisions[j].gameObject.CompareTag("Enemy"))
                {
                    collisions[j].gameObject.GetComponent<Enemy>().takeDamage(SpellDatabase.lightningStrikeSpell.damage);
                }
            }
        }
        #endregion
        #region Tornado
        else if (preparedSpell.name == SpellName.Tornado)
        {
            //Animate
            Player.Instance.setCastingAnimation(3);
            ControllerManager.Instance.getScreenShakeController().screenShake(0.05f, 0.8f);

            //Spawn Tornado
            ControllerManager.Instance.getSpellEffectController().spawnEffect(SpellDatabase.tornadoSpell, new Vector2(affectedArea[0].x - 0.1f, affectedArea[0].y + 0.3f));
        }
        #endregion
        #region Summon Rain
        else if (preparedSpell.name == SpellName.SummonRain)
        {
            //Animate
            Player.Instance.setCastingAnimation(4);
            ControllerManager.Instance.getScreenFlashController().flashScreen(1f);

            //Activate Rain
            ControllerManager.Instance.getSpellEffectController().activateRain(16f, 1f);
        }
        #endregion
        #region Cleanse
        else if (preparedSpell.name == SpellName.Cleanse)
        {
            //Animate
            Player.Instance.setCastingAnimation(5);
            ControllerManager.Instance.getScreenFlashController().flashScreen(3f);
            ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().cleanseClip);

            //Spawn Effect
            ControllerManager.Instance.getSpellEffectController().spawnEffect(SpellDatabase.cleanseSpell, Vector2.zero);

            //Heal!
            ControllerManager.Instance.getCorruptionController().gainCorruption(SpellDatabase.cleanseSpell.damage);
        }
        #endregion
        #region Deep Freeze
        else if (preparedSpell.name == SpellName.DeepFreeze)
        {
            //Animate
            Player.Instance.setCastingAnimation(0);
            ControllerManager.Instance.getScreenFlashController().flashScreen(2f);
            ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().flashFreezeClip);

            //Iterate Affected Area
            List<Enemy> affectedEnemies = new List<Enemy>();
            for (int i = 0; i < affectedArea.Length; i++)
            {
                //Check for Puddles
                ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(affectedArea[i], EnvironmentCondition.Ice);

                //Check for Enemies
                Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[i], new Vector2(0.95f, 0.95f), 0f);
                for (int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.CompareTag("Enemy"))
                    {
                        Enemy enemy = collisions[j].gameObject.GetComponent<Enemy>();
                        if (!affectedEnemies.Contains(enemy))
                        {
                            enemy.setCondition(Condition.Frozen, ControllerManager.Instance.getConditionController().frozenDuration * 2);
                            affectedEnemies.Add(enemy);
                        }
                    }
                }
            }
        }
        #endregion
        #region Hell Fire
        else if (preparedSpell.name == SpellName.Hellfire)
        {
            //Animate
            Player.Instance.setCastingAnimation(1);
            ControllerManager.Instance.getScreenShakeController().screenShake(0.2f, 0.3f);
            ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().fireClip);

            //Iterate Affected Area
            List<Enemy> affectedEnemies = new List<Enemy>();
            for (int i = 0; i < affectedArea.Length; i++)
            {
                //Check for Puddles
                ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(affectedArea[i], EnvironmentCondition.Fire);

                //If Area is Mage, Ignore
                if (affectedArea[i] == (Vector2)Player.Instance.gameObject.transform.position) continue;

                //Spawn Fire
                ControllerManager.Instance.getSpellEffectController().spawnEffect(SpellDatabase.fireBlastSpell, affectedArea[i]);

                //Check for Affected Enemies
                Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[i], new Vector2(0.95f, 0.95f), 0f);
                for (int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.CompareTag("Enemy"))
                    {
                        Enemy enemy = collisions[j].gameObject.GetComponent<Enemy>();
                        if (!affectedEnemies.Contains(enemy))
                        {
                            enemy.setCondition(Condition.Ablaze, ControllerManager.Instance.getConditionController().ablazeDuration);
                            enemy.takeDamage(SpellDatabase.hellFireSpell.damage);
                            affectedEnemies.Add(enemy);
                        }
                    }
                }
            }
        }
        #endregion
        #region Superbolt
        else if (preparedSpell.name == SpellName.Superbolt)
        {
            //Animate
            Player.Instance.setCastingAnimation(2);

            //Juicyness
            ControllerManager.Instance.getScreenShakeController().screenShake(0.3f, 0.5f);
            ControllerManager.Instance.getScreenFlashController().flashScreen(0.8f);
            ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().thunderClip1);

            //Spawn Lightning
            ControllerManager.Instance.getSpellEffectController().spawnEffect(SpellDatabase.superboltSpell, affectedArea[0]);

            //Check for Puddles
            EnvironmentCondition affectedTile = ControllerManager.Instance.getEnvironmentController().getEnvironmentCondition(affectedArea[0]);
            if (affectedTile == EnvironmentCondition.Puddle || affectedTile == EnvironmentCondition.Shock)
            {
                Vector2[] affectedTiles = ControllerManager.Instance.getEnvironmentController().getConnectedPuddles(affectedArea[0]);
                for (int i = 0; i < affectedTiles.Length; i++)
                {
                    ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(affectedTiles[i], EnvironmentCondition.Shock);
                }
            }

            //Shock All Adjacent
            ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(affectedArea[0], EnvironmentCondition.Shock);
            List<Vector2> adjacent = ControllerManager.Instance.getEnvironmentController().getAdjacentTiles(affectedArea[0]);
            for(int i = 0; i < adjacent.Count; i++)
            {
                ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(adjacent[i], EnvironmentCondition.Shock);
                affectedTile = ControllerManager.Instance.getEnvironmentController().getEnvironmentCondition(adjacent[i]);
                if (affectedTile == EnvironmentCondition.Puddle || affectedTile == EnvironmentCondition.PuddleAndShock)
                {
                    Vector2[] affectedTiles = ControllerManager.Instance.getEnvironmentController().getConnectedPuddles(adjacent[i]);
                    for (int j = 0; j < affectedTiles.Length; j++)
                    {
                        ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(affectedTiles[j], EnvironmentCondition.Shock);
                    }
                }
            }

            //Check for Affected Enemies
            Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[0], new Vector2(0.95f, 0.95f), 0f);
            for (int j = 0; j < collisions.Length; j++)
            {
                if (collisions[j].gameObject.CompareTag("Enemy"))
                {
                    collisions[j].gameObject.GetComponent<Enemy>().takeDamage(SpellDatabase.superboltSpell.damage);
                }
            }

            //Check for Enemies in Adjacent Tiles
            for(int i = 0; i < adjacent.Count; i++)
            {
                collisions = Physics2D.OverlapBoxAll(adjacent[i], new Vector2(0.95f, 0.95f), 0f);
                for (int j = 0; j < collisions.Length; j++)
                {
                    if (collisions[j].gameObject.CompareTag("Enemy"))
                    {
                        collisions[j].gameObject.GetComponent<Enemy>().takeDamage(10);
                    }
                }
            }
        }
        #endregion
        #region Hurricane
        else if (preparedSpell.name == SpellName.Hurricane)
        {
            //Animate
            Player.Instance.setCastingAnimation(3);
            ControllerManager.Instance.getScreenShakeController().screenShake(0.08f, 0.8f);

            //Spawn Hurricane
            ControllerManager.Instance.getSpellEffectController().spawnEffect(SpellDatabase.hurricaneSpell, new Vector2(affectedArea[0].x - 0.1f, affectedArea[0].y + 0.3f));
        }
        #endregion
        #region Cloudburst
        else if (preparedSpell.name == SpellName.Cloudburst)
        {
            //Animate
            Player.Instance.setCastingAnimation(4);
            ControllerManager.Instance.getScreenFlashController().flashScreen(1f);

            //Activate Rain
            ControllerManager.Instance.getSpellEffectController().activateRain(16f, 0.5f);
        }
        #endregion
        #region Purify
        else if (preparedSpell.name == SpellName.Purify)
        {
            //Animate
            Player.Instance.setCastingAnimation(5);
            ControllerManager.Instance.getScreenFlashController().flashScreen(3f);
            ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().purifyClip);

            //Spawn Effect
            ControllerManager.Instance.getSpellEffectController().spawnEffect(SpellDatabase.cleanseSpell, Vector2.zero);

            //Heal!
            ControllerManager.Instance.getCorruptionController().gainCorruption(SpellDatabase.purifySpell.damage);
        }
        #endregion
        #region Thunder Storm
        else if (preparedSpell.name == SpellName.ThunderStorm)
        {
            //Animate
            Player.Instance.setCastingAnimation(2);
            ControllerManager.Instance.getScreenFlashController().flashScreen(3f);

            //Activate Rain
            ControllerManager.Instance.getSpellEffectController().activateThunderStorm(10f, 1f, 1.5f);
        }
        #endregion
        #region Hail Storm
        else if (preparedSpell.name == SpellName.HailStorm)
        {
            //Animate
            Player.Instance.setCastingAnimation(4);
            ControllerManager.Instance.getScreenShakeController().screenShake(0.2f, 0.2f);
            ControllerManager.Instance.getScreenFlashController().flashScreen(1f);

            //Activate Hail Storm
            ControllerManager.Instance.getSpellEffectController().activateHailStorm(10f, 1f);
        }
        #endregion
        #region Typhoon
        else if (preparedSpell.name == SpellName.Typhoon)
        {
            //Animate
            Player.Instance.setCastingAnimation(3);
            ControllerManager.Instance.getScreenShakeController().screenShake(0.05f, 0.8f);

            //Spawn Tornado
            ControllerManager.Instance.getSpellEffectController().spawnEffect(SpellDatabase.tornadoSpell, new Vector2(affectedArea[0].x - 0.1f, affectedArea[0].y + 0.3f));

            //Activate Rain
            ControllerManager.Instance.getSpellEffectController().activateRain(10f, 1f);
        }
        #endregion
        #region Super Cell
        else if (preparedSpell.name == SpellName.SuperCell)
        {
            //Animate
            Player.Instance.setCastingAnimation(3);
            ControllerManager.Instance.getScreenShakeController().screenShake(0.3f, 0.5f);
            ControllerManager.Instance.getScreenFlashController().flashScreen(0.8f);

            //Spawn Tornado
            ControllerManager.Instance.getSpellEffectController().spawnEffect(SpellDatabase.tornadoSpell, new Vector2(affectedArea[0].x - 0.1f, affectedArea[0].y + 0.3f));

            //Spawn Lightning
            ControllerManager.Instance.getSpellEffectController().spawnEffect(SpellDatabase.lightningStrikeSpell, affectedArea[0]);
            ControllerManager.Instance.getSoundController().playSound(ControllerManager.Instance.getSoundController().thunderClip2);

            //Check for Puddles
            EnvironmentCondition affectedTile = ControllerManager.Instance.getEnvironmentController().getEnvironmentCondition(affectedArea[0]);
            if (affectedTile == EnvironmentCondition.Puddle || affectedTile == EnvironmentCondition.Shock)
            {
                Vector2[] affectedTiles = ControllerManager.Instance.getEnvironmentController().getConnectedPuddles(affectedArea[0]);
                for (int i = 0; i < affectedTiles.Length; i++)
                {
                    ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(affectedTiles[i], EnvironmentCondition.Shock);
                }
            }

            //Check for Affected Enemies
            Collider2D[] collisions = Physics2D.OverlapBoxAll(affectedArea[0], new Vector2(0.95f, 0.95f), 0f);
            for (int j = 0; j < collisions.Length; j++)
            {
                if (collisions[j].gameObject.CompareTag("Enemy"))
                {
                    collisions[j].gameObject.GetComponent<Enemy>().takeDamage(SpellDatabase.lightningStrikeSpell.damage);
                }
            }

        }
        #endregion
        #region Fire Storm
        else if (preparedSpell.name == SpellName.FireStorm)
        {
            //Animate
            Player.Instance.setCastingAnimation(1);
            ControllerManager.Instance.getScreenShakeController().screenShake(0.05f, 0.8f);

            //Spawn Tornado
            ControllerManager.Instance.getSpellEffectController().spawnEffect(SpellDatabase.fireStormSpell, new Vector2(affectedArea[0].x - 0.1f, affectedArea[0].y + 0.3f));
        }
        #endregion
        #region Blizzard
        else if (preparedSpell.name == SpellName.Blizzard)
        {
            //Animate
            Player.Instance.setCastingAnimation(3);
            ControllerManager.Instance.getScreenFlashController().whiteOut(10f, 1f);

            //Activate Hail Storm
            ControllerManager.Instance.getSpellEffectController().activateBlizzard(10f, 0.5f);
        }
        #endregion

        //Reset Prepared Spells
        resetPreparedSpells();

        //Reset Spell Range Overlay
        SpellRangeOverlay.Instance.disableSpellOverlay();

        //Reset Cursor
        ControllerManager.Instance.getCursorChangerController().resetMouse();
    }

    // Update is called once per frame
    void Update ()
    {
        //Right Mouse Button
        if (Input.GetMouseButtonDown(1)) resetCurrentPreparedSpell();
        else
        {
            //If Mouse is Released
            Spell preparedSpell = getPreparedSpell();
            if (preparedSpell != null)
            {
                //Check Mana Levels
                bool hasMana = ControllerManager.Instance.getManaController().getCurrentMana() >= preparedSpell.manaCost - ControllerManager.Instance.getCorruptionController().getCorruptionBonuses();

                //Update Text Ballon
                Player.Instance.setManaTextBalloon(!hasMana);

                //Update Prepared Spell Text
                updateGrimmoireFeedback(preparedSpell);

                //If Cast Runes is Not Active (i.e. Not Drawing)
                if (!CastRunes.Instance.isActive())
                {
                    //Update Cursor
                    ControllerManager.Instance.getCursorChangerController().changeMouse(preparedSpell, hasMana);

                    //Set Spell Range Overlay
                    if (preparedSpell.areaType != SpellAreaType.All) SpellRangeOverlay.Instance.setSpellOverlay(preparedSpell.areaType, preparedSpell.spellRange, Camera.main.ScreenToWorldPoint(Input.mousePosition));

                    //Check Mouse Click & Check if You have Enough Mana
                    if (Input.GetMouseButtonDown(0) && hasMana) castSpell(preparedSpell);
                }
            }
        }
    }
}
