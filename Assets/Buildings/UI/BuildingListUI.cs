using UnityEngine;

public class BuildingListUI : MonoBehaviour
{
    [SerializeField] private BuildingUI buildingUiPrefab;
    [SerializeField] private RectTransform uiParent;

    public BuildingUI AddBuilding(Building _building)
    {
        BuildingUI _ui = Instantiate(buildingUiPrefab, uiParent);
        _ui.SetBuilding(_building);

        return _ui;
    }
}