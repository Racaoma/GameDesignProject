using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    //List of Enemies
    public Stack<GameObject> enemyPool;
    private GameObject prefab;

    //Constructor
    public EnemyPool(int size, GameObject prefab)
    {
        this.prefab = prefab;
        enemyPool = new Stack<GameObject>();

        for(int i = 0; i < size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.GetComponent<Enemy>().onDeathAction += returnItem;
            enemyPool.Push(obj);
        }
    }

    //Get Item Method
    public GameObject getItem()
    {
        if (enemyPool.Count > 0) return enemyPool.Pop();
        else
        {
            GameObject obj = Instantiate(prefab);
            obj.GetComponent<Enemy>().onDeathAction += returnItem;
            return Instantiate(obj);
        }
    }

    //Return Item Method
    public void returnItem(GameObject item)
    {
        EnemySpawner.Instance.deSpawnEnemy(item);
        enemyPool.Push(item);
    }
}
