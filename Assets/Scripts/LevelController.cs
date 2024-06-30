using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Level[] allLevels;

    private void Start()
    {
        CreateLevel();

    }

    public void CreateLevel()
    {
        if (allLevels.Length == 0) return;

        int levelID = allLevels.Length >= 1 ? SaveLoadManager.GetLevel() % allLevels.Length : 0;

        Instantiate(allLevels[levelID]);
    }
}
