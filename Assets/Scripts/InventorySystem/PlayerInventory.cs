using ItemSystem;

namespace InventorySystem
{
	public class PlayerInventory : Inventory
	{
		private void OnEnable()
		{
			Item.OnItemCollected += Add;
			InventoryManager.OnRemoveItem += Remove;
		}

		private void OnDisable()
		{
			Item.OnItemCollected -= Add;
			InventoryManager.OnRemoveItem -= Remove;
		}
	}
}