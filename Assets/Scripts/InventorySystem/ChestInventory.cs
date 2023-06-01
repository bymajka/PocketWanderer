using System;
using System.Linq;
using ItemSystem;
using ItemSystem.ItemsData;

namespace InventorySystem
{
	public class ChestInventory : Inventory
	{
		public static event Action<InventoryItem> OnInventoryChangedChest;
		protected override void Add(ItemData itemData)
		{
			var inventoryItem = Items
				.FirstOrDefault(i => i.ItemData == itemData && i.StackSize < itemData.stackCapacity);

			if (inventoryItem != null)
			{
				inventoryItem.AddToStack();
				OnInventoryChangedChest?.Invoke(inventoryItem);
			}
			else
			{
				InventoryItem newItem = new(itemData);
				Items.Add(newItem);
				OnInventoryChangedChest?.Invoke(newItem);
			}
		}
		protected override void Remove(ItemData itemData)
		{
			var inventoryItem = Items
				.LastOrDefault(i => i.ItemData == itemData && i.StackSize > 0);

			if (inventoryItem != null)
			{
				inventoryItem.RemoveFromStack();
				OnInventoryChangedChest?.Invoke(inventoryItem);

				if (inventoryItem.StackSize != 0)
					return;

				Items.Remove(inventoryItem);
			}
		}
		private void OnEnable()
		{
			InventoryManager<ChestInventory>.OnRemoveItem += Remove;
		}

		private void OnDisable()
		{
			InventoryManager<ChestInventory>.OnRemoveItem -= Remove;
		}
	}
}