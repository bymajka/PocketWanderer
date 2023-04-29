using System;
using System.Collections.Generic;
using System.Linq;
using ItemSystem;
using ItemSystem.ItemsData;
using UnityEngine;

namespace InventorySystem
{
	public class Inventory : MonoBehaviour
	{
		public static event Action OnInventoryChange;
		public List<InventoryItem> Items { get; set; } = new();
		[field: SerializeField] public int Capacity { get; private set; }
		
		public void Add(ItemData itemData)
		{
			var inventoryItem = Items.Where(i => i.ItemData == itemData && i.StackSize < itemData.stackCapacity)
				.FirstOrDefault();

			if (inventoryItem != null)
			{
				inventoryItem.AddToStack();
				OnInventoryChange?.Invoke();
			}
			else
			{
				InventoryItem newItem = new(itemData);
				Items.Add(newItem);
				OnInventoryChange?.Invoke();
			}
		}

		public void Remove(ItemData itemData)
		{
			var inventoryItem = Items.Where(i => i.ItemData == itemData && i.StackSize > 0)
				.LastOrDefault();

			inventoryItem.RemoveFromStack();
			OnInventoryChange?.Invoke();
			
			if (inventoryItem.StackSize != 0)
				return;

			Items.Remove(inventoryItem);
			OnInventoryChange?.Invoke();
			//_inventoryItemByItemData.Remove(itemData);
		}

		public void TransferItem(ItemData itemToTransfer, Inventory otherInventory)
		{
			Remove(itemToTransfer);

			otherInventory.Add(itemToTransfer);
		}
	}
}