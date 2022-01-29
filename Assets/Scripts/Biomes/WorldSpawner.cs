using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Mouledoux.Node;

public class WorldSpawner : MonoBehaviour
{
    public Biome worldProfile;
    private Biome[] biomes;


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
    public ITraversable[,] gridNodes;





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
                StartCoroutine(GenerateNewHexGrid(cols, rows, mapTexture));
            }
        }
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



    // ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
    public IEnumerator GenerateNewHexGrid(uint a_xSize, uint a_ySize, Texture2D a_sampleTexture)
    {
        GameObject gridCell;
        
        int txWidth = a_sampleTexture.width;
        int txHeight = a_sampleTexture.height;

        int pixX, pixY;
        float hueSample, satSample, valSample;

        int hexOffset;
        Vector3 pos;
        Vector3 scale;

        bool isWall;
        System.Func<int, uint, bool> IsEdge = delegate(int index, uint max)
        {
            return index == 0 || index == max - 1;
        };



        ClearBoard();
        cols = a_xSize;
        rows = a_ySize;
        gridNodes = new ITraversable[cols, rows];

        for(int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                hexOffset = (i % 2);
                pixX = (int)(((float)i/cols) * txWidth);
                pixY = (int)(((float)j/rows) * txHeight);


                // biome, elevation, temperature
                Color.RGBToHSV(a_sampleTexture.GetPixel(pixX, pixY), out hueSample, out satSample, out valSample);

                isWall = edgesAreWalls && (IsEdge(i, cols) || IsEdge(j, rows));

                float initBias = float.MaxValue;
                Biome biome = worldProfile.GetSubBiome(hueSample, satSample, valSample, ref initBias);

                gridCell = isWall ? worldProfile.biomeTile : biome.biomeTile;
                gridCell = gridCell == null ? worldProfile.subBiomes[0].biomeTile : gridCell;
                gridCell = Instantiate(gridCell);

                pos = new Vector3(
                    ((-cols / 2) + i) * xOffset,
                    0,
                    (((-rows / 2) + j) + (hexOffset * 0.5f)) * zOffset);

                scale = isWall ? new Vector3(1f, (maxHeight + 1f), 1f) :
                Vector3.one + Vector3.up * ((int)(satSample * maxHeight));


                gridCell.name = $"{biome.name}[{i}, {j}]";
                gridCell.transform.parent = transform;

                gridCell.transform.localPosition = pos * 2f;
                gridCell.transform.Rotate(Vector3.up, 30);
                gridCell.transform.localScale = scale;


                gridNodes[i, j] = (gridCell.GetComponent<ITraversable>());
                
                gridNodes[i, j].coordinates[0] = i;
                gridNodes[i, j].coordinates[1] = j;
                gridNodes[i, j].pathingValues[1] = satSample;
                gridNodes[i, j].isTraversable = !isWall;
                

                {
                    // Set the biome material
                    Renderer cellRenderer;
                    if(gridCell.TryGetComponent<Renderer>(out cellRenderer))
                    {
                        cellRenderer.material = biome.biomeMaterial;
                    }

                    // Spawn the biome's decoration
                    if(biome.biomeDeco != null)
                    {
                        Vector3 highpoint = gridCell.transform.position + Vector3.up * gridCell.transform.localScale.y / 8f;

                        GameObject deco = Instantiate(biome.biomeDeco);
                        deco.transform.position = highpoint;
                        deco.transform.parent = gridCell.transform;
                    
                    }
                }
            }
            yield return null;
        }
    }
}