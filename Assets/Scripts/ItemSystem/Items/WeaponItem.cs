using Entity.Behaviour;
using ItemSystem.ItemsData;
using UnityEngine;

namespace ItemSystem.Items
{
	public class WeaponItem : InventoryItem
	{
		public readonly WeaponData _weaponData;

		public WeaponItem(WeaponData itemData) : base(itemData)
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
				_weaponData.Damage <= 0)
			{
				return;
			}

			playerStats.Damage += _weaponData.Damage;
			base.Use();
		}
	}
}