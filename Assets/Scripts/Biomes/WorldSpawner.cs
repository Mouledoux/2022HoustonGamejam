using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Mouledoux.Node;

public class WorldSpawner : MonoBehaviour
{
    public GameObject cellNodePrefab;


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
    public GameObject[,] gridNodes;





    // ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
    private void Start()
    {

        //mapTexture = Perlin.GeneratePerlinTexture(perlinSeed, (int)cols * 10, (int)rows * 10, (int)xOffset, (int)zOffset, biomeScale, elevationScale, temperatureScale);
        StartCoroutine(GenerateNewHexGrid(cols, rows));
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
    }



    // ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
    bool ClearBoard()
    {
        if(gridNodes == null)
            return false;

        foreach (GameObject node in gridNodes)
        {
            GameObject nodeObject = node;
            Destroy(nodeObject);
        }

        gridNodes = null;
        return true;
    }



    // ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
    public IEnumerator GenerateNewHexGrid(uint a_xSize, uint a_ySize)
    {
        GameObject gridCell;
        
        int txWidth = (int)a_xSize;
        int txHeight = (int)a_ySize;

        int pixX, pixY;

        int hexOffset;
        Vector3 pos;
        Vector3 scale;

        bool isWall;
        System.Func<int, uint, bool> IsEdge = delegate(int index, uint max)
        {
            return index == 0 || index == max - 1;
        };

        cols = a_xSize;
        rows = a_ySize;
        gridNodes = new GameObject[cols, rows];

        for(int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                hexOffset = (i % 2);
                pixX = (int)(((float)i/cols) * txWidth);
                pixY = (int)(((float)j/rows) * txHeight);

                isWall = edgesAreWalls && (IsEdge(i, cols) || IsEdge(j, rows));

                gridCell = Instantiate(cellNodePrefab);

                float px = (i) * xOffset;
                float py = 0;
                float pz = (j) + (hexOffset * 0.5f) * zOffset;

                gridCell.transform.parent = transform;

                pos = new Vector3(px, py, pz);
                scale = isWall ? new Vector3(1f, (maxHeight + 1f), 1f) : Vector3.one;

                gridCell.transform.localPosition = pos;
                gridCell.transform.Rotate(Vector3.up, 30);
                gridCell.transform.localScale = scale;               

                gridNodes[i, j] = gridCell;
            }

            yield return null;
        }
    }
}