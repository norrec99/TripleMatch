using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDataBase
{
    public static Item[] Items { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        Items = Resources.LoadAll<Item>("Items/");
    }

}
