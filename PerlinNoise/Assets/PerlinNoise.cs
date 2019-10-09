﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [RangeAttribute(1f, 10f)]
    public float flatness = 1f;
    [RangeAttribute(1f, 20f)]
    public float frequency = 1f;
    [RangeAttribute(1f, 10f)]
    public int octaves = 8;
    Terrain terrain;
    public float offsetX=100f;
    public float offsetY = 100f;
    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }

    // Update is called once per frame
    void Update()
    {
        
        float[,] heightmap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight);

        for (int i = 0; i < terrain.terrainData.heightmapHeight; ++i)
        {
            for (int j = 0; j < terrain.terrainData.heightmapWidth; ++j)
            {

                float x = j / (float)terrain.terrainData.heightmapWidth+offsetX;
                float y = i / (float)terrain.terrainData.heightmapHeight+offsetY;


                float current_frequency = frequency;
                float height = 0f;
                float amplitude = 1;
                for (int z = 0; z < octaves; ++z)
                {
                    height = height + Mathf.PerlinNoise(x * frequency, y * frequency) * amplitude;
                    amplitude /= 2;
                    current_frequency *= 2;
                }

                heightmap[i, j] = height / flatness;
            }
        }
        terrain.terrainData.SetHeights(0, 0, heightmap);
        offsetX += Time.deltaTime * 1f;
        offsetY += Time.deltaTime * 1f;
    }
}
