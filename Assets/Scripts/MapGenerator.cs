using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;

    void Start()
    {
        GenerateMap();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateMap();
        }
    }

    void GenerateMap()
   {
        map = new int[width, height];
        RandomFillMap();
        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }
   }

    void RandomFillMap() // works through the double nested loop and picks a random state (wall / open)
    {
        if (useRandomSeed)
        {
            seed = System.DateTime.Now.ToString(); // Time.time wouldn't work for me so I am using the computers time instead
        }

        System.Random PseudoRandom = new System.Random(seed.GetHashCode()); // sets seed to int

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (PseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0; // if statement
                }              
            }
        }
    }

    void SmoothMap() // if there are four walls next to the current tile make the current tile a wall, otherwise make it open
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighborWallTiles = GetSurroundingWallCount(x, y);

                if (neighborWallTiles > 4)
                {
                    map[x, y] = 1;
                }
                else if (neighborWallTiles < 4)
                {
                    map[x, y] = 0;
                }
            }
        }
    }


    int GetSurroundingWallCount(int gridX, int gridY) // Counts the surrounding walls (they are listed 0 or 1) and adds the number for SmoothMap()
    {
        int wallCount = 0;
        for (int neighborX = gridX - 1; neighborX <= gridX + 1; neighborX++)
        {
            for (int neighborY = gridY - 1; neighborY <= gridY + 1; neighborY++)
            {
                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                {
                    if (neighborX != gridX || neighborY != gridY)
                    {
                        wallCount += map[neighborX, neighborY];
                    }
                }
                else wallCount++;
            }
        }
        return wallCount;
    }
    

    private void OnDrawGizmos() // TEMP map draw
    {
        if (map != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.color = (map[x,y] == 0) ? Color.white : Color.black;
                    Vector3 pos = new Vector3(-width / 2 + x + 0.5f, 0, -height / 2 + y + 0.5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }
}
