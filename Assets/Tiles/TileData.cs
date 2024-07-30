using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Tile Data", menuName = "Scriptable Objects/Tiles/Tile")]
public class TileData : ScriptableObject
{
    public string Name => tileName;
    public Sprite Sprite => tileSprite;
    public List<WorkBuildingTile> PossibleBuildings => possibleWorkBuildings;
    public List<HouseTile> PossibleHouses => possibleHouses;

    [Header("Tile Settings")]
    [SerializeField] private string tileName;
    [SerializeField] private Sprite tileSprite;
    [Space(15)]

    [Header("Building Settings")]
    [SerializeField] private List<WorkBuildingTile> possibleWorkBuildings;
    [SerializeField] private List<HouseTile> possibleHouses;

    private Dictionary<WorkBuilding, WorkBuildingTile> possibleWorkBuildingsDict = null;
    private Dictionary<House, HouseTile> possibleHousesDict = null;

    public void CopyTileData(TileData _target)
    {
        tileName = _target.Name;
        tileSprite = _target.Sprite;
        possibleWorkBuildings = _target.PossibleBuildings;
        possibleHouses = _target.PossibleHouses;
    }

    public bool IsWorkBuildingPossible(WorkBuilding _building)
    {
        if (possibleWorkBuildingsDict == null)
            InitWorkDict();

        return possibleWorkBuildingsDict.ContainsKey(_building);
    }

    public bool TryGetWorkBuildingResultingTileInfo(WorkBuilding _building, out WorkBuildingTile _tileGroup)
    {
        if (possibleWorkBuildingsDict == null)
            InitWorkDict();

        if (possibleWorkBuildingsDict.TryGetValue(_building, out _tileGroup))
            return true;

        return false;
    }

    public bool IsHousePossible(House _building)
    {
        if (possibleHousesDict == null)
            InitHouseDict();

        return possibleHousesDict.ContainsKey(_building);
    }

    public bool TryGetHouseResultingTileInfo(House _building, out HouseTile _tileGroup)
    {
        if (possibleHousesDict == null)
            InitHouseDict();

        if (possibleHousesDict.TryGetValue(_building, out _tileGroup))
            return true;

        return false;
    }

    private void InitWorkDict()
    {
        possibleWorkBuildingsDict = new();
        foreach (WorkBuildingTile _buildingTile in possibleWorkBuildings)
            possibleWorkBuildingsDict.Add(_buildingTile.Building, _buildingTile);
    }

    private void InitHouseDict()
    {
        possibleHousesDict = new();
        foreach (HouseTile _buildingTile in possibleHouses)
            possibleHousesDict.Add(_buildingTile.Building, _buildingTile);
    }

    [System.Serializable]
    public class WorkBuildingTile
    {
        [field: SerializeField] public bool HasVariations { get; private set; } = false;

        [field: SerializeField] public WorkBuilding Building { get; private set; }
        [field: SerializeField] public TileData ResultingTileData { get; private set; }
        [field: SerializeField] public TileGroup ResultingVariations { get; private set; }
    }

    [System.Serializable]
    public class HouseTile
    {
        [field: SerializeField] public bool HasVariations { get; private set; } = false;

        [field: SerializeField] public House Building { get; private set; }
        [field: SerializeField] public TileData ResultingTileData { get; private set; }
        [field: SerializeField] public TileGroup ResultingVariations { get; private set; }
    }
}