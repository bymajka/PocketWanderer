using ItemSystem.ItemsData;
using UnityEngine;

namespace ItemSystem.Items
{
	public class SpeedPotion : PotionItem
	{
		public SpeedPotion(PotionData itemData) : base(itemData)
		{
		}

		public override void Use()
		{
			Debug.Log("Use from" + GetType());
			ModifyPlayerStats("MovementSpeed");
			RemoveItemFromInventory(PotionData);
		}
	}
}