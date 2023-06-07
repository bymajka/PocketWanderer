using ItemSystem.ItemsData;

namespace ItemSystem
{
	public abstract class InventoryItem
	{
		public int StackSize { get; set; }
		public ItemData ItemData { get; set; }

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

		public virtual void Use()
		{
			
		}
	}
}