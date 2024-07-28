using UnityEngine;

public class WorkersChangeHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private WorkersManager workersManager;
    [SerializeField] private BuildingsManager buildingsManager;
    [SerializeField] private TileSelectUiManager tileSelectUi;

    private void Start()
    {
        tileSelectUi.OnDecreaseWorkers += (_tile, _buildingUi) =>
        {
            buildingsManager.TryGetBuildingOnTile(_tile, out Building _building);
            int _newNumWorkers = workersManager.RemoveWorker(_tile);

            _buildingUi.SetWorkersText(_building, _newNumWorkers);
        };

        tileSelectUi.OnIncreaseWorkers += (_tile, _buildingUi) =>
        {
            buildingsManager.TryGetBuildingOnTile(_tile, out Building _building);
            workersManager.TryAddWorker(_tile, out int _newNumWorkers);

            _buildingUi.SetWorkersText(_building, _newNumWorkers);
        };
    }
}
