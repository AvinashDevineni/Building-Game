using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private TileData tileData = null;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool hasBeenInitialized = false;

    private Building building = null;

    private void Awake()
    {
        if (tileData != null)
            Initialize(tileData);
    }

    public void Initialize(TileData _newData)
    {
        if (hasBeenInitialized)
            Debug.LogError($"Tile {tileData.Name} has already been initialized.");

        tileData = _newData;
        spriteRenderer.sprite = _newData.Sprite;

        hasBeenInitialized = true;
    }

    public TileData GetData() => tileData;

    public Building GetBuilding() => building;

    public bool TrySetBuilding(Building _building)
    {
        if (tileData.PossibleBuildings.Contains(_building))
        {
            building = _building;
            spriteRenderer.sprite = building.Sprite;
            return true;
        }

        return false;
    }
}