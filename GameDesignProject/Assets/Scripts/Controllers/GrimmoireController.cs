using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrimmoireController : MonoBehaviour
{
    //References
    public Image preparedSpell1_Image;
    public Image preparedSpell2_Image;
    public Text preparedSpellText;

    //Spell Icons
    public Sprite defaultRune1;
    public Sprite defaultRune2;
    public Sprite flashFreezeIcon;
    public Sprite fireBlastIcon;
    public Sprite lightningStrikeIcon;
    public Sprite tornadoIcon;
    public Sprite summonRainIcon;
    public Sprite cleanseIcon;

    //Variables
    private Spell preparedSpell1;
    private Spell preparedSpell2;

    //Cancel Both Prepared Spell
    public void resetPreparedSpells()
    {
        preparedSpell1 = null;
        preparedSpell2 = null;
        resetGrimmoireIcons();
        updatePreparedSpellText(null);
        SpellRangeOverlay.Instance.disableSpellOverlay();
        ControllerManager.Instance.getCursorChangerController().resetMouse();
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

    //Update Grimmoire to Spell Icon
    private void updateGrimmoireIcon(int pos, SpellName newSpell)
    {
        if(pos == 0)
        {
            if (newSpell == SpellName.FlashFreeze) preparedSpell1_Image.sprite = flashFreezeIcon;
            else if (newSpell == SpellName.FireBlast) preparedSpell1_Image.sprite = fireBlastIcon;
            else if (newSpell == SpellName.LightningStrike) preparedSpell1_Image.sprite = lightningStrikeIcon;
            else if (newSpell == SpellName.Tornado) preparedSpell1_Image.sprite = tornadoIcon;
            else if (newSpell == SpellName.SummonRain) preparedSpell1_Image.sprite = summonRainIcon;
            else if (newSpell == SpellName.Cleanse) preparedSpell1_Image.sprite = cleanseIcon;
        }
        else
        {
            if (newSpell == SpellName.FlashFreeze) preparedSpell2_Image.sprite = flashFreezeIcon;
            else if (newSpell == SpellName.FireBlast) preparedSpell2_Image.sprite = fireBlastIcon;
            else if (newSpell == SpellName.LightningStrike) preparedSpell2_Image.sprite = lightningStrikeIcon;
            else if (newSpell == SpellName.Tornado) preparedSpell2_Image.sprite = tornadoIcon;
            else if (newSpell == SpellName.SummonRain) preparedSpell2_Image.sprite = summonRainIcon;
            else if (newSpell == SpellName.Cleanse) preparedSpell2_Image.sprite = cleanseIcon;
        }
    }

    //Reset Grimmoire
    private void resetGrimmoireIcons()
    {
        preparedSpell1_Image.sprite = defaultRune1;
        preparedSpell2_Image.sprite = defaultRune2;
    }

    //Update Prepared Spell Text
    public void updatePreparedSpellText(Spell spell)
    {
        if (spell == null) preparedSpellText.text = "No Spell Prepared";
        else
        {
            int corruptionBonus = ControllerManager.Instance.getCorruptionController().getCorruptionBonuses();
            int manaCost = spell.manaCost - corruptionBonus;
            if(corruptionBonus == 0)
            {
                if (spell.name == SpellName.FlashFreeze) preparedSpellText.text = "Flash Freeze\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.FireBlast) preparedSpellText.text = "Fire Blast\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.LightningStrike) preparedSpellText.text = "Lightning Strike\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.Tornado) preparedSpellText.text = "Tornado\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.SummonRain) preparedSpellText.text = "Summon Rain\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.Cleanse) preparedSpellText.text = "Cleanse\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.DeepFreeze) preparedSpellText.text = "Deep Freeze\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.Hellfire) preparedSpellText.text = "Hell Fire\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.Superbolt) preparedSpellText.text = "Superbolt\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.Hurricane) preparedSpellText.text = "Hurricane\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.Cloudburst) preparedSpellText.text = "Cloudburst\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.Purify) preparedSpellText.text = "Purify\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.ThunderStorm) preparedSpellText.text = "Thunder Storm\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.HailStorm) preparedSpellText.text = "Hail Storm\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.Typhoon) preparedSpellText.text = "Typhoon\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.SuperCell) preparedSpellText.text = "Super Cell\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.FireStorm) preparedSpellText.text = "Fire Storm\nMana Cost = " + manaCost;
                else if (spell.name == SpellName.Blizzard) preparedSpellText.text = "Blizzard\nMana Cost = " + manaCost;
            }
            else
            {
                if (spell.name == SpellName.FlashFreeze) preparedSpellText.text = "Flash Freeze\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.FireBlast) preparedSpellText.text = "Fire Blast\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.LightningStrike) preparedSpellText.text = "Lightning Strike\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.Tornado) preparedSpellText.text = "Tornado\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.SummonRain) preparedSpellText.text = "Summon Rain\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.Cleanse) preparedSpellText.text = "Cleanse\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.DeepFreeze) preparedSpellText.text = "Deep Freeze\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.Hellfire) preparedSpellText.text = "Hell Fire\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.Superbolt) preparedSpellText.text = "Superbolt\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.Hurricane) preparedSpellText.text = "Hurricane\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.Cloudburst) preparedSpellText.text = "Cloudburst\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.Purify) preparedSpellText.text = "Purify\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.ThunderStorm) preparedSpellText.text = "Thunder Storm\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.HailStorm) preparedSpellText.text = "Hail Storm\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.Typhoon) preparedSpellText.text = "Typhoon\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.SuperCell) preparedSpellText.text = "Super Cell\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.FireStorm) preparedSpellText.text = "Fire Storm\nMana Cost = <color=maroon>" + manaCost + "</color>";
                else if (spell.name == SpellName.Blizzard) preparedSpellText.text = "Blizzard\nMana Cost = <color=maroon>" + manaCost + "</color>";
            }
        }
    } 

    //Prepare Spell Verifier
    public void prepareNewSpell(Spell newSpell)
    {
        if (newSpell != null)
        {
            if (preparedSpell1 == null)
            {
                preparedSpell1 = newSpell;
                updateGrimmoireIcon(0, newSpell.name);
            }
            else
            {
                if (preparedSpell1.name == SpellName.FlashFreeze)
                {
                    if (newSpell.name == SpellName.FlashFreeze || newSpell.name == SpellName.Tornado || newSpell.name == SpellName.SummonRain) preparedSpell2 = newSpell;
                    else resetPreparedSpells();
                }
                else if (preparedSpell1.name == SpellName.FireBlast)
                {
                    if (newSpell.name == SpellName.FireBlast || newSpell.name == SpellName.Tornado) preparedSpell2 = newSpell;
                    else resetPreparedSpells();
                }
                else if (preparedSpell1.name == SpellName.LightningStrike)
                {
                    if (newSpell.name == SpellName.LightningStrike || newSpell.name == SpellName.Tornado || newSpell.name == SpellName.SummonRain) preparedSpell2 = newSpell;
                    else resetPreparedSpells();
                }
                else if (preparedSpell1.name == SpellName.Tornado)
                {
                    if (newSpell.name == SpellName.Tornado || newSpell.name == SpellName.FlashFreeze || newSpell.name == SpellName.FireBlast || newSpell.name == SpellName.LightningStrike || newSpell.name == SpellName.SummonRain) preparedSpell2 = newSpell;
                    else resetPreparedSpells();
                }
                else if (preparedSpell1.name == SpellName.SummonRain)
                {
                    if (newSpell.name == SpellName.SummonRain || newSpell.name == SpellName.FlashFreeze || newSpell.name == SpellName.LightningStrike || newSpell.name == SpellName.Tornado) preparedSpell2 = newSpell;
                    else resetPreparedSpells();
                }
                else if (preparedSpell1.name == SpellName.Cleanse)
                {
                    if (newSpell.name == SpellName.Cleanse) preparedSpell2 = newSpell;
                    else resetPreparedSpells();
                }

                //Update Grimmoire Icon
                if (preparedSpell2 != null) updateGrimmoireIcon(1, newSpell.name);
            }
        }
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
                else if (preparedSpell2.name == SpellName.Tornado) return SpellDatabase.blizzardSpell;
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
        if (preparedSpell.name == SpellName.FlashFreeze)
        {
            //Animate
            Player.Instance.setCastingAnimation(0);

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
        else if (preparedSpell.name == SpellName.FireBlast)
        {
            //Animate
            Player.Instance.setCastingAnimation(1);
            ControllerManager.Instance.getScreenShakeController().screenShake(0.1f, 0.2f);

            //Iterate Affected Area
            List<Enemy> affectedEnemies = new List<Enemy>();
            for (int i = 0; i < affectedArea.Length; i++)
            {
                //Check for Puddles
                ControllerManager.Instance.getEnvironmentController().setEnvironmentCondition(affectedArea[i], EnvironmentCondition.Fire);

                //If Area is Mage, Ignore
                if (affectedArea[i] == (Vector2) Player.Instance.gameObject.transform.position) continue;

                //Spawn Fire
                ControllerManager.Instance.getSpellEffectController().spawnEffect(preparedSpell, affectedArea[i]);

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
                            enemy.takeDamage(preparedSpell);
                            affectedEnemies.Add(enemy);
                        }
                    }
                }
            }
        }
        else if (preparedSpell.name == SpellName.LightningStrike)
        {
            //Animate
            Player.Instance.setCastingAnimation(2);

            //Juicyness
            ControllerManager.Instance.getScreenShakeController().screenShake(0.3f, 0.5f);
            ControllerManager.Instance.getScreenFlashController().flashScreen(0.8f);

            //Spawn Lightning
            ControllerManager.Instance.getSpellEffectController().spawnEffect(preparedSpell, affectedArea[0]);

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
                    collisions[j].gameObject.GetComponent<Enemy>().takeDamage(preparedSpell);
                }
            }
        }
        else if (preparedSpell.name == SpellName.Tornado)
        {
            //Animate
            Player.Instance.setCastingAnimation(3);

            //Spawn Hurricane
            ControllerManager.Instance.getSpellEffectController().spawnEffect(preparedSpell, new Vector2(affectedArea[0].x - 0.1f, affectedArea[0].y + 0.3f));
        }
        else if (preparedSpell.name == SpellName.SummonRain)
        {
            //Animate
            Player.Instance.setCastingAnimation(4);

            //Activate Rain
            ControllerManager.Instance.getSpellEffectController().activateRain(10f, 1f);
        }
        else if (preparedSpell.name == SpellName.Cleanse)
        {
            //Animate
            Player.Instance.setCastingAnimation(5);

            //Spawn Effect
            ControllerManager.Instance.getSpellEffectController().spawnEffect(preparedSpell, Vector2.zero);

            //Heal!
            ControllerManager.Instance.getCorruptionController().gainCorruption(-10);
        }

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
                updatePreparedSpellText(preparedSpell);

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
