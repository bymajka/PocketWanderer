using ItemSystem.ItemsData;
using PlayerSystem;
using UnityEngine;

namespace ItemSystem.Items
{
	public class ArmorItem : InventoryItem
	{
		public readonly ArmorData _armorData;

		public ArmorItem(ArmorData itemData) : base(itemData)
		{
		}

		public void Use()
		{
			GameObject player = PlayerManager.Instance.PlayerObject;

			if (player == null)
				return;

			var playerBehaviour = player.GetComponent<PlayerBehaviour>();
			var playerStats = playerBehaviour.Stats;

			if (playerStats.Armor <= 0 &&
				_armorData.ArmorRating <= 0)
			{
				return;
			}

			playerStats.Armor += _armorData.ArmorRating;
		}
	}
}