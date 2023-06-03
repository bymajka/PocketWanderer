using System;
using System.Collections.Generic;
using System.Linq;
using ItemSystem;
using ItemSystem.ItemsData;
using UnityEngine;

namespace InventorySystem
{
	public abstract class Inventory : MonoBehaviour
	{
		public static event Action<InventoryItem> OnInventoryChange;
		public List<InventoryItem> Items { get; set; } = new();
		[field: SerializeField] public int Capacity { get; private set; }
		
		public virtual void Add(ItemData itemData)
		{
			var inventoryItem = Items.Where(i => i.ItemData == itemData && i.StackSize < itemData.StackCapacity)
				.FirstOrDefault();

			if (inventoryItem != null)
			{
				inventoryItem.AddToStack();
				OnInventoryChange?.Invoke(inventoryItem);
			}
			else
			{
				InventoryItem newItem = new(itemData);
				Items.Add(newItem);
				OnInventoryChange?.Invoke(newItem);
			}
		}

		protected virtual void Remove(ItemData itemData)
		{
			var inventoryItem = Items.Where(i => i.ItemData == itemData && i.StackSize > 0)
				.LastOrDefault();

			inventoryItem.RemoveFromStack();
			OnInventoryChange?.Invoke(inventoryItem);
			
			if (inventoryItem.StackSize != 0)
				return;

			Items.Remove(inventoryItem);
		}

		public void TransferItem(ItemData itemToTransfer, Inventory otherInventory)
		{
			Remove(itemToTransfer);
			otherInventory.Add(itemToTransfer);
		}
	}
}