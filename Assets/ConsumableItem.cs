using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Inventory/Consumable Item")]
public class ConsumableItem : Item
{
    public int healthRestoreValue;
    public int manaRestoreValue;

    public void Consume()
    {
        Debug.Log("Consumed: " + itemName);
        // Implement consumption logic (e.g., restore health/mana)
    }
}
