using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TileSelector : MonoBehaviour
{
    public event Action<Tile> OnTileSelect;
    public event Action<ClickFailReason> OnClickFail;

    [Header("Tile Select")]
    [SerializeField] private bool isTileHoverSelect = false;
    [SerializeField] private float minSecsToBeDrag = 0.25f;
    [SerializeField] private LayerMask tileLayer;
    [SerializeField] private Camera cam;
    [Space(15)]

    private GameObject curSelectedTile = null;

    private float secsSpentHolding = 0f;

    private void Update()
    {
        bool _isMouseHeld = Input.GetMouseButton(0);
        bool _isMouseUp = Input.GetMouseButtonUp(0);
        if (!isTileHoverSelect && !(_isMouseHeld || _isMouseUp))
            return;

        Collider2D _col = Physics2D.OverlapPoint(cam.ScreenToWorldPoint(Input.mousePosition), tileLayer);

        // if UI clicked
        if (EventSystem.current.IsPointerOverGameObject())
        {
            OnClickFail?.Invoke(ClickFailReason.OVER_UI);
            return;
        }

        if (!_col || !_col.TryGetComponent(out Tile _tile))
        {
            OnClickFail?.Invoke(ClickFailReason.INVALID_GAMEOBJECT);
            return;
        }

        // this is so that if the user drags instead of clicks
        // the tile UI will not show
        if (!isTileHoverSelect)
        {
            if (_isMouseHeld)
            {
                secsSpentHolding += Time.deltaTime;
                return;
            }

            // if mouse isn't held
            // then mouse must be up
            else
            {
                if (secsSpentHolding > minSecsToBeDrag)
                {
                    secsSpentHolding = 0f;
                    OnClickFail?.Invoke(ClickFailReason.DRAG_FOR_TOO_LONG);
                    return;
                }

                secsSpentHolding = 0f;
            }
        }

        if (curSelectedTile == _col.gameObject)
        {
            OnClickFail?.Invoke(ClickFailReason.SAME_TILE);
            return;
        }

        curSelectedTile = _col.gameObject;
        OnTileSelect?.Invoke(_tile);
    }

    public enum ClickFailReason
    {
        OVER_UI, INVALID_GAMEOBJECT, DRAG_FOR_TOO_LONG, SAME_TILE
    }
}