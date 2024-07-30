using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class TileSelectUiManager : MonoBehaviour
{
    public event Action<Tile, WorkBuilding> OnWorkBuildingCreate;
    public event Action<Tile, House> OnHouseCreate;
    public event Action<Tile, WorkBuildingUI> OnDecreaseWorkers;
    public event Action<Tile, WorkBuildingUI> OnIncreaseWorkers;

    [Header("Dependencies")]
    [SerializeField] private TileSelector tileSelector;
    [SerializeField] private WorkBuildingsManager workBuildingsManager;
    [SerializeField] private WorkersManager workersManager;
    [SerializeField] private HousesManager housesManager;
    [SerializeField] private HouseOccupantsManager houseOccupantsManager;
    [SerializeField] private Camera cam;
    [SerializeField] private CameraZoom camZoom;
    [Space(15)]

    [Header("Tile UI")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private TileUI tileUiPrefab;

    [Header("Settings")]
    [SerializeField] private bool areHousesFirstInBuildingsList = true; 
    [SerializeField] private Vector2 tileUiOffset;

    private Tile curTile;

    private TileUI tileUi;
    private GameObject tileUiObject;
    private RectTransform tileUiTrans;

    private Vector2 initialPosition;

    private void Awake()
    {
        TileUI _ui = Instantiate(tileUiPrefab, canvas.GetComponent<RectTransform>());
        tileUi = _ui;
        tileUiObject = _ui.gameObject;
        tileUiTrans = _ui.GetComponent<RectTransform>();

        tileUi.OnWorkBuildingCreate += _building => OnWorkBuildingCreate?.Invoke(curTile, _building);
        tileUi.OnHouseCreate += _house => OnHouseCreate?.Invoke(curTile, _house);
        tileUi.OnDecreaseWorkersClicked += _buildingUi => OnDecreaseWorkers?.Invoke(curTile, _buildingUi);
        tileUi.OnIncreaseWorkersClicked += _buildingUi => OnIncreaseWorkers?.Invoke(curTile, _buildingUi);

        tileUiObject.SetActive(false);
    }

    private void Start()
    {
        tileSelector.OnTileSelect += _tile => 
        {
            initialPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            InitializeTileUI(_tile, initialPosition);
        };

        tileSelector.OnClickFail += _reason =>
        {
            switch (_reason)
            {
                case TileSelector.ClickFailReason.OVER_UI:
                    tileUiObject.SetActive(IsPointerOverTileUI());
                    break;

                case TileSelector.ClickFailReason.INVALID_GAMEOBJECT:
                    tileUiObject.SetActive(false);
                    break;

                case TileSelector.ClickFailReason.SAME_TILE:
                    if (!tileUiObject.activeInHierarchy)
                        InitializeTileUI(curTile, cam.ScreenToWorldPoint(Input.mousePosition));
                    else tileUiObject.SetActive(!tileUiObject.activeInHierarchy);

                    break;

                default:
                    break;
            }
        };

        camZoom.OnZoom += _orthoSize =>
        {
            float _zoomAmount = _orthoSize / camZoom.InitialCamSize;
            tileUiTrans.localScale = new Vector2(_zoomAmount, _zoomAmount);
            Canvas.ForceUpdateCanvases();
            SetMiddleLeftToPosition(initialPosition);
        };
    }

    public void UpdateTileUI(Tile _tile) => InitializeTileUI(_tile, initialPosition);

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

    private void InitializeTileUI(Tile _tile, Vector2 _middleLeftPos)
    {
        curTile = _tile;

        SetMiddleLeftToPosition(_middleLeftPos);

        bool _doesWorkBuildingExist = workBuildingsManager.TryGetWorkBuildingOnTile(_tile, out WorkBuilding _workBuilding);
        bool _doesHouseExist = housesManager.TryGetHouseOnTile(_tile, out House _house);

        tileUi.SetTile
        (
            _tile, _workBuilding, _doesWorkBuildingExist ? workersManager.GetNumWorkersInTile(_tile) : -1,
            _house, _doesHouseExist ? houseOccupantsManager.GetNumOccupantsInTile(_tile) : -1,
            areHousesFirstInBuildingsList
        );

        tileUiObject.SetActive(true);
    }

    private void SetMiddleLeftToPosition(Vector2 _position)
    {
        tileUiTrans.position = _position + ((Vector2)tileUiTrans.position - tileUi.MiddleLeft) + tileUiOffset;
    }
}