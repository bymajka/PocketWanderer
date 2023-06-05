using ItemSystem.ItemsData;
using PlayerSystem;
using UnityEngine;

namespace ItemSystem.Items
{
	public class SpeedPotion : PotionItem
	{
		public SpeedPotion(PotionData itemData) : base(itemData)
		{
		}

		protected override void Use()
		{
			ModifyPlayerStats("SpeedPotion");
		}
	}
}