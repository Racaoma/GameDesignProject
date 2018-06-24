using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devourer : MonoBehaviour
{
    private void FixedUpdate()
    {
        Collider2D[] collisions = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(0.95f, 0.95f), 0f);
        for (int j = 0; j < collisions.Length; j++)
        {
            if (collisions[j].gameObject.tag == "Enemy")
            {
                Enemy enemy = collisions[j].gameObject.GetComponent<Enemy>();
                enemy.killEnemy();
            }
        }
    }
}
