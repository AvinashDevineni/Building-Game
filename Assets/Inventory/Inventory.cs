using System.Collections.Generic;

public class Inventory
{
    private Dictionary<Resource, int> inventory;

    public Inventory() => inventory = new();

    public Inventory(List<ResourceCount> _initialInventory)
    {
        inventory = new();

        foreach (ResourceCount _resourceCount in _initialInventory)
            Add(_resourceCount.Resource, _resourceCount.Count);
    }

    public bool TryGetResource(Resource _resource, out int _count)
    {
        if (inventory.TryGetValue(_resource, out _count))
            return true;

        return false;
    }

    public bool Add(Resource _resource, int _count)
    {
        int _newAmount = _count;
        bool _wasPresent = false;

        if (inventory.TryGetValue(_resource, out int _existingAmount))
        {
            _newAmount += _existingAmount;
            _wasPresent = true;
        }

        inventory[_resource] = _newAmount;
        return _wasPresent;
    }

    public bool Add(Resource _resource) => Add(_resource, 1);

    public void Add(List<ResourceCount> _resources)
    {
        foreach (ResourceCount _resourceCount in _resources)
            Add(_resourceCount.Resource, _resourceCount.Count);
    }

    public void Add(Inventory _inventory) => Add(_inventory.GetInventoryItems());

    public bool Remove(Resource _resource, int _count)
    {
        if (inventory.TryGetValue(_resource, out int _existingAmount))
        {
            int _newAmount = _existingAmount - _count;
            _newAmount = UnityEngine.Mathf.Clamp(_newAmount, 0, _existingAmount);
            inventory[_resource] = _newAmount;

            return true;
        }

        else return false;
    }

    public bool Remove(Resource _resource) => Remove(_resource, 1);

    public void Remove(List<ResourceCount> _resources)
    {
        foreach (ResourceCount _resourceCount in _resources)
            Remove(_resourceCount.Resource, _resourceCount.Count);
    }

    public void Remove(Inventory _inventory) => Remove(_inventory.GetInventoryItems());

    public bool AreResourcesInInventory(List<ResourceCount> _resources)
    {
        foreach (ResourceCount _resourceCount in _resources)
        {
            Resource _castedResource = _resourceCount.Resource;
            if (!inventory.ContainsKey(_castedResource))
                return false;

            if (inventory[_castedResource] < _resourceCount.Count)
                return false;
        }

        return true;
    }

    public bool AreResourcesInInventory(Inventory _inventory) => AreResourcesInInventory(_inventory.GetInventoryItems());

    public List<ResourceCount> GetInventoryItems()
    {
        List<ResourceCount> _counts = new();
        foreach (KeyValuePair<Resource, int> _itemAmount in inventory)
            _counts.Add(new ResourceCount(_itemAmount.Key, _itemAmount.Value));

        return _counts;
    }
}