using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuildingUI : MonoBehaviour
{
    public event Action<Building> OnBuildingCreate;

    [SerializeField] private Button buildingCreateButton;

    [Header("Building UI")]
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private TextMeshProUGUI buildingLevelText;
    [SerializeField] private Image buildingImage;
    [SerializeField] private ResourceListUI costList;
    [SerializeField] private ResourceListUI gainsList;

    private Building setBuilding = null;

    private void Start()
    {
        buildingCreateButton.onClick.AddListener(() =>
        {
            if (setBuilding == null)
                return;

            OnBuildingCreate?.Invoke(setBuilding);
        });
    }

    public void SetBuilding(Building _building)
    {
        buildingNameText.text = _building.Name;
        buildingLevelText.text = $"Level {_building.Level}";
        buildingImage.sprite = _building.Sprite;

        foreach (ResourceCount _resourceCost in _building.RequiredResources)
            costList.AddResource(_resourceCost);

        foreach (ResourceCount _resourceCost in _building.ResourcesGained)
            gainsList.AddResource(_resourceCost);

        setBuilding = _building;
    }
}