using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool isStackable;
    public int maxStackSize;
    public ItemType itemType;

    public enum ItemType
    {
        Consumable,
        Equipment,
        Material
    }
}
