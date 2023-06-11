﻿using ItemSystem.ItemsData;
using UnityEngine;

namespace ItemSystem.Items
{
	public class PowerPotion : PotionItem
	{
		public PowerPotion(PotionData itemData) : base(itemData)
		{
		}

		public override void Use()
		{
			Debug.Log("Use from" + GetType());
			ModifyPlayerStats("Damage");
			RemoveItemFromInventory(PotionData);
		}
	}
}