using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    //Map Dimensions
    public int width = 18;
    public int height = 9;

    //Balance Variables
    public float iceDuration;
    public float shockDuration;
    public float puddleDuration;

    //Sprites References
    public Sprite iceSprite;
    public Sprite puddleSprite;

    //Variables
    public GameObject environment;
    private GameObject[] tiles;

    // Use this for initialization
    void Start ()
    {
        tiles = new GameObject[environment.transform.childCount];
        for (int i = 0; i < environment.transform.childCount; i++)
        {
            tiles[i] = environment.transform.GetChild(i).gameObject;
        }
	}

    //Get Environment Condition
    public EnvironmentCondition getEnvironmentCondition(Vector2 position)
    {
        int tileNumber = ((int)(position.y + 5f) * width) + (int)(position.x + 9f);
        if (insideArea(tileNumber)) return tiles[tileNumber].GetComponent<Environment>().currentEnvironmentCondition;
        else return EnvironmentCondition.None;
    }

    //Set Environment Condition
    public void setEnvironmentCondition(Vector2 position, EnvironmentCondition condition, float additionalTime = 0f)
    {
        int tileNumber = ((int)(position.y + 5f) * width) + (int)(position.x + 9f);
        if (insideArea(tileNumber)) tiles[tileNumber].GetComponent<Environment>().setEnvironmentCondition(condition, additionalTime);
    }

    //Check if Inside Area
    public bool insideArea(int cellNumber)
    {
        if (cellNumber >= 0 && cellNumber < tiles.Length) return true;
        else return false;
    }

    //Get All Puddle
    public List<Environment> getAllPuddles()
    {
        List<Environment> result = new List<Environment>();
        for (int x = 0; x < tiles.Length; x++)
        {
            Environment env = tiles[x].GetComponent<Environment>();
            if (env.currentEnvironmentCondition == EnvironmentCondition.Puddle) result.Add(env);
        }
        return result;
    }

    //Get Adjacent Tiles
    public List<Vector2> getAdjacentTiles(Vector2 center)
    {
        List<Vector2> result = new List<Vector2>();
        if (center.y > -5f && center.y < 4f && center.x > -8.5f && center.x < 8.5f)
        {
            //UP
            int tileNumber = ((int)(center.y + 6f) * width) + (int)(center.x + 9f);
            if (insideArea(tileNumber)) result.Add(tiles[tileNumber].transform.position);

            //DOWN
            tileNumber = ((int)(center.y + 4f) * width) + (int)(center.x + 9f);
            if (insideArea(tileNumber)) result.Add(tiles[tileNumber].transform.position);

            //LEFT
            tileNumber = ((int)(center.y + 5f) * width) + (int)(center.x + 8f);
            if (insideArea(tileNumber)) result.Add(tiles[tileNumber].transform.position);

            //RIGHT
            tileNumber = ((int)(center.y + 5f) * width) + (int)(center.x + 10f);
            if (insideArea(tileNumber)) result.Add(tiles[tileNumber].transform.position);

            //MAIN DIAGONAL UPPER
            tileNumber = ((int)(center.y + 6f) * width) + (int)(center.x + 8f);
            if (insideArea(tileNumber)) result.Add(tiles[tileNumber].transform.position);

            //MAIN DIAGONAL BOTTOM
            tileNumber = ((int)(center.y + 4f) * width) + (int)(center.x + 10f);
            if (insideArea(tileNumber)) result.Add(tiles[tileNumber].transform.position);

            //SECONDARY DIAGONAL UPPER
            tileNumber = ((int)(center.y + 6f) * width) + (int)(center.x + 10f);
            if (insideArea(tileNumber)) result.Add(tiles[tileNumber].transform.position);

            //SECONDARY DIAGONAL BOTTOM
            tileNumber = ((int)(center.y + 4f) * width) + (int)(center.x + 8f);
            if (insideArea(tileNumber)) result.Add(tiles[tileNumber].transform.position);
        }

        //Finally...
        return result;
    }

    //Get Connected Puddles
    public Vector2[] getConnectedPuddles(Vector2 position)
    {
        LinkedList<Vector2> intermediaryList = new LinkedList<Vector2>();
        intermediaryList.AddFirst(position);
        LinkedListNode<Vector2> currentNode = null;

        if (position.y > -5f && position.y < 4f && position.x > -8.5f && position.x < 8.5f)
        {
            do
            {
                //Get Position
                if (currentNode != null) currentNode = currentNode.Next;
                else currentNode = intermediaryList.First;
                position = currentNode.Value;

                //UP
                int tileNumber = ((int)(position.y + 6f) * width) + (int)(position.x + 9f);
                if (insideArea(tileNumber) && (tiles[tileNumber].GetComponent<Environment>().currentEnvironmentCondition == EnvironmentCondition.Puddle || tiles[tileNumber].GetComponent<Environment>().currentEnvironmentCondition == EnvironmentCondition.PuddleAndShock))
                {
                    if(!intermediaryList.Contains(tiles[tileNumber].transform.position)) intermediaryList.AddLast(tiles[tileNumber].transform.position);
                }

                //DOWN
                tileNumber = ((int)(position.y + 4f) * width) + (int)(position.x + 9f);
                if (insideArea(tileNumber) && (tiles[tileNumber].GetComponent<Environment>().currentEnvironmentCondition == EnvironmentCondition.Puddle || tiles[tileNumber].GetComponent<Environment>().currentEnvironmentCondition == EnvironmentCondition.PuddleAndShock))
                {
                    if (!intermediaryList.Contains(tiles[tileNumber].transform.position)) intermediaryList.AddLast(tiles[tileNumber].transform.position);
                }

                //LEFT
                tileNumber = ((int)(position.y + 5f) * width) + (int)(position.x + 8f);
                if (insideArea(tileNumber) && (tiles[tileNumber].GetComponent<Environment>().currentEnvironmentCondition == EnvironmentCondition.Puddle || tiles[tileNumber].GetComponent<Environment>().currentEnvironmentCondition == EnvironmentCondition.PuddleAndShock))
                {
                    if (!intermediaryList.Contains(tiles[tileNumber].transform.position)) intermediaryList.AddLast(tiles[tileNumber].transform.position);
                }

                //RIGHT
                tileNumber = ((int)(position.y + 5f) * width) + (int)(position.x + 10f);
                if (insideArea(tileNumber) && (tiles[tileNumber].GetComponent<Environment>().currentEnvironmentCondition == EnvironmentCondition.Puddle || tiles[tileNumber].GetComponent<Environment>().currentEnvironmentCondition == EnvironmentCondition.PuddleAndShock))
                {
                    if (!intermediaryList.Contains(tiles[tileNumber].transform.position)) intermediaryList.AddLast(tiles[tileNumber].transform.position);
                }
            }
            while (currentNode != intermediaryList.Last);
        }

        //Build Array
        Vector2[] result = new Vector2[intermediaryList.Count];
        int index = 0;
        foreach (Vector2 pos in intermediaryList)
        {
            result[index] = pos;
            index++;
        }

        //Finally
        return result;
    }
}
