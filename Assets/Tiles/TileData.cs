using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Tile Data", menuName = "Scriptable Objects/Tiles/Tile")]
public class TileData : ScriptableObject
{
    public string Name => tileName;
    public Sprite Sprite => tileSprite;
    public bool IsBuilding => isBuilding;
    public int MaxPersonCapacity => maxPersonCapacity;
    public List<BuildingTile> PossibleBuildings => possibleBuildings;

    [Header("Tile Settings")]
    [SerializeField] private string tileName;
    [SerializeField] private Sprite tileSprite;
    [Space(15)]

    [Header("Building Settings")]
    [SerializeField] private bool isBuilding;
    [SerializeField] [Min(0)] private int maxPersonCapacity;
    [SerializeField] private List<BuildingTile> possibleBuildings;

    private Dictionary<Building, TileData> possibleBuildingsDict = null;

    public void CopyTileData(TileData _target)
    {
        Debug.LogWarning("Copying TileData.");

        tileName = _target.Name;
        tileSprite = _target.Sprite;
        possibleBuildings = _target.PossibleBuildings;
    }

    public bool IsBuildingPossible(Building _building)
    {
        if (possibleBuildingsDict == null)
            InitDict();

        return possibleBuildingsDict.ContainsKey(_building);
    }

    public bool TryGetBuildingResultingTile(Building _building, out TileData _tileData)
    {
        if (possibleBuildingsDict == null)
            InitDict();

        if (possibleBuildingsDict.TryGetValue(_building, out _tileData))
            return true;

        return false;
    }

    private void InitDict()
    {
        possibleBuildingsDict = new();
        foreach (BuildingTile _buildingTile in possibleBuildings)
            possibleBuildingsDict.Add(_buildingTile.Building, _buildingTile.ResultingTile);
    }

    [System.Serializable]
    public class BuildingTile
    {
        [field: SerializeField] public Building Building { get; private set; }
        [field: SerializeField] public TileData ResultingTile { get; private set; }
    }
}