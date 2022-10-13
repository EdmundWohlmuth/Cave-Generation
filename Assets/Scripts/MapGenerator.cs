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

    private void Start()
    {
        GenerateMap();
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

    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
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

    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {

            }
        }
    }


    void GetSurroundingWallCount(int x, int y)
    {
        int wallCount = 0;
        for (int neighborX = -1; neighborX <= x + 1; neighborX++)
        {
            for (int neighborY = -1; neighborY <= y + 1; neighborY++)
            {

            }
        }
    }
    

    private void OnDrawGizmos()
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
