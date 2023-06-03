using System;
using System.Linq;
using ItemSystem;
using ItemSystem.ItemsData;

namespace InventorySystem
{
	public class PlayerInventory : Inventory
	{
		public static event Action<InventoryItem> OnInventoryChangedPlayer;
		public override void Add(ItemData itemData)
		{
			var inventoryItem = Items.Where(i => i.ItemData == itemData && i.StackSize < itemData.stackCapacity)
				.FirstOrDefault();

			if (inventoryItem != null)
			{
				inventoryItem.AddToStack();
				OnInventoryChangedPlayer?.Invoke(inventoryItem);
			}
			else
			{
				InventoryItem newItem = new(itemData);
				Items.Add(newItem);
				OnInventoryChangedPlayer?.Invoke(newItem);
			}
		}
		protected override void Remove(ItemData itemData)
		{
			var inventoryItem = Items
				.LastOrDefault(i => i.ItemData == itemData && i.StackSize > 0);

			if (inventoryItem != null)
			{
				inventoryItem.RemoveFromStack();
				OnInventoryChangedPlayer?.Invoke(inventoryItem);

				if (inventoryItem.StackSize != 0)
					return;

				Items.Remove(inventoryItem);
			}
		}

		private void OnEnable()
		{
			Item.OnItemCollected += Add;
			InventoryManager<PlayerInventory>.OnRemoveItem += Remove;
		}

		private void OnDisable()
		{
			Item.OnItemCollected -= Add;
			InventoryManager<PlayerInventory>.OnRemoveItem -= Remove;
		}
	}
}