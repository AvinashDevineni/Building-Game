using UnityEngine;
using System.Collections.Generic;

public class WorkBuildingsManager : MonoBehaviour
{
    [Header("Dependenices")]
    [SerializeField] private WorkersManager workersManager;

    private Dictionary<Tile, WorkBuilding> workBuildings = new();

    public bool TryGetWorkBuildingOnTile(Tile _tile, out WorkBuilding _building)
    {
        return workBuildings.TryGetValue(_tile, out _building);
    }

    public bool IsTileWorkBuilding(Tile _tile) => workBuildings.ContainsKey(_tile);

    public Inventory CalculateTotalWorkBuildingProduction()
    {
        Inventory _totalBuildingProduction = new();
        foreach (KeyValuePair<Tile, WorkBuilding> _tileBuilding in workBuildings)
        {
            int _numWorkers = workersManager.GetNumWorkersInTile(_tileBuilding.Key);

            foreach (ResourceCount _resourceCount in _tileBuilding.Value.ResourcesGainedPerWorker)
                _totalBuildingProduction.Add(_resourceCount.Resource, _resourceCount.Count * _numWorkers);
        }

        return _totalBuildingProduction;
    }

    public void UpdateTileWorkBuilding(Tile _tile, WorkBuilding _building) => workBuildings[_tile] = _building;

    public void RemoveTileWorkBuilding(Tile _tile) => workBuildings.Remove(_tile);
}