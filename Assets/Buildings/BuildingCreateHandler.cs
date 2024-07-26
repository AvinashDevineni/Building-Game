using UnityEngine;

public class BuildingCreateHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TileSelectUiManager tileSelectUi;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private BuildingsManager buildingsManager;
    [Space(15)]

    [Header("UI")]
    [SerializeField] private bool turnTileUiOffOnBuild = true;

    private void Start()
    {
        tileSelectUi.OnBuildingCreate += (_tile, _building) =>
        {
            if (!playerInventory.ResourceInventory.AreResourcesInInventory(_building.RequiredResources))
            {
                Debug.Log("Resources not in inventory");
                return;
            }

            if (!_tile.TrySetBuilding(_building))
                Debug.LogError($"Tile ${_tile.GetData().Name} cannot have building ${_building.Name}");

            playerInventory.ResourceInventory.Remove(_building.RequiredResources);
            buildingsManager.UpdateTileBuilding(_tile, _building);

            tileSelectUi.SetTileUiActive(!turnTileUiOffOnBuild);
        };
    }
}