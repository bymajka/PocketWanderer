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
			if (TryGetItem(itemData, out InventoryItem inventoryItem))
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

		private bool TryGetItem(ItemData itemData, out InventoryItem inventoryItem)
		{
			inventoryItem = Items.Where(i => i.ItemData == itemData && i.StackSize < itemData.stackCapacity)
				.FirstOrDefault();

			return inventoryItem != null;
		}

		public void Remove(ItemData itemData)
		{
			if (!TryGetItem(itemData, out InventoryItem inventoryItem))
				return;

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

		//private bool TryGetInventoryItem(ItemData itemData, out InventoryItem inventoryItem)
		//{
		//	return _inventoryItemByItemData.TryGetValue(itemData, out inventoryItem);
		//}
	}
}