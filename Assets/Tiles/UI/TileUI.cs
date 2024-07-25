using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class TileUI : MonoBehaviour
{
    public event Action<Tile, Building> OnBuildingCreate;

    public Vector2 BottomLeft => bottomLeft.position;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI tileNameText;
    [SerializeField] private BuildingListUI buildingList;
    [Space(15)]

    [SerializeField] private Transform bottomLeft;

    private List<BuildingUI> buildingUIs = new();

    public void SetTile(Tile _tile)
    {
        foreach (BuildingUI _buildingUI in buildingUIs)
            Destroy(_buildingUI.gameObject);

        buildingUIs = new();

        tileNameText.text = _tile.GetData().Name;
        foreach (Building _building in _tile.GetData().PossibleBuildings)
        {
            BuildingUI _buildingUI = buildingList.AddBuilding(_building);
            buildingUIs.Add(_buildingUI);
            _buildingUI.OnBuildingCreate += _building =>
            {
                if (_tile == null)
                    return;

                OnBuildingCreate?.Invoke(_tile, _building);
            };
        }
    }
}