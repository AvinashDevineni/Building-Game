using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class TileUI : MonoBehaviour
{
    public event Action<WorkBuilding> OnWorkBuildingCreate;
    public event Action<House> OnHouseCreate;
    public event Action<WorkBuildingUI> OnDecreaseWorkersClicked;
    public event Action<WorkBuildingUI> OnIncreaseWorkersClicked;

    public Vector2 BottomLeft => bottomLeft.position;
    public Vector2 MiddleLeft => middleLeft.position;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI tileNameText;
    [SerializeField] private BuildingListUI buildingList;
    [Space(15)]

    [SerializeField] private Transform bottomLeft;
    [SerializeField] private Transform middleLeft;

    #nullable enable
    public void SetTile(Tile _tile, WorkBuilding? _currentWorkBuilding = null, int _numWorkers = -1,
                        House? _currentHouse = null, int _numOccupants = -1, bool _areHousesFirst = true)
    #nullable disable
    {
        buildingList.DestroyBuildingUIs();

        tileNameText.text = _tile.GetData().Name;
        
        if (_areHousesFirst)
        {
            AddHousesToList(_tile, _currentHouse, _numOccupants);
            AddWorkBuildingsToList(_tile, _currentWorkBuilding, _numWorkers);
        }

        else
        {
            AddWorkBuildingsToList(_tile, _currentWorkBuilding, _numWorkers);
            AddHousesToList(_tile, _currentHouse, _numOccupants);
        }
    }
    
    #nullable enable
    private void AddHousesToList(Tile _tile, House? _currentHouse, int _numOccupants)
    #nullable disable
    {
        if (_currentHouse != null)
        {
            if (_numOccupants < 0)
                Debug.LogError("If currentBuilding is set, numWorkers must be a positive number.");

            HouseUI _ui = buildingList.AddCurrentHouse(_currentHouse, _numOccupants);
        }

        foreach (House _building in _tile.GetData().PossibleHouses.Select(_buildingTile => _buildingTile.Building))
        {
            HouseUI _ui = buildingList.AddHouse(_building);
            _ui.OnBuildingCreate += _building =>
            {
                if (_tile == null)
                    return;

                OnHouseCreate?.Invoke(_building);
            };
        }
    }

    #nullable enable
    private void AddWorkBuildingsToList(Tile _tile, WorkBuilding _currentWorkBuilding, int _numWorkers)
    {
        if (_currentWorkBuilding != null)
        {
            if (_numWorkers < 0)
                Debug.LogError("If currentBuilding is set, numWorkers must be a positive number.");

            WorkBuildingUI _ui = buildingList.AddCurrentWorkBuilding(_currentWorkBuilding, _numWorkers);
            _ui.OnDecreaseWorkersClicked += () => OnDecreaseWorkersClicked?.Invoke(_ui);
            _ui.OnIncreaseWorkersClicked += () => OnIncreaseWorkersClicked?.Invoke(_ui);
        }

        foreach (WorkBuilding _building in _tile.GetData().PossibleBuildings.Select(_buildingTile => _buildingTile.Building))
        {
            WorkBuildingUI _ui = buildingList.AddWorkBuilding(_building);
            _ui.OnBuildingCreate += _building =>
            {
                if (_tile == null)
                    return;

                OnWorkBuildingCreate?.Invoke(_building);
            };
        }
    }
}