using UnityEngine;

public class DayEndHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private DaytimeManager daytimeManager;
    [Space(15)]

    [Header("Resource Production")]
    [SerializeField] private WorkBuildingsManager buildingsManager;
    [SerializeField] private PlayerInventory playerInventory;
    [Space(15)]

    [Header("Occupants Creation")]
    [SerializeField] private HousesManager housesManager;
    [SerializeField] private HouseOccupantsManager houseOccupantsManager;

    private void Start()
    {
        daytimeManager.OnDayEnd += () =>
        {
            playerInventory.ResourceInventory.Add(buildingsManager.CalculateTotalWorkBuildingProduction());

            foreach (Tile _tileWithHouse in housesManager.GetAllTilesWithHouses())
                houseOccupantsManager.TryFillHouse(_tileWithHouse, out int _);
        };
    }
}