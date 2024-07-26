using UnityEngine;
using System.Collections.Generic;

public class TileGrid : MonoBehaviour
{
    [Header("Tiles")]
    [SerializeField] private Transform tileParent;
    [SerializeField] private List<TileData> possibleTiles;
    [SerializeField] private Tile tilePrefab;
    [Space(15)]

    [SerializeField] private int tileSize = 1;
    [SerializeField] private int gridSize = 3;

    private Dictionary<Vector2Int, Tile> tilesAtLocalPositions = new();

    private void Awake() => BuildGrid(gridSize);

    public void BuildGrid(int _size)
    {
        if (_size % 2 != 1)
            Debug.LogError("Size must be an odd number.");

        int _gridExtent = (_size - 1) / 2; 
        for (int x = -_gridExtent; x <= _gridExtent; x++)
        {
            for (int y = -_gridExtent; y <= _gridExtent; y++)
            {
                Tile _tile = Instantiate(tilePrefab, tileParent);
                _tile.Initialize(possibleTiles[Random.Range(0, possibleTiles.Count)]);
                _tile.transform.position = new Vector2(x * tileSize, y * tileSize);
                _tile.transform.localScale = new Vector2(tileSize, tileSize);

                tilesAtLocalPositions[new Vector2Int(x, y)] = _tile;
            }
        }
    }

    public Tile GetTileFromLocal(Vector2Int _localPosition)
    {
        if (tilesAtLocalPositions.ContainsKey(_localPosition))
            return tilesAtLocalPositions[_localPosition];

        return null;
    }

    public Tile GetTileFromGlobal(Vector2 _globalPosition)
    {
        Vector2Int _convertedPosition = new Vector2Int((int)_globalPosition.x / tileSize, (int)_globalPosition.y / tileSize);
        if (tilesAtLocalPositions.ContainsKey(_convertedPosition))
            return tilesAtLocalPositions[_convertedPosition];

        return null;
    }

    public List<Tile> GetAllTiles()
    {
        List<Tile> _allTiles = new();
        foreach (Tile _tile in tilesAtLocalPositions.Values)
            _allTiles.Add(_tile);

        return _allTiles;
    }
}