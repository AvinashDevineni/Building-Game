using UnityEngine;
using System.Collections.Generic;

public class ResourceListUI : MonoBehaviour
{
    [SerializeField] private ResourceUI resourceUiPrefab;
    [SerializeField] private RectTransform uiParent;

    private Dictionary<ResourceCount, ResourceUI> resources = new();

    public bool PutResource(ResourceCount _resourceCount)
    {
        bool _isAlreadyContained = resources.ContainsKey(_resourceCount);

        ResourceUI _ui = Instantiate(resourceUiPrefab, uiParent);
        _ui.SetResourceCount(_resourceCount);
        resources[_resourceCount] = _ui;

        return _isAlreadyContained;
    }

    public bool AddResource(ResourceCount _resourceCount)
    {
        bool _isAlreadyContained;

        if (resources.TryGetValue(_resourceCount, out ResourceUI _ui))
        {
            _ui.SetCount(_ui.Count + _resourceCount.Count);
            _isAlreadyContained = true;
        }

        else
        {
            PutResource(_resourceCount);
            _isAlreadyContained = false;
        }

        return _isAlreadyContained;
    }
}