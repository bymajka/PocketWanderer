using System.Collections.Generic;
using ItemSystem;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryManager: MonoBehaviour
    {
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private int inventoryCapacity;
        private List<InventorySlot> _inventorySlots;

        public InventoryManager()
        {
            _inventorySlots = new List<InventorySlot>(inventoryCapacity);
        }

        private void Awake()
        {
            DrawInventory(Inventory.Items);
            Inventory.OnInventoryChange += DrawInventory;
        }

        private void OnDestroy()
        {
            Inventory.OnInventoryChange -= DrawInventory;
        }

        private void ResetInventory()
        {
            foreach (Transform childTransform in transform)
            {
                Destroy(childTransform.gameObject);
            }

            _inventorySlots = new List<InventorySlot>(inventoryCapacity);
        }

        private void DrawInventory(List<InventoryItem> inventory)
        {
            ResetInventory();

            CreateEmptyInventory(_inventorySlots.Capacity);

            for (int i = 0; i < inventory.Count; i++)
            {
                _inventorySlots[i].FillSlot(inventory[i]);
            }
        }

        private void CreateInventorySlot()
        {
            GameObject newSlot = Instantiate(slotPrefab, transform, false);
            InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
            newSlotComponent.ClearSlot();
            _inventorySlots.Add(newSlotComponent);
        }

        private void CreateEmptyInventory(int capacity)
        {
            for (int i = 0; i < capacity; i++)
                CreateInventorySlot();
        }

        public void EnableDisableInventory() => gameObject.SetActive(!gameObject.activeSelf);
    }
}