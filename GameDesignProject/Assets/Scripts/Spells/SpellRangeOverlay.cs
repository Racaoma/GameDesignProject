using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellRangeOverlay : MonoBehaviour
{
    //Map Dimensions
    public int width = 18;
    public int height = 9;

    //Current Active Overlays
    private LinkedList<GameObject> activeOverlays;

    //Current Spell Center
    private Vector2 currentSpellCenter;

    //Start Method
    private void Start()
    {
        activeOverlays = new LinkedList<GameObject>();
        currentSpellCenter = Vector2.negativeInfinity;
    }

    //Enable Spell Overlay Method
    public void setSpellOverlay(SpellAreaType areaType, int size, Vector2 center)
    {
        //Check if Inside Bounds (Bounds X-Axis fixed by Camera)
        if (center.y > -5f && center.y < 4f && center.x > -8.5f && center.x < 8.5f)
        {
            //Get Cell Number
            int horizontalCell = ((int)(center.x + 9f));
            int verticalCell = ((int)(center.y + 5f));

            //Setup Center
            center = new Vector2(horizontalCell, verticalCell);

            //Check Spell Center
            if (center == currentSpellCenter) return;
            else currentSpellCenter = center;

            //Reset Spell Overlay
            resetSpellOverlay();

            //Check Area Type
            if (areaType == SpellAreaType.Square)
            {
                //TODO
            }
            else if (areaType == SpellAreaType.Circle || areaType == SpellAreaType.Cross)
            {
                //Make Center
                GameObject obj = this.transform.GetChild((verticalCell * width) + horizontalCell).gameObject;
                obj.SetActive(true);
                activeOverlays.AddLast(obj);

                //Make Cross
                for (int i = 1; i < size; i++)
                {
                    //UP
                    if(insideArea(verticalCell+i, horizontalCell))
                    {
                        obj = this.transform.GetChild(((verticalCell + i) * width) + horizontalCell).gameObject;
                        obj.SetActive(true);
                        activeOverlays.AddLast(obj);
                    }
                    //DOWN
                    if (insideArea(verticalCell - i, horizontalCell))
                    {
                        obj = this.transform.GetChild(((verticalCell - i) * width) + horizontalCell).gameObject;
                        obj.SetActive(true);
                        activeOverlays.AddLast(obj);
                    }
                    //LEFT
                    if (insideArea(verticalCell, horizontalCell - i))
                    {
                        obj = this.transform.GetChild((verticalCell * width) + horizontalCell - i).gameObject;
                        obj.SetActive(true);
                        activeOverlays.AddLast(obj);
                    }
                    //RIGHT
                    if (insideArea(verticalCell, horizontalCell + i))
                    {
                        obj = this.transform.GetChild((verticalCell * width) + horizontalCell + i).gameObject;
                        obj.SetActive(true);
                        activeOverlays.AddLast(obj);
                    }
                }

                //Make Rest of Circle if Area is Circle
                if(areaType == SpellAreaType.Circle)
                {
                    for (int i = 1; i < size-1; i++)
                    {
                        if (insideArea(verticalCell + i, horizontalCell + i))
                        {
                            obj = this.transform.GetChild(((verticalCell + i) * width) + horizontalCell + i).gameObject;
                            obj.SetActive(true);
                            activeOverlays.AddLast(obj);
                        }
                        if (insideArea(verticalCell - i, horizontalCell - i))
                        {
                            obj = this.transform.GetChild(((verticalCell - i) * width) + horizontalCell - i).gameObject;
                            obj.SetActive(true);
                            activeOverlays.AddLast(obj);
                        }
                        if (insideArea(verticalCell + i, horizontalCell - i))
                        {
                            obj = this.transform.GetChild(((verticalCell + i) * width) + horizontalCell - i).gameObject;
                            obj.SetActive(true);
                            activeOverlays.AddLast(obj);
                        }
                        if (insideArea(verticalCell - i, horizontalCell + i))
                        {
                            obj = this.transform.GetChild(((verticalCell - i) * width) + horizontalCell + i).gameObject;
                            obj.SetActive(true);
                            activeOverlays.AddLast(obj);
                        }
                    }
                }
            }
        }
    }

    //Check if Inside Area
    public bool insideArea(int vertical, int horizontal)
    {
        if (horizontal >= 0 && horizontal < width && vertical >= 0 && vertical < height) return true;
        else return false;
    }

    //Get Affected Area
    public Vector2[] getAffectedArea()
    {
        Vector2[] affectedArea = new Vector2[activeOverlays.Count];

        //Iterate Linked List
        int index = 0;
        foreach (GameObject obj in activeOverlays)
        {
            affectedArea[index] = obj.transform.position;
            index++;
        }

        //Finally...
        return affectedArea;
    }

    //Disable Spell Overlay
    public void disableSpellOverlay()
    {
        resetSpellOverlay();
        currentSpellCenter = Vector2.negativeInfinity;
    }

    //Reset Spell Overlay
    public void resetSpellOverlay()
    {
        //Reset Active Cells
        while(activeOverlays.Count > 0)
        {
            activeOverlays.Last.Value.SetActive(false);
            activeOverlays.RemoveLast();
        }
    }
}
