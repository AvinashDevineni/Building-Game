using UnityEngine;

public class DayEndHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private DaytimeManager daytimeManager;
    [Space(15)]

    [Header("Resource Production")]
    [SerializeField] private BuildingsManager buildingsManager;
    [SerializeField] private PlayerInventory playerInventory;

    private void Start()
    {
        daytimeManager.OnDayEnd += () =>
        {
            playerInventory.ResourceInventory.Add(buildingsManager.CalculateTotalBuildingProduction());
        };
    }
}