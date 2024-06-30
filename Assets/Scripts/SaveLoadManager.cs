using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager
{
    const string KEY_LEVEL = "level";

    public static void IncreaseLevel()
    {
        int currentLevel = GetLevel();
        PlayerPrefs.SetInt(KEY_LEVEL, currentLevel + 1);
    }
    public static int GetLevel()
    {
        return PlayerPrefs.GetInt(KEY_LEVEL, 1);
    }
}
