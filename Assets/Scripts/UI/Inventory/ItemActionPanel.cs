using InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class ItemActionPanel : MonoBehaviour
    {
        private InventorySlot _inventorySlot;
        private bool _isListenerSet;
        
        private void Awake()
        {
            _inventorySlot = GetComponentInParent<InventorySlot>();
        }

        private void Update()
        {
            if (_inventorySlot.InventoryItem == null || _isListenerSet)
                return;
            
            GetComponent<Button>().onClick.AddListener(UseItem);
            _isListenerSet = true;
        }
        
        private void UseItem()
        {
            _inventorySlot.InventoryItem.Use();
        }
    }
}
