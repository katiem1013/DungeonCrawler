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

    void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();

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
    }
}
