using System;

namespace ItemSystem
{
	public class InventoryItem
	{
		public int StackSize { get; set; }
		private ItemData _itemData;

		public InventoryItem(ItemData itemData)
		{
			_itemData = itemData;
			AddToStack();
		}

		public void AddToStack()
		{
			StackSize++;
		}

		internal void RemoveFromStack()
		{
			StackSize--;
		}
	}
}