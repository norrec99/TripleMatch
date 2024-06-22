using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Row[] rows;
    public Tile[,] tiles { get; private set; }
    public int width => tiles.GetLength(0);
    public int height => tiles.GetLength(1);
    public static Board Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        tiles = new Tile[rows.Max(row => row.tiles.Length), rows.Length];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var tile = rows[y].tiles[x];

                tile.x = x;
                tile.y = y;
                
                tile.Item = ItemDataBase.Items[Random.Range(0, ItemDataBase.Items.Length)];
                
                tiles[x, y] = tile;
            }
        }
    }
}
