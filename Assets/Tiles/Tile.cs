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

    public bool TryBuildBuilding(Building _building)
    {
        if (tileData.IsBuildingPossible(_building))
        {
            tileData.TryGetBuildingResultingTile(_building, out TileData _newTileData);
            Initialize(_newTileData);

            return true;
        }

        return false;
    }
}