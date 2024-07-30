using UnityEngine;
using System.Collections.Generic;

public class HouseOccupantsManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private HousesManager housesManager;
    [Space(15)]

    [SerializeField] private Resource peopleResource;

    private int totalNumPeople = 0;
    private Dictionary<Tile, int> numOccupantsInTiles = new();

    public int GetTotalNumPeople() => totalNumPeople;

    public int GetNumOccupantsInTile(Tile _tile)
    {
        TileData _data = _tile.GetData();

        if (!housesManager.TryGetHouseOnTile(_tile, out House _))
            Debug.LogError($"Tile {_data.Name} does not have a house.");

        if (numOccupantsInTiles.TryGetValue(_tile, out int _numPeople))
            return _numPeople;

        else return 0;
    }

    public AddResult TryAddOccupant(Tile _tile, out int _newNumOccupants)
    {
        TileData _data = _tile.GetData();
        
        if (!housesManager.TryGetHouseOnTile(_tile, out House _house))
            Debug.LogError($"Tile {_data.Name} does not have a house.");

        if (!numOccupantsInTiles.TryGetValue(_tile, out int _prevNumOccupants))
            _prevNumOccupants = 0;

        if (_prevNumOccupants == _house.MaxPersonCapacity)
        {
            _newNumOccupants = _prevNumOccupants;
            return AddResult.NO_MORE_SPACE;
        }

        _newNumOccupants = _prevNumOccupants + 1;
        numOccupantsInTiles[_tile] = _newNumOccupants;
        totalNumPeople++;

        playerInventory.ResourceInventory.Add(peopleResource, 1);

        return AddResult.SUCCESS;
    }

    public AddResult TryFillHouse(Tile _tile, out int _newNumOccupants)
    {
        TileData _data = _tile.GetData();

        if (!housesManager.TryGetHouseOnTile(_tile, out House _house))
            Debug.LogError($"Tile {_data.Name} does not have a house.");

        if (!numOccupantsInTiles.TryGetValue(_tile, out int _prevNumOccupants))
            _prevNumOccupants = 0;

        if (_prevNumOccupants == _house.MaxPersonCapacity)
        {
            _newNumOccupants = _prevNumOccupants;
            return AddResult.NO_MORE_SPACE;
        }

        int _deltaPeople = _house.MaxPersonCapacity - _prevNumOccupants;
        _newNumOccupants = _prevNumOccupants + _deltaPeople;
        numOccupantsInTiles[_tile] = _newNumOccupants;
        totalNumPeople += _deltaPeople;

        playerInventory.ResourceInventory.Add(peopleResource, _deltaPeople);

        return AddResult.SUCCESS;
    }

    public int RemoveAllOccupants(Tile _tile)
    {
        TileData _data = _tile.GetData();
        
        if (!housesManager.TryGetHouseOnTile(_tile, out House _))
            Debug.LogError($"Tile {_data.Name} does not have a house.");

        if (!numOccupantsInTiles.TryGetValue(_tile, out int _prevNumOccupants))
            _prevNumOccupants = 0;

        if (_prevNumOccupants == 0)
            return 0;

        numOccupantsInTiles[_tile] = _prevNumOccupants;
        totalNumPeople -= _prevNumOccupants;

        playerInventory.ResourceInventory.Remove(peopleResource, _prevNumOccupants);

        return _prevNumOccupants;
    }

    public enum AddResult { SUCCESS, NO_MORE_SPACE };
}