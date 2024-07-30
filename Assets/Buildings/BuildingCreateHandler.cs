using UnityEngine;

public class BuildingCreateHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TileSelectUiManager tileSelectUi;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private WorkBuildingsManager workBuildingsManager;
    [SerializeField] private HousesManager housesManager;
    [Space(15)]

    [Header("UI")]
    [SerializeField] private bool turnTileUiOffOnBuild = true;

    private void Start()
    {
        tileSelectUi.OnWorkBuildingCreate += (_tile, _workBuilding) =>
        {
            if (!playerInventory.ResourceInventory.AreResourcesInInventory(_workBuilding.RequiredResources))
            {
                Debug.Log("Resources not in inventory");
                return;
            }

            if (!_tile.TryBuildWorkBuilding(_workBuilding))
                Debug.LogError($"Tile ${_tile.GetData().Name} cannot have building ${_workBuilding.Name}");

            playerInventory.ResourceInventory.Remove(_workBuilding.RequiredResources);
            workBuildingsManager.UpdateTileWorkBuilding(_tile, _workBuilding);

            tileSelectUi.UpdateTileUI(_tile);
            tileSelectUi.SetTileUiActive(!turnTileUiOffOnBuild);
        };

        tileSelectUi.OnHouseCreate += (_tile, _house) =>
        {
            if (!playerInventory.ResourceInventory.AreResourcesInInventory(_house.RequiredResources))
            {
                Debug.Log("Resources not in inventory");
                return;
            }

            if (!_tile.TryBuildBuilding(_house))
                Debug.LogError($"Tile ${_tile.GetData().Name} cannot have building ${_house.Name}");

            playerInventory.ResourceInventory.Remove(_house.RequiredResources);
            housesManager.UpdateTileHouse(_tile, _house);

            tileSelectUi.UpdateTileUI(_tile);
            tileSelectUi.SetTileUiActive(!turnTileUiOffOnBuild);

            print("HOuse created!");
        };
    }
}