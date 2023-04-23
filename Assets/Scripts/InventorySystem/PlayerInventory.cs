using ItemSystem;

namespace InventorySystem
{
	public class PlayerInventory : Inventory
	{
		private void OnEnable()
		{
			Item.OnItemCollected += Add;
		}

		private void OnDisable()
		{
			Item.OnItemCollected -= Add;
		}
	}
}