using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HouseUI : MonoBehaviour
{
    public event Action<House> OnBuildingCreate;

    [SerializeField] private Resource maxPeopleResource;
    [SerializeField] private Button buildingCreateButton;

    [Header("Building UI")]
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private TextMeshProUGUI buildingLevelText;
    [SerializeField] private Image buildingImage;
    [SerializeField] private ResourceListUI costList;
    [SerializeField] private ResourceListUI gainsList;

    [Header("Current Building UI Only")]
    [SerializeField] private TextMeshProUGUI numOccupantsOverMaxText;

    private House setBuilding = null;

    private void Start()
    {
        // null for built buildings
        if (buildingCreateButton != null)
        {
            buildingCreateButton.onClick.AddListener(() =>
            {
                if (setBuilding == null)
                    return;

                OnBuildingCreate?.Invoke(setBuilding);
            });
        }
    }

    public void SetBuilding(House _building, int _numPeopleOccupying = -1)
    {
        buildingNameText.text = _building.Name;
        buildingLevelText.text = $"Level {_building.Level}";
        buildingImage.sprite = _building.Sprite;

        foreach (ResourceCount _resourceCost in _building.RequiredResources)
            costList.AddResource(_resourceCost);

        gainsList.AddResource(new ResourceCount(maxPeopleResource, _building.MaxPersonCapacity));

        setBuilding = _building;

        if (numOccupantsOverMaxText != null)
            SetNumOccupantsText(_building, _numPeopleOccupying);
    }

    public void SetNumOccupantsText(House _building, int _numPeopleOccupying)
    {
        numOccupantsOverMaxText.text = $"{_numPeopleOccupying}/{_building.MaxPersonCapacity}";
    }

}