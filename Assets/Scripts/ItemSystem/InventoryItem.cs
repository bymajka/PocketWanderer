using InventorySystem;
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

		public void RemoveFromStack()
		{
			StackSize--;
		}

		public virtual void Use()
		{
		}

		protected void RemoveItemFromInventory(ItemData itemData)
		{
			PlayerManager
				.Instance
				.PlayerObject.GetComponent<PlayerInventory>()
				.Remove(itemData);
		}
	}
}