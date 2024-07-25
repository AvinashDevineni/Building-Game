using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Buiding", menuName = "Scriptable Objects/Resources/Building")]
public class Building : Resource
{
    public List<ResourceCount> RequiredResources => requiredResources;
    public List<ResourceCount> ResourcesGained => resourcesGained;
    public int Level => level;

    [SerializeField] private List<ResourceCount> requiredResources;
    [SerializeField] private List<ResourceCount> resourcesGained;
    [SerializeField] private int level = 1;
}