using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrike : MonoBehaviour
{
    //Variables
    public float fade;
    private SpriteRenderer spriteRenderer;

    //Start Method
    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update ()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, spriteRenderer.color.a - (fade * Time.deltaTime));
        if (spriteRenderer.color.a <= 0f) Destroy(this.gameObject);
    }
}
