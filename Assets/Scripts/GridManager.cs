using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int rows = 6;
    [SerializeField] private int cols = 6;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform mainCam;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(row, col), Quaternion.identity);
                spawnedTile.name = $"TileHolder {row} {col}";

                bool isOffset = (row % 2 == 0 && col % 2 != 0) || (row % 2 != 0 && col % 2 == 0);
                spawnedTile.Init(isOffset); 
            }
        }

        mainCam.position = new Vector3((float)rows/2 - 0.5f, (float)cols/2 - 0.5f, -10);
    }

}
