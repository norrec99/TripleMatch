using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class TileObject : MonoBehaviour
{
    [SerializeField] private int tileSpriteID;
    public int TileSpriteID
    {
        get { return tileSpriteID; }
        set { tileSpriteID = value; }
    }
    [SerializeField] private SpriteRenderer bgImage;
    [SerializeField] private SpriteRenderer tileIcon;
    [SerializeField] private Sprite[] sprites;
    public Sprite[] Sprites
    {
        get { return sprites; }
    }
    [SerializeField] private int zIndex;
    [SerializeField] private int slotID;
    public int SlotdID
    {
        get { return slotID; }
        set { slotID = value; }
    }
    [SerializeField] private BoxCollider2D boxCollider2D;
    public BoxCollider2D BoxCollider2D
    {
        get { return boxCollider2D; }
    }

    private bool tileStatus;

    private void Start()
    {
        ChangeSprite();
        CheckOverlap();
        CheckTileStatus();
        MatchingArea.Instance.TilesOverlapEvent += CheckOverlap;
    }
    private void OnDestroy()
    {
        MatchingArea.Instance.TilesOverlapEvent -= CheckOverlap;
    }
    private void ChangeSprite()
    {
        tileIcon.sprite = sprites[tileSpriteID];
        tileIcon.sortingOrder = zIndex * 2;
        bgImage.sortingOrder = zIndex * 2 - 1;
    }
    private void CheckTileStatus()
    {
        if (tileStatus)
        {
            bgImage.DOColor(Color.white, 0f);
            tileIcon.DOColor(Color.white, 0f);
            gameObject.layer = LayerMask.NameToLayer("OpenTile");
        }
        else
        {
            bgImage.DOColor(Color.grey, 0f);
            tileIcon.DOColor(Color.grey, 0f);
            gameObject.layer = LayerMask.NameToLayer("CloseTile");
        }
    }

    public void UpdateLocation()
    {
        transform.DOMove(MatchingArea.Instance.slots[slotID].transform.position, .2f);
    }
    public void OnTouch()
    {
        Debug.Log("touched");
        Level.Instance.RemoveTile(this);
        boxCollider2D.enabled = false;
        transform.DOKill();
        transform.DOMove(MatchingArea.Instance.slots[slotID].transform.position, 0.2f).OnComplete((() =>
        {
            Debug.Log("SpriteID: " + tileSpriteID);
            MatchingArea.Instance.CheckMatch(TileSpriteID);
        }));
    }
    public void CheckOverlap()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxCollider2D.size, 0f);

        if (colliders.Length > 1)
        {
            bool hasOverlap = colliders.Any(collider => collider.gameObject != gameObject);
            tileStatus = hasOverlap;

            if (hasOverlap)
            {
                int maxZ = -1;
                foreach (var collider in colliders)
                {
                    if (collider.gameObject != gameObject)
                    {
                        var tile = collider.GetComponent<TileObject>();
                        if (tile != null && tile.zIndex > maxZ)
                        {
                            maxZ = tile.zIndex;
                        }
                    }
                }

                if (maxZ > zIndex)
                {
                    tileStatus = false;
                }
                else
                {
                    tileStatus = true;
                }
            }
        }
        else
        {
            tileStatus = true;
        }
        CheckTileStatus();

    }
}
