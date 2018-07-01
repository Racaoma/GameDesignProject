using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct EnemyType
{
    public GameObject enemy;
    public EnemyPool enemyPool;

    public EnemyType(GameObject enemy, EnemyPool enemyPool)
    {
        this.enemy = enemy;
        this.enemyPool = enemyPool;
    }
};

public class EnemySpawner : MonoBehaviour
{
    //List of Enemies
    public GameObject[] possibleEnemies;
    private List<EnemyType> possibleEnemiesPool;
    private List<GameObject> currentEnemies;

    //Lanes
    public Vector2[] lanes;

    //Spawn Points
    private int wave;
    private int currentSpawnPoints;

    //Spawn Delay
    public float spawnDelay = 1f;
    private float remainingSpawnDelay;

    //Wave Text Reference
    public Text waveText;

    //Singleton Instance Variable
    private static EnemySpawner instance;
    public static EnemySpawner Instance
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

    // Use this for initialization
    void Start ()
    {
        wave = 1;
        currentSpawnPoints = 5;
        remainingSpawnDelay = 0f;

        //Create Lists
        possibleEnemiesPool = new List<EnemyType>();
        currentEnemies = new List<GameObject>();

        for (int i = 0; i < possibleEnemies.Length; i++)
        {
            GameObject enemy = possibleEnemies[i];
            possibleEnemiesPool.Add(new EnemyType(enemy, new EnemyPool(10, enemy)));
        }
    }

    //DeSpawn Enemy Method
    public void deSpawnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        currentEnemies.Remove(enemy);
    }
	
    //Shuffle Lists
    private void shuffleEnemyList()
    {
        for (int i = 0; i < possibleEnemiesPool.Count; i++)
        {
            EnemyType temp = possibleEnemiesPool[i];
            int randomIndex = Random.Range(i, possibleEnemiesPool.Count);
            possibleEnemiesPool[i] = possibleEnemiesPool[randomIndex];
            possibleEnemiesPool[randomIndex] = temp;
        }
    }

    private void shuffleLaneList()
    {
        for (int i = 0; i < lanes.Length; i++)
        {
            Vector2 temp = lanes[i];
            int randomIndex = Random.Range(i, lanes.Length);
            lanes[i] = lanes[randomIndex];
            lanes[randomIndex] = temp;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //Check if There is Still Enemies to Spawn
        if (currentSpawnPoints > 0)
        {
            //Check for Spawn Delay
            if (remainingSpawnDelay <= 0f)
            {
                shuffleEnemyList();
                shuffleLaneList();

                for (int i = 0; i < possibleEnemiesPool.Count; i++)
                {
                    int cost = possibleEnemiesPool[i].enemy.GetComponent<Enemy>().spawnCost;
                    if (cost > currentSpawnPoints) continue;
                    else
                    {
                        //Get Enemy From Pool
                        GameObject obj = possibleEnemiesPool[i].enemyPool.getItem();
                        currentEnemies.Add(obj);

                        //Get Lane
                        while ((Vector2)obj.transform.position == Vector2.zero)
                        {
                            for (i = 0; i < lanes.Length; i++)
                            {
                                if (Physics2D.OverlapPoint(lanes[i]) == null)
                                {
                                    obj.transform.position = lanes[i];
                                    break;
                                }
                            }
                        }

                        //Set Active!
                        obj.SetActive(true);

                        //Update Internal Variables
                        currentSpawnPoints -= cost;
                        remainingSpawnDelay = spawnDelay * cost;
                        break;
                    }
                }
            }
            else
            {
                //Update Remaining Spawn Delay
                remainingSpawnDelay -= Time.deltaTime;
            }
        }
        else
        {
            //Check if Wave is Cleared
            if (currentEnemies.Count == 0)
            {
                wave++;
                currentSpawnPoints = 5 + (wave * 3);
                remainingSpawnDelay = 3f;
                waveText.text = "Current Wave: " + wave;
            }
        }
	}
}
