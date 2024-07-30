using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New House", menuName = "Scriptable Objects/Buildings/House")]
public class House : ScriptableObject
{
    public string Name => buildingName;
    public Sprite Sprite => sprite;
    public List<ResourceCount> RequiredResources => requiredResources;
    public int MaxPersonCapacity => maxPersonCapacity;
    public int Level => level;

    [Header("Building Settings")]
    [SerializeField] private string buildingName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private List<ResourceCount> requiredResources;
    [SerializeField] private int level = 1;
    [SerializeField] private int maxPersonCapacity;
}