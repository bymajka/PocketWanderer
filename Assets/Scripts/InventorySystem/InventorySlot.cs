using ItemSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI labelText;
        [SerializeField] private TextMeshProUGUI stackSizeText;

        public void ClearSlot()
        {
            image.enabled = false;
            labelText.enabled = false;
            stackSizeText.enabled = false;
        }

        public void FillSlot(InventoryItem inventoryItem)
        {
            if (inventoryItem == null)
            {
                ClearSlot();
                return;
            }

            image.enabled = true;
            labelText.enabled = true;
            stackSizeText.enabled = true;
        
            image.sprite = inventoryItem.ItemData.icon;
            labelText.text = inventoryItem.ItemData.displayName;
            stackSizeText.text = inventoryItem.StackSize.ToString();
        }
    }
}
