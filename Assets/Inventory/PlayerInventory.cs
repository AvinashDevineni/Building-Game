using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public Inventory ResourceInventory { get; private set; }

    [Header("Initial Inventory")]
    [SerializeField] private List<ResourceCount> initialResourceInventory;
    [Space(15)]

    [Header("Serialized Inventory")]
    [SerializeField] private List<ResourceCount> serializedResourceInventory;

    private void Start() => ResourceInventory = new(initialResourceInventory);

    private void Update() => serializedResourceInventory = ResourceInventory.GetInventoryItems();
}