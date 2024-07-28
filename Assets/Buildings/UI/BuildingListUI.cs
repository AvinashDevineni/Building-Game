using UnityEngine;

public class BuildingListUI : MonoBehaviour
{
    [SerializeField] private RectTransform uiParent;

    [Header("UI Prefabs")]
    [SerializeField] private BuildingUI unbuiltBuildingUiPrefab;
    [SerializeField] private BuildingUI builtBuildingUiPrefab;

    public BuildingUI AddCurrentBuilding(Building _building, int _numWorkers)
    {
        BuildingUI _ui = Instantiate(builtBuildingUiPrefab, uiParent);
        _ui.SetBuilding(_building, _numWorkers);

        return _ui;
    }

    public BuildingUI AddBuilding(Building _building)
    {
        BuildingUI _ui = Instantiate(unbuiltBuildingUiPrefab, uiParent);
        _ui.SetBuilding(_building);

        return _ui;
    }

    public void DestroyBuildingUIs() => uiParent.DestroyChildren();
}