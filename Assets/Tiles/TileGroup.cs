using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Tile Group", menuName = "Scriptable Objects/Tiles/Tile Group")]
public class TileGroup : ScriptableObject
{
    public List<TileDataFrequency> TileVariations => tileVariations;

    [SerializeField] private List<TileDataFrequency> tileVariations;

    public TileData GetRandomTileBasedOnFrequencies()
    {
        float _freqSum = 0;
        foreach (TileDataFrequency _tileDataFrequency in tileVariations)
            _freqSum += _tileDataFrequency.Frequency;

        float _chosen = Random.Range(0, _freqSum);
        foreach (TileDataFrequency _tileDataFrequency in tileVariations)
        {
            _chosen -= _tileDataFrequency.Frequency;

            if (_chosen <= 0)
                return _tileDataFrequency.TileData;
        }

        Debug.LogError("GetRandomTileBasedOnFrequences failed.");

        return null;
    }

    public void SetTileVariationsTo(TileData _data)
    {
        tileVariations = new();
        tileVariations.Add(new TileDataFrequency(_data));
    }

    [System.Serializable]
    public class TileDataFrequency
    {
        [field: SerializeField] public TileData TileData { get; private set; }
        [field: SerializeField] public float Frequency { get; private set; } = 1f;
    
        public TileDataFrequency(TileData _data, float _freq = 1)
        {
            TileData = _data;
            Frequency = _freq;
        }
    }
}