using UnityEngine;
using System.Collections.Generic;

public class WorkersManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private WorkBuildingsManager workBuildingsManager;
    [Space(15)]

    [SerializeField] private Resource peopleResource;

    private int totalNumWorkers = 0;
    private Dictionary<Tile, int> numWorkersInTiles = new();

    public int GetTotalNumWorkers() => totalNumWorkers;

    public int GetNumOfPeopleNotWorking() => playerInventory.ResourceInventory.GetResource(peopleResource) - totalNumWorkers;

    public int GetNumWorkersInTile(Tile _tile)
    {
        TileData _data = _tile.GetData();

        if (!workBuildingsManager.TryGetWorkBuildingOnTile(_tile, out WorkBuilding _))
            Debug.LogError($"Tile {_data.Name} does not have a work building.");

        if (numWorkersInTiles.TryGetValue(_tile, out int _numPeople))
            return _numPeople;

        else return 0;
    }

    public AddResult TryAddWorker(Tile _tile, out int _newNumWorkersInTile)
    {
        TileData _data = _tile.GetData();
        
        if (!workBuildingsManager.TryGetWorkBuildingOnTile(_tile, out WorkBuilding _building))
            Debug.LogError($"Tile {_data.Name} does not have a work building.");

        if (!numWorkersInTiles.TryGetValue(_tile, out int _prevNumWorkers))
            _prevNumWorkers = 0;

        int _numPeople = playerInventory.ResourceInventory.GetResource(peopleResource);

        if (_numPeople == 0)
        {
            _newNumWorkersInTile = _prevNumWorkers;
            return AddResult.NO_MORE_PEOPLE;
        }

        if (_prevNumWorkers + 1 > _building.MaxNumOfWorkers)
        {
            _newNumWorkersInTile = _prevNumWorkers;
            return AddResult.NO_MORE_SPACE;
        }

        _newNumWorkersInTile = _prevNumWorkers + 1;
        numWorkersInTiles[_tile] = _newNumWorkersInTile;
        totalNumWorkers++;

        playerInventory.ResourceInventory.Remove(peopleResource, 1);

        return AddResult.SUCCESS;
    }

    public int RemoveWorker(Tile _tile)
    {
        TileData _data = _tile.GetData();

        if (!workBuildingsManager.TryGetWorkBuildingOnTile(_tile, out WorkBuilding _))
            Debug.LogError($"Tile {_data.Name} does not have a work building.");

        if (!numWorkersInTiles.TryGetValue(_tile, out int _prevNumWorkers))
            return 0;

        int _newNumWorkersInTile = _prevNumWorkers - 1;
        numWorkersInTiles[_tile] = _newNumWorkersInTile;
        totalNumWorkers--;

        playerInventory.ResourceInventory.Add(peopleResource, 1);

        return _newNumWorkersInTile;
    }

    public enum AddResult { SUCCESS, NO_MORE_PEOPLE, NO_MORE_SPACE };
}