using System;

namespace InventorySystem
{
	public class InventoryItem
	{
		public int StackSize { get; set; }
		private ItemData _itemData;

		public InventoryItem(ItemData itemData)
		{
			_itemData = itemData;
		}

		public void AddToStack()
		{
			throw new NotImplementedException();
		}

		internal void RemoveFromStack()
		{
			throw new NotImplementedException();
		}
	}
}