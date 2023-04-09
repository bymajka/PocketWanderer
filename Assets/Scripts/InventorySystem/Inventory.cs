using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class Inventory : MonoBehaviour
	{
		public List<InventoryItem> Items { get; set; } = new();
		private Dictionary<ItemData, InventoryItem> _inventoryItemByItemData = new();

		public void Add(ItemData itemData)
		{
			if (_inventoryItemByItemData.TryGetValue(itemData, out InventoryItem inventoryItem))
			{
				inventoryItem.AddToStack();
			}
			else
			{
				InventoryItem newItem = new(itemData);
				Items.Add(newItem);
				_inventoryItemByItemData.Add(itemData, newItem);
			}
		}

		public void Remove(ItemData itemData)
		{
			if (_inventoryItemByItemData.TryGetValue(itemData, out InventoryItem inventoryItem))
			{
				inventoryItem.RemoveFromStack();
				if (inventoryItem.StackSize == 0)
				{
					Items.Remove(inventoryItem);
					_inventoryItemByItemData.Remove(itemData);
				}
			}
		}
	}
}