using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
    private Item item;
    public Item Item 
    { 
        get => item; 
        
        set
        {
            if (item == value) return;

            item = value;
            icon.sprite = item.sprite;
        } 
    }
    public Image icon;
    public Button button;

    private void Awake()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        BoardContainer.Instance.MoveObjectToTileHolder(gameObject);
    }
}
