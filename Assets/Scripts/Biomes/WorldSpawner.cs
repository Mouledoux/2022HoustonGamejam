﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Mouledoux.Node;

public class WorldSpawner : MonoBehaviour
{
    public BiomeObject worldProfile;
    private BiomeObject[] biomes;


    [Header("Grid Dimensions")]
    [SerializeField] private uint _rows;
    [SerializeField] private uint _cols;

    public uint rows
    {
        get { return _rows; }
        set { _rows = value; }
    }
    public uint cols
    {
        get { return _cols; }
        set { _cols = value; }
    }
    public float maxHeight = 1f;
    public bool edgesAreWalls;

    public float xOffset, zOffset;

    [Header("Perlin Generation")]
    public string perlinSeed;
    public float biomeScale = 2f;
    public float elevationScale = 2f;
    public float temperatureScale = 2f;
    
    [Space]
    public float perlinOffsetX;
    public float perlinOffsetY;


    [Header("Custom Map!")]
    public Texture2D mapTexture;
    public Node[,] gridNodes;





    // ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
    private void Start()
    {

    }




    // ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
    private void Update()
    {
        //GeneratePerlinTexture(perlinSeed, perlinScale);

        if(Input.GetKey(KeyCode.LeftControl))
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                //StartCoroutine(GenerateNewHexGrid(cols, rows, mapTexture == null ? GeneratePerlinTexture(perlinSeed, biomeScale, elevationScale, temperatureScale) : mapTexture));
            }
        }

        
        GeneratePerlinTexture(perlinSeed, biomeScale, elevationScale, temperatureScale);
    }



    // ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
    bool ClearBoard()
    {
        if(gridNodes == null)
            return false;

        foreach (Node node in gridNodes)
        {
            GameObject[] nodeObject = node.GetInformation<GameObject>();
            if(nodeObject.Length > 0)
                Destroy(nodeObject[0]);
        }

        gridNodes = null;
        return true;
    }



    // // ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
    // public IEnumerator GenerateNewHexGrid(uint a_xSize, uint a_ySize, Texture2D a_sampleTexture)
    // {
    //     GameObject gridCell;
        
    //     int txWidth = a_sampleTexture.width;
    //     int txHeight = a_sampleTexture.height;

    //     int pixX, pixY;
    //     float hueSample, satSample, valSample;

    //     int hexOffset;
    //     Vector3 pos;
    //     Vector3 scale;

    //     bool isWall;
    //     System.Func<int, uint, bool> IsEdge = delegate(int index, uint max)
    //     {
    //         return index == 0 || index == max - 1;
    //     };



    //     ClearBoard();
    //     cols = a_xSize;
    //     rows = a_ySize;
    //     gridNodes = new Node[cols, rows];

    //     for(int i = 0; i < cols; i++)
    //     {
    //         for (int j = 0; j < rows; j++)
    //         {
    //             hexOffset = (i % 2);
    //             pixX = (int)(((float)i/cols) * txWidth);
    //             pixY = (int)(((float)j/rows) * txHeight);


    //             // biome, elevation, temperature
    //             Color.RGBToHSV(a_sampleTexture.GetPixel(pixX, pixY), out hueSample, out satSample, out valSample);

    //             isWall = edgesAreWalls && (IsEdge(i, cols) || IsEdge(j, rows));

    //             float initBias = float.MaxValue;
    //             BiomeObject biome = worldProfile.GetSubBiome(hueSample, satSample, valSample, ref initBias);

    //             gridCell = isWall ? worldProfile.biomeTile : biome.biomeTile;
    //             gridCell = gridCell == null ? worldProfile.subBiomes[0].biomeTile : gridCell;
    //             gridCell = Instantiate(gridCell);

    //             pos = new Vector3(
    //                 ((-cols / 2) + i) * xOffset,
    //                 0,
    //                 (((-rows / 2) + j) + (hexOffset * 0.5f)) * zOffset);

    //             scale = isWall ? new Vector3(1f, (maxHeight + 1f), 1f) :
    //             Vector3.one + Vector3.up * ((int)(satSample * maxHeight));


    //             gridCell.name = $"{biome.name}[{i}, {j}]";
    //             gridCell.transform.parent = transform;

    //             gridCell.transform.localPosition = pos * 2f;
    //             gridCell.transform.Rotate(Vector3.up, 30);
    //             gridCell.transform.localScale = scale;


    //             gridNodes[i, j] = (gridCell.GetComponent<TraversableNode>());
    //             gridNodes[i, j] = gridNodes[i, j] == null ? gridCell.AddComponent<TraversableNode>() : gridNodes[i, j];
                
    //             gridNodes[i, j].coordinates[0] = i;
    //             gridNodes[i, j].coordinates[1] = j;
    //             gridNodes[i, j].pathingValues[1] = satSample;
    //             gridNodes[i, j].isTraversable = !isWall;
                

    //             {
    //                 // Set the biome material
    //                 Renderer cellRenderer;
    //                 if(gridCell.TryGetComponent<Renderer>(out cellRenderer))
    //                 {
    //                     cellRenderer.material = biome.biomeMaterial;
    //                 }

    //                 // Spawn the biome's decoration
    //                 if(biome.biomeDeco != null)
    //                 {
    //                     Vector3 highpoint = gridCell.transform.position + Vector3.up * gridCell.transform.localScale.y / 8f;

    //                     GameObject deco = Instantiate(biome.biomeDeco);
    //                     deco.transform.position = highpoint;
    //                     deco.transform.parent = gridCell.transform;
                    
    //                 }
    //             }



    //             // Set node neighbors -----
    //             {
    //                 if(j > 0)
    //                 {
    //                     gridNodes[i, j].nodeData.AddNeighbor(gridNodes[i, j - 1].nodeData);
    //                 }

    //                 if(i > 0)
    //                 {
    //                     int nextJ = j + (hexOffset * 2 - 1);


    //                     gridNodes[i, j].nodeData.AddNeighbor(gridNodes[i - 1, j].nodeData);

    //                     if(nextJ >= 0 && nextJ < rows)
    //                     {
    //                         gridNodes[i, j].nodeData.AddNeighbor(gridNodes[i - 1, nextJ].nodeData);
    //                     }
    //                 }
    //             }
    //         }
    //         yield return null;
    //     }
    // }


    // ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
   
   
   
   
    private Texture2D GeneratePerlinTexture(string seed_, float scale1_ = 1f, float scale2_= 1f, float scale3_ = 1f)
    {
        Renderer renderer = GetComponent<Renderer>();
        Texture2D perlinTexture = new Texture2D((int)cols, (int)rows);
        Color[] pixels = new Color[perlinTexture.width * perlinTexture.height];

        float sampleH, sampleS, sampleV;

        int seedLen;


        seed_ = seed_.GetHashCode().ToString();
        seedLen = seed_.Length;

        for(int i = 0; i < perlinTexture.height; i++)
        {
            for(int j = 0; j < perlinTexture.width; j++)
            {
                sampleH = GetPerlinNoiseValue(j + perlinOffsetX, i + perlinOffsetY, seed_.Substring(0,             seedLen / 2), scale1_, 1f);
                sampleS = GetPerlinNoiseValue(j + perlinOffsetX, i + perlinOffsetY, seed_.Substring(seedLen / 4,   seedLen / 2), scale2_, 1f);
                sampleV = GetPerlinNoiseValue(j + perlinOffsetX, i + perlinOffsetY, seed_.Substring(seedLen / 2,   seedLen / 2), scale3_, 1f);

                pixels[(i * perlinTexture.width) + j] = Color.HSVToRGB(sampleH, sampleS, sampleV);
            }
        }


        perlinTexture.SetPixels(pixels);
        perlinTexture.Apply();

        if(renderer != null)
            renderer.material.SetTexture("_BaseMap", perlinTexture);

        return perlinTexture;
    }



    // ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
    float GetPerlinNoiseValue(float xCoord_, float yCoord_, string seed_ = "", float scale1_ = 1f, float valueMod_ = 1f)
    {   
        float seedHash = (float)(seed_.GetHashCode() % (seed_.Length));

        xCoord_ /= cols;
        yCoord_ /= rows;

        xCoord_ *= scale1_;
        yCoord_ *= scale1_;

        xCoord_ += seedHash;
        yCoord_ += seedHash;

        return Mathf.Clamp01(Mathf.PerlinNoise(xCoord_, yCoord_)) * valueMod_;
    }





    /// DEPRECIATED METHODS // DEPRECIATED METHODS // DEPRECIATED METHODS // DEPRECIATED METHODS // DEPRECIATED METHODS //
    #region DEPRECIATED METHODS

    [System.Obsolete("depreciated: use GenerateTextureGrid instead")]
    public void GenerateNewMap()
    {
        return;
        // ClearBoard();

        // m_gridNodes = new TraversableNode[cols, m_rows];

        // for(int i = 0; i < m_cols; i++)
        // {
        //     for (int j = 0; j < m_rows; j++)
        //     {
        //         GameObject gridCell = null;

        //         float xCoord = (float)i/(float)cols;
        //         float yCoord = (float)j/(float)rows;

        //         float perlinHeight = GetPerlinNoiseValue(xCoord, yCoord, m_perlinSeed, m_perlinScale);
        //         float height = (int)(perlinHeight * m_perlinScale) * perlinHeightMod;
                

        //         bool isWall = m_edgesAreWalls && (i == 0 || j == 0 || i == m_cols-1 || j == m_rows-1);
        //         Vector3 scale = isWall ? new Vector3(1f, 16f, 1f) : new Vector3(1f, 1f,  1f);


        //         float biomeScale = 8f;
        //         System.Func<int, int> getBiomeNoise = (int valueMod) =>
        //             (int)GetPerlinNoiseValue(xCoord, yCoord, (m_perlinSeed + biomeSeed), biomeScale, valueMod);
                
        //         int perlinFort = (int)GetPerlinNoiseValue(xCoord, yCoord, m_perlinSeed.Substring(m_perlinSeed.Length - 1), 4, 10);


        //         float resistance = 0f;

        //         if(isWall)
        //         {
        //             gridCell = Instantiate(outerWall[getBiomeNoise(outerWall.Count)]) as GameObject;
        //             resistance = float.MaxValue;
        //         }
        //         else if(perlinFort == 9)
        //         {
        //             gridCell = Instantiate(Special[getBiomeNoise(Special.Count)]) as GameObject;
        //             resistance = 7f;
        //         }
        //         else if(perlinHeight >= 0.8f)
        //         {
        //             gridCell = Instantiate(highLand[getBiomeNoise(highLand.Count)]) as GameObject;
        //             resistance = 24f;
        //         }
        //         else  if(perlinHeight >= 0.4f)
        //         {
        //             gridCell = Instantiate(medLand[getBiomeNoise(medLand.Count)]) as GameObject;
        //             resistance = 8f;
        //         }
        //         else
        //         {
        //             gridCell = Instantiate(lowLand[getBiomeNoise(lowLand.Count)]) as GameObject;
        //             resistance = 16f;
        //         }

        //         //height += perlinFort > 3 ? perlinHeightMod : 0;

        //         if(gridCell != null)
        //         {
        //             int hexOffset = (i % 2);

        //             gridCell.name = $"[{i}, {j}]";
        //             gridCell.transform.parent = transform;
        //             //gridCell.transform.localPosition = new Vector3(((-cols / 2) + i) * 0.85f, height, ((-rows / 2) + j) + (hexOffset * 0.5f));
        //             gridCell.transform.localPosition = new Vector3(((-cols / 2) + i) * 0.85f, 0, ((-rows / 2) + j) + (hexOffset * 0.5f));
        //             gridCell.transform.Rotate(Vector3.up, 30);
        //             gridCell.transform.localScale = Vector3.one + Vector3.up * height * 8;

        //             m_gridNodes[i, j] = (gridCell.GetComponent<TraversableNode>());
        //             m_gridNodes[i, j] = m_gridNodes[i, j] == null ? gridCell.AddComponent<TraversableNode>() : m_gridNodes[i, j];
                    
        //             m_gridNodes[i, j].xCoord = i;
        //             m_gridNodes[i, j].yCoord = j;
        //             m_gridNodes[i, j].travelCost = resistance + height;
        //             m_gridNodes[i, j].isTraversable = !isWall;
                    
        //             if(j > 0)
        //             {
        //                 m_gridNodes[i, j].AddNeighbor(m_gridNodes[i, j - 1]);
        //             }

        //             if(i > 0)
        //             {
        //                 m_gridNodes[i, j].AddNeighbor(m_gridNodes[i - 1, j]);

        //                 int nextJ = j + (hexOffset * 2 - 1);

        //                 if(nextJ >= 0 && nextJ < m_rows)
        //                 {
        //                     m_gridNodes[i, j].AddNeighbor(m_gridNodes[i - 1, nextJ]);
        //                 }
        //             }
        //         }
        //     }
        // }
    }
    
    
    #endregion
}