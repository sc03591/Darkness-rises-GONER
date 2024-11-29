using UnityEngine;

public class GameController : MonoBehaviour
{
    private Inventory playerInventory;

    void Start()
    {
        // Find the player's inventory component
        playerInventory = FindObjectOfType<Inventory>();

        // Example of adding items to the inventory
        Item potion = ScriptableObject.CreateInstance<Item>();
        potion.itemName = "Health Potion";
        potion.itemType = Item.ItemType.Consumable;

        playerInventory.AddItem(potion);

        // Example of using an item
        playerInventory.UseItem(potion);
    }
}
