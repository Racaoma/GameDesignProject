using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    private void FixedUpdate()
    {
        Collider2D result = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1 << 10);
        if (result != null) result.gameObject.GetComponent<RuneSphere>().triggerSphere();
    }
}
