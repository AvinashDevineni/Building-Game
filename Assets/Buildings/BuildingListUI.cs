using UnityEngine;

public class BuildingListUI : MonoBehaviour
{
    [SerializeField] private RectTransform uiParent;

    [Header("UI Prefabs")]
    [SerializeField] private WorkBuildingUI unbuiltWorkBuildingUiPrefab;
    [SerializeField] private WorkBuildingUI builtWorkBuildingUiPrefab;
    [SerializeField] private HouseUI unbuiltHouseUiPrefab;
    [SerializeField] private HouseUI builtHouseUiPrefab;

    public WorkBuildingUI AddCurrentWorkBuilding(WorkBuilding _building, int _numWorkers)
    {
        WorkBuildingUI _ui = Instantiate(builtWorkBuildingUiPrefab, uiParent);
        _ui.SetBuilding(_building, _numWorkers);

        return _ui;
    }

    public WorkBuildingUI AddWorkBuilding(WorkBuilding _building)
    {
        WorkBuildingUI _ui = Instantiate(unbuiltWorkBuildingUiPrefab, uiParent);
        _ui.SetBuilding(_building);

        return _ui;
    }

    public HouseUI AddCurrentHouse(House _building, int _numWorkers)
    {
        HouseUI _ui = Instantiate(builtHouseUiPrefab, uiParent);
        _ui.SetBuilding(_building, _numWorkers);

        return _ui;
    }

    public HouseUI AddHouse(House _building)
    {
        HouseUI _ui = Instantiate(unbuiltHouseUiPrefab, uiParent);
        _ui.SetBuilding(_building);

        return _ui;
    }

    public void DestroyBuildingUIs() => uiParent.DestroyChildren();

    public void DestroyWorkBuildingUIs()
    {
        uiParent.DestroyChildren(_trans => _trans.TryGetComponent(out WorkBuildingUI _));
    }

    public void DestroyHouseUIs()
    {
        uiParent.DestroyChildren(_trans => _trans.TryGetComponent(out HouseUI _));
    }
}