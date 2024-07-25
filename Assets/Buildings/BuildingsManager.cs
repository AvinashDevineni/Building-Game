using UnityEngine;
using System.Collections.Generic;

public class BuildingsManager : MonoBehaviour
{
    private Dictionary<Tile, Building> tileBuildings = new();

    public Inventory<Resource> CalculateTotalBuildingProduction()
    {
        Inventory<Resource> _totalBuildingProduction = new();
        foreach (Building _building in tileBuildings.Values)
        {
            foreach (ResourceCount _resourceCount in _building.ResourcesGained)
                _totalBuildingProduction.Add(_resourceCount.Resource, _resourceCount.Count);
        }

        return _totalBuildingProduction;
    }

    public void UpdateTileBuilding(Tile _tile, Building _building) => tileBuildings[_tile] = _building;
}