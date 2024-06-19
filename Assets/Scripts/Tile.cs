using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void Init(bool isOffset)
    {
        spriteRenderer.color = isOffset ? offsetColor : baseColor;
    }
}
