using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Tile Data", menuName = "Scriptable Objects/Tile")]
public class TileData : ScriptableObject
{
    public string Name => tileName;
    public Sprite Sprite => tileSprite;
    public List<Building> PossibleBuildings => possibleBuildings;

    [SerializeField] private string tileName;
    [SerializeField] private Sprite tileSprite;
    [SerializeField] private List<Building> possibleBuildings;
}