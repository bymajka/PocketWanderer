using Entity.Behaviour;
using InventorySystem;
using ItemSystem.ItemsData;
using UnityEngine;

namespace ItemSystem.Items
{
	public class ArmorItem : InventoryItem
	{
		public ArmorItem(ArmorData itemData) : base(itemData)
		{
		}

		public override void Use()
		{
			GameObject player = PlayerManager.Instance.PlayerObject;

			if (player == null)
				return;

			var playerBehaviour = player.GetComponent<PlayerEntityBehaviour>();
			var playerStats = playerBehaviour.Stats;

			ArmorData armorItem = (ArmorData) ItemData;
			
			if (playerStats.Armor <= 0 &&
				armorItem.ArmorRating <= 0)
			{
				return;
			}

			playerStats.Armor += armorItem.ArmorRating;
			PlayerManager.Instance.PlayerObject.GetComponent<PlayerInventory>().Remove(armorItem);
		}
	}
}