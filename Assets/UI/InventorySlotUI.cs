using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI quantityText;

    public void UpdateSlot(Sprite icon, int quantity)
    {
        itemIcon.sprite = icon;
        itemIcon.enabled = (icon != null); // Hide if no icon
        quantityText.text = quantity > 1 ? quantity.ToString() : "";
    }
}