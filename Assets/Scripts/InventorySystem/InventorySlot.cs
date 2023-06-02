using ItemSystem;
using TMPro;
using UI.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI labelText;
        [SerializeField] private TextMeshProUGUI stackSizeText;
        [SerializeField] private Button deleteButton;

        public delegate void OnClickRemove();

        public OnClickRemove onClickRemove;
        public InventoryItem InventoryItem { get; private set; }

        public void OnEnable()
        {
            deleteButton.onClick.AddListener(() => onClickRemove?.Invoke());
        }

        public void ClearSlot()
        {
            InventoryItem = null;
            image.enabled = false;
            labelText.enabled = false;
            stackSizeText.enabled = false;
            GetComponentInChildren<ItemActionPanel>().RemoveOldButtons();
        }

        public void FillSlot(InventoryItem inventoryItem)
        {
            /*if (inventoryItem == null)
            {
                ClearSlot();
                return;
            }*/

            InventoryItem = inventoryItem;

            image.enabled = true;
            labelText.enabled = true;
            stackSizeText.enabled = true;
        
            image.sprite = inventoryItem.ItemData.icon;
            labelText.text = inventoryItem.ItemData.displayName;
            stackSizeText.text = inventoryItem.StackSize.ToString();
        }
    }
}
