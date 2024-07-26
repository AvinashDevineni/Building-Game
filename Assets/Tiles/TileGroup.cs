using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Tile Group", menuName = "Scriptable Objects/Tiles/Tile Group")]
public class TileGroup : ScriptableObject
{
    public List<TileData> TileVariations => tileVariations;

    [SerializeField] private List<TileData> tileVariations;
}