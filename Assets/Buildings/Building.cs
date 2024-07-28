using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Buiding", menuName = "Scriptable Objects/Building")]
public class Building : ScriptableObject
{
    public string Name => buildingName;
    public Sprite Sprite => sprite;
    public List<ResourceCount> RequiredResources => requiredResources;
    public List<ResourceCount> ResourcesGainedPerWorker => resourcesGainedPerWorker;
    public int MaxNumOfWorkers => maxNumberOfWorkers;
    public int Level => level;

    [SerializeField] private string buildingName;
    [SerializeField] private Sprite sprite;
    [SerializeField] private List<ResourceCount> requiredResources;
    [SerializeField] private List<ResourceCount> resourcesGainedPerWorker;
    [SerializeField] private int maxNumberOfWorkers;
    [SerializeField] private int level = 1;
}