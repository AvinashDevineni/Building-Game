using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Scriptable Objects/Resources/Resource")]
public class Resource : ScriptableObject
{
    public string Name => resourceName;
    public Sprite Sprite => sprite;

    [SerializeField] private string resourceName;
    [SerializeField] private Sprite sprite;

    public override bool Equals(object other)
    {
        Resource _item = (Resource)other;
        return _item.Name == Name;
    }

    public override int GetHashCode() => Name.GetHashCode();
}