using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    public int hitPoints;
    public float speed;
    public int spawnCost;
    public Action<GameObject> onDeathAction;

    //Kill Enemy
    public void killEnemy()
    {
        this.transform.position = Vector2.zero;
        if (onDeathAction != null) onDeathAction(this.gameObject);
    }

    //Enemy Arrived at Destination
    public void retireEnemy()
    {
        //TODO: Something
        killEnemy();
    }

    //Action Safeguard
    private void OnDestroy()
    {
        onDeathAction = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Check if Dead
        if (hitPoints == 0) killEnemy();
        else
        {
            //Move!
            Vector2 target = new Vector2(-9.5f, this.transform.position.y);
            if(((Vector2)this.transform.position - target) == Vector2.zero) retireEnemy();
            else this.transform.position = Vector2.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        }
    }
}
