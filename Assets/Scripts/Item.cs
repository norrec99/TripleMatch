using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Match-3/Item")]
public class Item : ScriptableObject
{
    public int ID;
    public int value;
    public Sprite sprite;
}
