﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TerrainGenerator : MonoBehaviour
{
    [RangeAttribute(1f,10f)]
    public float flatness = 1f;

    Texture2D heightimage;
    Terrain terrain;
    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        heightimage = new Texture2D(terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);
        heightimage.LoadImage(File.ReadAllBytes("Assets/2.jpg"));
    }

    // Update is called once per frame
    void Update()
    {

        float[,] heightmap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);

        for (int i = 0; i < terrain.terrainData.heightmapHeight; ++i)
        {
            for (int j = 0; j < terrain.terrainData.heightmapWidth; ++j)
            {

                float x = j / (float)terrain.terrainData.heightmapWidth;
                float y = i / (float)terrain.terrainData.heightmapHeight;
                float height = heightimage.GetPixel(i,j).r;
                /*
                float current_frequency = frequency;
                float height = 0f;
                float amplitude = 1;
                for (int z = 0; z < octaves; ++z)
                {
                    height=height+ Mathf.PerlinNoise(i * frequency, j * frequency)*amplitude;
                    amplitude /= 2;
                    current_frequency *= 2;
                }
                */

                heightmap[i, j] = height / flatness;
            }
        }
        terrain.terrainData.SetHeights(0, 0, heightmap);
    }
}
