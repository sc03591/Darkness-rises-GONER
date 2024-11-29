using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory/Equipment Item")]
public class EquipmentItem : Item
{
    public int attackPower;
    public int defenseValue;

    public void Equip()
    {
        Debug.Log("Equipped: " + itemName);
        // Implement equipment logic (e.g., increase stats)
    }
}
