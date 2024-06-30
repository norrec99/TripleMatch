using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<TileObject> tiles = new List<TileObject>();

    public static Level Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddTile(TileObject tile)
    {
        if (!tiles.Contains(tile))
        {
            tiles.Add(tile);
        }
    }
    public void RemoveTile(TileObject tile)
    {
        if (tiles.Contains(tile))
        {
            tiles.Remove(tile);
        }

        if (tiles.Count == 0)
        {
            Debug.Log("Level complete!");
        }
    }
}
