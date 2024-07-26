using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Tile Data", menuName = "Scriptable Objects/Tile")]
public class TileData : ScriptableObject
{
    public string Name => tileName;
    public Sprite Sprite => tileSprite;
    public List<BuildingTile> PossibleBuildings => possibleBuildings;

    [SerializeField] private string tileName;
    [SerializeField] private Sprite tileSprite;
    [SerializeField] private List<BuildingTile> possibleBuildings;

    private Dictionary<Building, TileData> possibleBuildingsDict = null;

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