using UnityEngine;
using System.Collections.Generic;

public class BuildingsManager : MonoBehaviour
{
    [Header("Dependenices")]
    [SerializeField] private WorkersManager workersManager;

    private Dictionary<Tile, Building> tileBuildings = new();

    public bool TryGetBuildingOnTile(Tile _tile, out Building _building) => tileBuildings.TryGetValue(_tile, out _building);

    public bool IsTileBuilding(Tile _tile) => tileBuildings.ContainsKey(_tile);

    public Inventory CalculateTotalBuildingProduction()
    {
        Inventory _totalBuildingProduction = new();
        foreach (KeyValuePair<Tile, Building> _tileBuilding in tileBuildings)
        {
            int _numWorkers = workersManager.GetNumWorkersInTile(_tileBuilding.Key);

            foreach (ResourceCount _resourceCount in _tileBuilding.Value.ResourcesGainedPerWorker)
                _totalBuildingProduction.Add(_resourceCount.Resource, _resourceCount.Count * _numWorkers);
        }

        return _totalBuildingProduction;
    }

    public void UpdateTileBuilding(Tile _tile, Building _building) => tileBuildings[_tile] = _building;
}