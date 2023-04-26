using System;
using System.Collections.Generic;
using ItemSystem;
using ItemSystem.ItemsData;
using UnityEngine;

namespace InventorySystem
{
	public class Inventory : MonoBehaviour
	{
		public static event Action<List<InventoryItem>> OnInventoryChange;
		public static List<InventoryItem> Items { get; set; } = new();
		private readonly Dictionary<ItemData, InventoryItem> _inventoryItemByItemData = new();

		public void Add(ItemData itemData)
		{
			if (TryGetInventoryItem(itemData, out InventoryItem inventoryItem))
			{
				inventoryItem.AddToStack();
				OnInventoryChange?.Invoke(Items);
			}
			else
			{
				InventoryItem newItem = new(itemData);
				Items.Add(newItem);
				_inventoryItemByItemData.Add(itemData, newItem);
				OnInventoryChange?.Invoke(Items);
			}
		}

		public void Remove(ItemData itemData)
		{
			if (!TryGetInventoryItem(itemData, out InventoryItem inventoryItem))
				return;

			inventoryItem.RemoveFromStack();
			if (inventoryItem.StackSize != 0)
				return;

			Items.Remove(inventoryItem);
			_inventoryItemByItemData.Remove(itemData);
		}

		public void TransferItem(ItemData itemToTransfer, Inventory otherInventory)
		{
			Remove(itemToTransfer);

			otherInventory.Add(itemToTransfer);
		}

		private bool TryGetInventoryItem(ItemData itemData, out InventoryItem inventoryItem)
		{
			return _inventoryItemByItemData.TryGetValue(itemData, out inventoryItem);
		}
	}
}