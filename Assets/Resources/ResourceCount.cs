using UnityEngine;

[System.Serializable]
public class ResourceCount
{
    public Resource Resource => resource;
    public int Count => count;

    [SerializeField] private Resource resource;
    [SerializeField] private int count;

    public ResourceCount(Resource _resource, int _count)
    {
        resource = _resource;
        count = _count;
    }
}