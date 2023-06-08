using Entity.Behaviour;
using InventorySystem;
using ItemSystem.ItemsData;

namespace ItemSystem.Items
{
	public class WeaponItem : InventoryItem
	{
		public WeaponItem(WeaponData itemData) : base(itemData)
		{
		}

		public override void Use()
		{
			var player = PlayerManager.Instance.PlayerObject;

			if (player == null)
				return;

			var playerBehaviour = player.GetComponent<PlayerEntityBehaviour>();
			var playerStats = playerBehaviour.Stats;

			var weaponData = (WeaponData) ItemData;
			
			if (playerStats.Armor <= 0 &&
				weaponData.Damage <= 0)
			{
				return;
			}

			playerStats.WeaponDamage += weaponData.Damage;
			PlayerManager.Instance.PlayerObject.GetComponent<PlayerInventory>().Remove(weaponData);
		}
	}
}