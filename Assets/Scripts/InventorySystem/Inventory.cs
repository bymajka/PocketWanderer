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
		public static event Action<List<InventoryItem>> OnInventoryChange;
		public static List<InventoryItem> Items { get; set; } = new();
		
		public void Add(ItemData itemData)
		{
			if (TryGetItem(itemData, out InventoryItem inventoryItem))
			{
				inventoryItem.AddToStack();
				OnInventoryChange?.Invoke(Items);
			}
			else
			{
				InventoryItem newItem = new(itemData);
				Items.Add(newItem);
				OnInventoryChange?.Invoke(Items);
			}
		}

		private static bool TryGetItem(ItemData itemData, out InventoryItem inventoryItem)
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
			OnInventoryChange?.Invoke(Items);
			
			if (inventoryItem.StackSize != 0)
				return;

			Items.Remove(inventoryItem);
			OnInventoryChange?.Invoke(Items);
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