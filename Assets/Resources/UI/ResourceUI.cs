using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    public Resource Resource { get; private set; }
    public int Count { get; private set; }

    [SerializeField] private Image resourceImage;
    [SerializeField] private TextMeshProUGUI countText;

    public void SetResourceCount(ResourceCount _resourceCount)
    {
        Resource = _resourceCount.Resource;
        Count = _resourceCount.Count;

        resourceImage.sprite = Resource.Sprite;
        countText.text = Count.ToString();
    }

    public void SetCount(int _count) => countText.text = _count.ToString();
}