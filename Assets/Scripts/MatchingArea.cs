using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MatchingArea : MonoBehaviour
{
    public Slot[] slots;
    public event Action TilesOverlapEvent;
    [HideInInspector] public TileObject lastTile;
    [HideInInspector] public Vector3 lastTilePosition;

    public static MatchingArea Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void JoinEmptySlot(TileObject selectedTile)
    {
        if (slots[slots.Length - 1].Tile != null)
        {
            Debug.Log("Slots are full!");
            return;
        }
        lastTile = selectedTile;
        lastTilePosition = selectedTile.transform.position;
        selectedTile.BoxCollider2D.enabled = false;
        // TilesOverlapEvent -= selectedTile.CheckOverlap;
        TilesOverlapEvent?.Invoke();
        // Eşleşen taş varsa
        for (int i = slots.Length - 1; i >= 0; i--)
        {
            if (slots[i].Tile != null && slots[i].Tile.TileSpriteID == selectedTile.TileSpriteID)
            {
                for (int j = slots.Length - 1; j > i; j--)
                {
                    if (slots[j].Tile != null)
                    {
                        slots[j + 1].Tile = slots[j].Tile;
                        slots[j].Tile = null;
                        slots[j + 1].Tile.UpdateLocation();
                    }
                }
                slots[i + 1].Tile = selectedTile;
                selectedTile.SlotdID = i + 1;
                selectedTile.OnTouch();
                return;
            }
        }
        
        // Eşleşen taş yoksa
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Tile == null)
            {
                slots[i].Tile = selectedTile;
                selectedTile.SlotdID = i;
                selectedTile.OnTouch();
                return;
            }
        }
    }
    public void CheckMatch(int spriteID)
    {
        // Eşleşme kontrolü
        int matchCount = 0;
        List<Slot> matchedSlots = new List<Slot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Tile != null)
            {
                if (slots[i].Tile.TileSpriteID == spriteID)
                {
                    matchCount++;
                    matchedSlots.Add(slots[i]);
                }
            }
        }

        // 3'lü eşleşme varsa
        if (matchCount >= 3)
        {
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("Matched");
                TileObject tempTile = matchedSlots[i].Tile;
                matchedSlots[i].Tile = null;
                tempTile.transform.DOScale(0, 0.2f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    Destroy(tempTile.gameObject);
                });
            }
            lastTile = null;
            MoveToSlots();
        }
    }
    public void MoveToSlots()
    {
        //Slotlardaki Tile'ları kaydır.
        List<TileObject> allTiles = new List<TileObject>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Tile != null)
            {
                allTiles.Add(slots[i].Tile);
                slots[i].Tile = null;
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < allTiles.Count)
            {
                slots[i].Tile = allTiles[i];
                allTiles[i].UpdateLocation();
            }
            else
            {
                slots[i].Tile = null;
            }
        }
    }
}
