using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BoardContainer : MonoBehaviour
{
    [SerializeField] private RectTransform tileholderArea;

    public static BoardContainer Instance;

    private int selectedTotalTileCount = 0;
    private bool canClick = true;

    private List<Tile> selectedTiles = new List<Tile>();
    public List<Tile> unSelectedTiles = new List<Tile>();

    private Sequence moveSequence;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        foreach (var tile in Board.Instance.tiles)
        {
            unSelectedTiles.Add(tile);
        }
    }
    public void MoveObjectToTileHolder(GameObject animObject)
    {
        moveSequence?.Kill(true);
        if (canClick)
        {
            RectTransform rect = animObject.GetComponent<RectTransform>();
            animObject.transform.SetParent(tileholderArea.transform);

            moveSequence = DOTween.Sequence();

            moveSequence.Append(rect.DOAnchorPos(new Vector2(selectedTotalTileCount * rect.rect.width, 0f), 0.5f));

            moveSequence.OnPlay(() =>
            {
                selectedTotalTileCount++;
                if (selectedTotalTileCount >= 7)
                {
                    canClick = false;
                }
                Tile tileObject = animObject.GetComponent<Tile>();
                tileObject.GetComponent<Button>().enabled = false;
                selectedTiles.Add(tileObject);
                unSelectedTiles.Remove(tileObject);
            });

            moveSequence.OnComplete(() =>
            {
                CheckForMatchingTiles();
                if (selectedTotalTileCount >= 7)
                {
                    DisableAllTileButtons();
                }
            });
        }
    }
    private void CheckForMatchingTiles()
    {
        for (int i = 0; i < selectedTiles.Count; i++)
        {
            if (i + 2 < selectedTiles.Count)
            {
                if (selectedTiles[i + 1].Item.ID == selectedTiles[i + 2].Item.ID)
                {
                    if (selectedTiles[i].Item.ID == selectedTiles[i + 1].Item.ID)
                    {
                        selectedTotalTileCount -= 3;
                        canClick = true;
                        Debug.Log("Matched");
                        var tempTile1 = selectedTiles[i];
                        var tempTile2 = selectedTiles[i + 1];
                        var tempTile3 = selectedTiles[i + 2];
                        selectedTiles.Remove(tempTile1);
                        selectedTiles.Remove(tempTile2);
                        selectedTiles.Remove(tempTile3);
                        Destroy(tempTile1.gameObject);
                        Destroy(tempTile2.gameObject);
                        Destroy(tempTile3.gameObject);
                    }
                }
            }
        }
    }
    private void DisableAllTileButtons()
    {
        foreach (var tile in unSelectedTiles)
        {
            tile.DisableButton();
        }
    }
}
