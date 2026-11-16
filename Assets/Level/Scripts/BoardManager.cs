using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardManager : MonoBehaviour
{

    public class CellData
    {
        public bool Passable;
    }

    private CellData[,] boardData;

    private Tilemap tilemap;

    public int width;
    public int height; 
    public Tile[] groundTiles;
    public Tile[] wallTiles;

    private Grid grid; 
    public PlayerController player;


    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        tilemap = GetComponentInChildren<Tilemap>();
        boardData = new CellData[width, height];
        grid = GetComponentInChildren<Grid>();

        for (int y = 0; y < height; ++y)
        {
            for(int x = 0; x < width; ++x)
            {
                Tile tile;
                boardData[x,y] = new CellData();
 
                if(x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    tile = wallTiles[Random.Range(0, wallTiles.Length)];
                    boardData[x,y].Passable = false;
                }

                else
                {
                    tile = groundTiles[Random.Range(0, groundTiles.Length)];
                    boardData[x,y].Passable = true;
                }

                tilemap.SetTile(new Vector3Int(x,y,0), tile);
            }
        }

        player.Spawn(this, new Vector2Int(1,1));
    }

    public UnityEngine.Vector3 CellToWorld(Vector2Int cellIndex)
    {
        return grid.GetCellCenterWorld((Vector3Int)cellIndex);
    }

    public CellData GetCellData(Vector2Int cellIndex)
    {
        if(cellIndex.x < 0 || cellIndex.x >= width || cellIndex.y < 0 || cellIndex.y >= height)
        {
            return null;
        }

        return boardData[cellIndex.x, cellIndex.y];
    }
}
