using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Work Building", menuName = "Scriptable Objects/Buildings/Work Building")]
public class WorkBuilding : ScriptableObject
{
    public string Name => buildingName;
    public Sprite Sprite => sprite;
    public List<ResourceCount> RequiredResources => requiredResources;
    public int Level => level;
    public List<ResourceCount> ResourcesGainedPerWorker => resourcesGainedPerWorker;
    public int MaxNumOfWorkers => maxNumberOfWorkers;

    [Header("Building Settings")]
    [SerializeField] private string buildingName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private List<ResourceCount> requiredResources;
    [SerializeField] private int level = 1;
    [Space(15)]

    [Header("Worker Settings")]
    [SerializeField] private List<ResourceCount> resourcesGainedPerWorker;
    [SerializeField] private int maxNumberOfWorkers;
}