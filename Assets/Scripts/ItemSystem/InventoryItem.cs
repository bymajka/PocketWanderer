using ItemSystem.ItemsData;

namespace ItemSystem
{
	public class InventoryItem
	{
		public int StackSize { get; set; }
		public ItemData ItemData { get; }

		public InventoryItem(ItemData itemData)
		{
			ItemData = itemData;
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