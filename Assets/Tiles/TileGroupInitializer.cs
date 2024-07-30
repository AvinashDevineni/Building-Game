using UnityEngine;
using System.Collections.Generic;

public class TileGroupInitializer : MonoBehaviour
{
    [SerializeField] private List<TileGroup> tileGroupsToInitialize;
    [SerializeField] private List<TileData> tileDatas;

    [ContextMenu("Initialize Tile Groups")]
    public void InitializeTileGroups()
    {
        int _numTiles = tileGroupsToInitialize.Count;
        if (_numTiles != tileDatas.Count)
            Debug.LogError("tileGroupsToInitialize and tileDatas must be the same length.");

        for (int i = 0; i < _numTiles; i++)
        {
            foreach (TileGroup.TileDataFrequency _tileVariation in tileGroupsToInitialize[i].TileVariations)
                _tileVariation.TileData.CopyTileData(tileDatas[i]);
        }
    }
}