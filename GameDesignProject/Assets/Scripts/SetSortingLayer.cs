using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSortingLayer : MonoBehaviour
{
	private void Start()
    {
        var renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = "FillEffect";
    }
}
