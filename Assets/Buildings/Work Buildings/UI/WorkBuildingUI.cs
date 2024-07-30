using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WorkBuildingUI : MonoBehaviour
{
    public event Action<WorkBuilding> OnBuildingCreate;
    public event Action OnDecreaseWorkersClicked;
    public event Action OnIncreaseWorkersClicked;

    [SerializeField] private Button buildingCreateButton;

    [Header("Building UI")]
    [SerializeField] private TextMeshProUGUI buildingNameText;
    [SerializeField] private TextMeshProUGUI buildingLevelText;
    [SerializeField] private Image buildingImage;
    [SerializeField] private ResourceListUI costList;
    [SerializeField] private ResourceListUI gainsList;

    [Header("Current Building UI Only")]
    [SerializeField] private Button decreaseWorkersButton; 
    [SerializeField] private Button increaseWorkersButton;
    [SerializeField] private TextMeshProUGUI numWorkersOverMaxText;

    private WorkBuilding setBuilding = null;

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

        // null for unbuilt buildings
        if (decreaseWorkersButton != null)
        {
            decreaseWorkersButton.onClick.AddListener(() => OnDecreaseWorkersClicked?.Invoke());
            increaseWorkersButton.onClick.AddListener(() => OnIncreaseWorkersClicked?.Invoke());
        }
    }

    public void SetBuilding(WorkBuilding _building, int _numWorkers = -1)
    {
        buildingNameText.text = _building.Name;
        buildingLevelText.text = $"Level {_building.Level}";
        buildingImage.sprite = _building.Sprite;

        foreach (ResourceCount _resourceCost in _building.RequiredResources)
            costList.AddResource(_resourceCost);

        foreach (ResourceCount _resourceCost in _building.ResourcesGainedPerWorker)
            gainsList.AddResource(_resourceCost);

        setBuilding = _building;

        if (numWorkersOverMaxText != null)
            SetWorkersText(_building, _numWorkers);
    }

    public void SetWorkersText(WorkBuilding _building, int _numWorkers)
    {
        numWorkersOverMaxText.text = $"{_numWorkers}/{_building.MaxNumOfWorkers}";
    }
}