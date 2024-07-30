using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileData tileData = null;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (tileData != null)
            Initialize(tileData);
    }

    public void Initialize(TileData _newData)
    {
        tileData = _newData;
        spriteRenderer.sprite = _newData.Sprite;
    }

    public TileData GetData() => tileData;

    public bool TryBuildWorkBuilding(WorkBuilding _building)
    {
        if (tileData.IsWorkBuildingPossible(_building))
        {
            tileData.TryGetWorkBuildingResultingTileInfo(_building, out TileData.WorkBuildingTile _newTileData);

            if (_newTileData.HasVariations)
                Initialize(_newTileData.ResultingVariations.GetRandomTileBasedOnFrequencies());
            else Initialize(_newTileData.ResultingTileData);

            return true;
        }

        return false;
    }

    public bool TryBuildBuilding(House _building)
    {
        if (tileData.IsHousePossible(_building))
        {
            tileData.TryGetHouseResultingTileInfo(_building, out TileData.HouseTile _newTileData);

            if (_newTileData.HasVariations)
                Initialize(_newTileData.ResultingVariations.GetRandomTileBasedOnFrequencies());
            else Initialize(_newTileData.ResultingTileData);

            return true;
        }

        return false;
    }
}