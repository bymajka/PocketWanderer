using System.Collections.Generic;
using ItemSystem.ItemsData;

namespace InventorySystem
{
	public class EnemyInventory : Inventory<ItemData>
	{
		public override List<ItemData> Items { get; set; } = new();

		public override void Add(ItemData itemData)
		{
			Items.Add(itemData);
		}

		public override void Remove(ItemData itemData)
		{
			Items.Remove(itemData);
		}
	}
}