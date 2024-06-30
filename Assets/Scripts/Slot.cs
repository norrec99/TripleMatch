using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private TileObject tile;
    public TileObject Tile
    {
        get { return tile; }
        set
        {
            if (tile != value)
            {
                tile = value;
                SetTileSlotID();
            }
        }
    }
    [SerializeField] private int slotID;

    public void SetTileSlotID()
    {
        if (tile != null)
        {
            tile.SlotdID = slotID;
        }
    }
}
