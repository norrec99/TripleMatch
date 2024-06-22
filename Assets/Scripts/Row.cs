using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    public Tile[] tiles; 

    public void DisableAllTileButtons()
    {
        foreach (Tile tile in tiles)
        {
            tile.DisableButton();
        }
    }
}
