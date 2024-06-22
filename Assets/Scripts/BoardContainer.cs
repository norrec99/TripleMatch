using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BoardContainer : MonoBehaviour
{
    [SerializeField] private RectTransform tileholderArea;

    public static BoardContainer Instance;

    private int totalTileCount = 0;

    private List<Tile> tiles = new List<Tile>();

    private Sequence moveSequence;

    private void Awake()
    {
        Instance = this;
    }

    public void MoveObjectToTileHolder(GameObject animObject)
    {
        RectTransform rect = animObject.GetComponent<RectTransform>();
        animObject.transform.SetParent(tileholderArea.transform);

        moveSequence?.Kill(true);
        moveSequence = DOTween.Sequence();

        moveSequence.Append(rect.DOAnchorPos(new Vector2((rect.rect.width / 2) + (totalTileCount * rect.rect.width), -rect.rect.height / 2), 0.5f));

        moveSequence.OnPlay(() =>
        {
            totalTileCount++;
            Tile tileObject = animObject.GetComponent<Tile>();
            tileObject.GetComponent<Button>().enabled = false;
            tiles.Add(tileObject);
            Debug.Log("totalTileCount " + totalTileCount);
        });

        moveSequence.OnComplete(() =>
        {
            CheckForMatchingTiles();
        });
    }

    private void CheckForMatchingTiles()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (i + 2 < tiles.Count)
            {
                if (tiles[i + 1].Item.ID == tiles[i + 2].Item.ID)
                {
                    if (tiles[i].Item.ID == tiles[i + 1].Item.ID)
                    {
                        totalTileCount -= 3;
                        Debug.Log("Matched");
                        var tempTile1 = tiles[i];
                        var tempTile2 = tiles[i + 1];
                        var tempTile3 = tiles[i + 2];
                        tiles.Remove(tempTile1);
                        tiles.Remove(tempTile2);
                        tiles.Remove(tempTile3);
                        Destroy(tempTile1.gameObject);
                        Destroy(tempTile2.gameObject);
                        Destroy(tempTile3.gameObject);
                    }
                }
            }
        }
    }
}
