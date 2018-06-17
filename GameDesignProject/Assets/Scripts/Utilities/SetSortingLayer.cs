using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SetSortingLayer : MonoBehaviour
{
    public int sortingOrder = 1;

	private void Start()
    {
        var renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = "UI";
        renderer.sortingOrder = sortingOrder;
    }
}
