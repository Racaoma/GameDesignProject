using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Fire : MonoBehaviour
{
    private Animator animator;

	// Use this for initialization
	void Start ()
    {
        animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) Destroy(this.gameObject);
	}
}
