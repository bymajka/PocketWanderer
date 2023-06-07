using InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class ItemActionPanel : MonoBehaviour
    {
        private void Awake()
        {
            InventorySlot.RewriteSlotListener += RewriteListener;
        }

        private void OnDestroy()
        {
            InventorySlot.RewriteSlotListener -= RewriteListener;
        }

        private void Start()
        {
            var parent = GetComponentInParent<InventorySlot>();
            if(parent.InventoryItem == null)
                return;
            RewriteListener(parent);
        }

        private void RewriteListener(InventorySlot slot)
        {
            GetComponent<Button>().onClick.AddListener(slot.InventoryItem.Use);
        }
    }
}
