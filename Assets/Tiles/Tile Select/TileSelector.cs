using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class TileSelector : MonoBehaviour
{
    public event Action<Tile> OnTileSelect;
    public event Action<Tile, Building> OnBuildingCreate;

    [Header("Tile Select")]
    [SerializeField] private bool isTileHoverSelect;
    [SerializeField] private LayerMask tileLayer;
    [SerializeField] private Camera cam;
    [Space(15)]

    [Header("Tile UI")]
    [SerializeField] private TileUI tileUiPrefab;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Vector2 tileUiOffset;

    private GameObject curSelectedTile = null;

    private GameObject tileUiObject = null;
    private TileUI tileUI = null;
    private RectTransform canvasTrans;

    private void Awake()
    {
        canvasTrans = canvas.GetComponent<RectTransform>();

        TileUI _ui = Instantiate(tileUiPrefab, canvasTrans);
        tileUI = _ui;
        tileUiObject = _ui.gameObject;

        _ui.OnBuildingCreate += (_tile, _building) => OnBuildingCreate?.Invoke(_tile, _building);

        tileUiObject.SetActive(false);
    }

    private void Update()
    {
        if (isTileHoverSelect || Input.GetMouseButtonDown(0))
        {
            Collider2D _col = Physics2D.OverlapPoint(cam.ScreenToWorldPoint(Input.mousePosition), tileLayer);

            // if UI clicked
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // if TileUI not clicked disable tile ui
                if (!IsPointerOverTileUI())
                    tileUiObject.SetActive(false);

                return;
            }

            if (!_col || !_col.TryGetComponent(out Tile _tile))
            {
                tileUiObject.SetActive(false);
                return;
            }

            if (curSelectedTile == _col.gameObject)
            {
                if (!tileUiObject.activeInHierarchy)
                    IntiailizeTileUI(_tile);
                else tileUiObject.SetActive(false);

                return;
            }

            curSelectedTile = _col.gameObject;
            IntiailizeTileUI(_tile);

            OnTileSelect?.Invoke(_tile);
        }
    }

    public void SetTileUiActive(bool _isActive) => tileUiObject.SetActive(_isActive);

    private bool IsPointerOverTileUI()
    {
        PointerEventData eventData = new(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> _uiRaycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, _uiRaycastResults);

        for (int index = 0; index < _uiRaycastResults.Count; index++)
        {
            if (_uiRaycastResults[index].gameObject == tileUiObject)
                return true;
        }
        return false;
    }

    private void IntiailizeTileUI(Tile _tile)
    {
        SetBottomLeftToMouse();
        tileUI.transform.position += (Vector3)tileUiOffset;
        tileUI.SetTile(_tile);

        tileUiObject.SetActive(true);
    }

    private void SetBottomLeftToMouse()
    {
        Vector2 _mousePos = GetMouseUiPosition();
        tileUI.transform.position = _mousePos + ((Vector2)tileUI.transform.position - tileUI.BottomLeft);
    }

    private Vector2 GetMouseUiPosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle
        (
            canvasTrans, Input.mousePosition,
            canvas.worldCamera, out Vector2 _localPos
        );

        Vector2 _globalPos = canvas.transform.TransformPoint(_localPos);

        return _globalPos;
    }
}