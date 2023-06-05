using ItemSystem.ItemsData;
using PlayerSystem;
using UnityEngine;

namespace ItemSystem.Items
{
    public class HealthPotion : PotionItem
    {
        public HealthPotion(PotionData itemData) : base(itemData)
        {
        }

        protected override void Use()
        {
			ModifyPlayerStats("HealthPotion");
		}
	}
}