using Entity.Behaviour;
using ItemSystem.ItemsData;
using UnityEngine;

namespace ItemSystem.Items
{
	public class ArmorItem : InventoryItem
	{
		public readonly ArmorData _armorData;

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

			if (playerStats.Armor <= 0 &&
				_armorData.ArmorRating <= 0)
			{
				return;
			}

			playerStats.Armor += _armorData.ArmorRating;
			base.Use();
		}
	}
}