using Entity.Behaviour;
using ItemSystem.ItemsData;

namespace ItemSystem.Items
{
	public class ArmorItem : InventoryItem
	{
		public ArmorItem(ArmorData itemData) : base(itemData)
		{
		}

		public override void Use()
		{
			var player = PlayerManager.Instance.PlayerObject;

			if (player == null)
				return;

			var playerBehaviour = player.GetComponent<PlayerEntityBehaviour>();
			var playerStats = playerBehaviour.Stats;

			var armorItem = (ArmorData) ItemData;
			
			if (playerStats.Armor <= 0 &&
				armorItem.ArmorRating <= 0)
			{
				return;
			}

			playerStats.Armor += armorItem.ArmorRating;
			RemoveItemFromInventory(armorItem);
		}
	}
}