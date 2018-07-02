using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorruptionController : MonoBehaviour
{
    //References
    public Text corruptionText;
    public RuntimeAnimatorController corruptAnimationController;
    public GameObject devourerGameOver;
    public GameObject corruptionParticles;
    public Image portrait;

    //Portrait References
    public Sprite spriteCorruption0;
    public Sprite spriteCorruption20;
    public Sprite spriteCorruption40;
    public Sprite spriteCorruption60;
    public Sprite spriteCorruption80;
    public Sprite spriteCorruption100;

    //Varibles
    private int corruption;
    private bool gameOver;
    private bool walkAway;
    private GameObject player;
    private float devourerSpawnTime;
    private float currentSpawnInterval;

    //Start Method
    private void Start()
    {
        corruption = 0;
        gameOver = false;
        walkAway = false;
        player = Player.Instance.gameObject;
        devourerSpawnTime = 5f;
        currentSpawnInterval = devourerSpawnTime;
    }

    //Get Current Corruption
    public int getCorruption()
    {
        return corruption;
    }

    //Is GameOver?
    public bool isGameOver()
    {
        return gameOver;
    }

    //Get Corruption Bonuses
    public int getCorruptionBonuses()
    {
        if (corruption < 20) return 0;
        else if (corruption >= 20 && corruption < 40) return 5;
        else if (corruption >= 40 && corruption < 60) return 10;
        else if (corruption >= 60 && corruption < 80) return 15;
        else return 20;
    }

    //Trigger Game Over
    private void triggerGameOver()
    {
        gameOver = true;
        ControllerManager.Instance.getGrimmoireController().resetPreparedSpells();
        ControllerManager.Instance.getScreenFlashController().flashBlackScreen(1f);
        Player.Instance.enabled = false;
        player.GetComponent<Animator>().runtimeAnimatorController = corruptAnimationController;
    }

    //Gain Corruption
    public void gainCorruption(int value)
    {
        if(value >= 0) corruption = Mathf.Min(corruption + value, 100);
        else corruption = Mathf.Max(corruption + value, 0);

        //Change Feedback
        if (corruption < 20)
        {
            corruptionText.text = "0";
            portrait.sprite = spriteCorruption0;
        }
        else if (corruption >= 20 && corruption < 40)
        {
            corruptionText.text = "5";
            portrait.sprite = spriteCorruption20;
        }
        else if (corruption >= 40 && corruption < 60)
        {
            corruptionText.text = "10";
            portrait.sprite = spriteCorruption40;
        }
        else if (corruption >= 60 && corruption < 80)
        {
            corruptionText.text = "15";
            portrait.sprite = spriteCorruption60;
        }
        else if (corruption >= 80 && corruption < 100)
        {
            corruptionText.text = "20";
            portrait.sprite = spriteCorruption80;
        }
        else if (corruption >= 100 && !gameOver)
        {
            corruptionText.text = "66";
            portrait.sprite = spriteCorruption100;
            triggerGameOver();
        }
    }

    //Spawn Corruption Particles
    public void spawnCorruptionParticles(Vector2 position)
    {
        Instantiate(corruptionParticles, position, Quaternion.identity);
    }

    //Update Method
    private void Update()
    {
        if (gameOver)
        {
            if (walkAway)
            {
                player.transform.position = Vector2.MoveTowards(player.transform.position, new Vector2(-9.5f, this.transform.position.y), Time.deltaTime * 0.3f);
                if (currentSpawnInterval <= 0f)
                {
                    Vector2 laneStart = ControllerManager.Instance.getEnemySpawner().getRandomLane();
                    laneStart.x = laneStart.x - Random.Range(1.5f, 4.5f);
                    if (!Physics2D.Raycast(laneStart, Vector2.left, 10f))
                    {
                        Instantiate(devourerGameOver, laneStart, Quaternion.identity);
                        currentSpawnInterval = devourerSpawnTime;
                    }
                }
                else currentSpawnInterval -= Time.deltaTime;
            }
            else if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.5f)
            {
                player.GetComponent<Animator>().SetTrigger("Advance");
                walkAway = true;
            }
        }
    }
}
