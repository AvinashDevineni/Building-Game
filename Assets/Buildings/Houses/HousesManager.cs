using UnityEngine;
using System.Collections.Generic;

public class HousesManager : MonoBehaviour
{
    [Header("Dependenices")]
    [SerializeField] private HouseOccupantsManager occupantsManager;

    private Dictionary<Tile, House> houses = new();

    public bool TryGetHouseOnTile(Tile _tile, out House _house)
    {
        return houses.TryGetValue(_tile, out _house);
    }

    public List<Tile> GetAllTilesWithHouses()
    {
        List<Tile> _tiles = new();
        foreach (Tile _tile in houses.Keys)
            _tiles.Add(_tile);

        return _tiles;
    }

    public bool IsTileHouse(Tile _tile) => houses.ContainsKey(_tile);

    public void UpdateTileHouse(Tile _tile, House _house) => houses[_tile] = _house;

    public void RemoveTileHouse(Tile _tile) => houses.Remove(_tile);

}