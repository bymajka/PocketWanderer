using System;
using System.Collections.Generic;
using System.Linq;
using ItemSystem;
using ItemSystem.ItemsData;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
    public abstract class InventoryManager<T>: MonoBehaviour where T : Inventory
    {
        [SerializeField] private GameObject slotPrefab;
        public List<InventorySlot> InventorySlots { get; protected set; }
        [SerializeField] protected T inventory;
        public static event Action<ItemData> OnRemoveItem; 

        public InventoryManager(T inventory)
        {
            this.inventory = inventory;
        }

        private void Awake()
        {
            InventorySlots = new List<InventorySlot>(inventory.Capacity);
            CreateEmptyInventory();
        }

        private void CreateInventorySlot()
        {
            GameObject newSlot = Instantiate(slotPrefab, transform, false);
            InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
            newSlotComponent.ClearSlot();
            InventorySlots.Add(newSlotComponent);
        }

        protected void CreateEmptyInventory()
        {
            for (int i = 0; i < InventorySlots.Capacity; i++)
                CreateInventorySlot();
            for (int i = 0; i < inventory.Items.Count; i++)
                InventorySlots[i].FillSlot(inventory.Items[i]);
            SignUpSlots();
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
            foreach (var slot in InventorySlots)
            {
                slot.onClickRemove += () => RemoveDirectItem(slot);
            }
        }

        public void UpdateSlot(InventoryItem item)
        {
            var occupiedSlot = InventorySlots.Find(i => i.InventoryItem == item);
            if (occupiedSlot != null)
            {
                if (occupiedSlot.InventoryItem.StackSize <= 0)
                {
                    occupiedSlot.ClearSlot();
                    return;
                }
                occupiedSlot.FillSlot(item);
            }
            else 
                InventorySlots.FirstOrDefault(i => i.InventoryItem == null)!.FillSlot(item);
        }
    }
}