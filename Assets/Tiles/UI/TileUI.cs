using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Collections.Generic;

public class TileUI : MonoBehaviour
{
    public event Action<Building> OnBuildingCreate;
    public event Action<BuildingUI> OnDecreaseWorkersClicked;
    public event Action<BuildingUI> OnIncreaseWorkersClicked;

    public Vector2 BottomLeft => bottomLeft.position;
    public Vector2 MiddleLeft => middleLeft.position;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI tileNameText;
    [SerializeField] private BuildingListUI buildingList;
    [Space(15)]

    [SerializeField] private Transform bottomLeft;
    [SerializeField] private Transform middleLeft;

    #nullable enable
    public void SetTile(Tile _tile, Building? _currentBuilding = null, int _numWorkers = -1)
    #nullable disable
    {
        buildingList.DestroyBuildingUIs();

        tileNameText.text = _tile.GetData().Name;
        
        if (_currentBuilding != null)
        {
            if (_numWorkers < 0)
                Debug.LogError("If currentBuilding is set, numWorkers must be a positive number.");

            BuildingUI _ui = buildingList.AddCurrentBuilding(_currentBuilding, _numWorkers);
            _ui.OnDecreaseWorkersClicked += () => OnDecreaseWorkersClicked?.Invoke(_ui);
            _ui.OnIncreaseWorkersClicked += () => OnIncreaseWorkersClicked?.Invoke(_ui);
        }

        foreach (Building _building in _tile.GetData().PossibleBuildings.Select(_buildingTile => _buildingTile.Building))
        {
            BuildingUI _ui = buildingList.AddBuilding(_building);
            _ui.OnBuildingCreate += _building =>
            {
                if (_tile == null)
                    return;

                OnBuildingCreate?.Invoke(_building);
            };
        }
    }
}