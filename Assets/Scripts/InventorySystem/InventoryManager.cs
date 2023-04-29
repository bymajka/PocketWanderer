using System;
using System.Collections.Generic;
using ItemSystem;
using ItemSystem.ItemsData;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public class InventoryManager<T>: MonoBehaviour where T : Inventory
    {
        [SerializeField] private GameObject slotPrefab;
        private List<InventorySlot> _inventorySlots;
        [SerializeField] private T inventory;
        public static event Action<ItemData> OnRemoveItem; 

        public InventoryManager(T inventory)
        {
            this.inventory = inventory;
            _inventorySlots = new List<InventorySlot>(inventory.Capacity);
        }

        private void Awake()
        {
            DrawInventory();
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

            _inventorySlots = new List<InventorySlot>(inventory.Capacity);
        }

        private void DrawInventory()
        {
            ResetInventory();

            CreateEmptyInventory(_inventorySlots.Capacity);

            for (int i = 0; i < inventory.Items.Count; i++)
            {
                _inventorySlots[i].FillSlot(inventory.Items[i]);
            }
            
            SignUpSlots();
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

        public void EnableDisableInventory()
        {
            GameObject parent;
            GameObject children = gameObject;
            do
            {
                parent = children.transform.parent.gameObject;
                children = parent;
            } while (children.transform.parent != null && !children.GetComponent<ScrollRect>());
            parent.gameObject.SetActive(!parent.activeSelf);
        }

        public static void RemoveDirectItem(InventorySlot inventorySlot)
        {
            if (inventorySlot.InventoryItem == null)
                return;
                
            OnRemoveItem?.Invoke(inventorySlot.InventoryItem.ItemData);
        }

        public void SignUpSlots()
        {
            foreach (var slot in _inventorySlots)
            {
                slot.onClickRemove += () => RemoveDirectItem(slot);
            }
        }
    }
}