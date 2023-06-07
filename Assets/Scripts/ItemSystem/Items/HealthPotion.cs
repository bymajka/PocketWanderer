using InventorySystem;
using ItemSystem.ItemsData;
using UnityEngine;

namespace ItemSystem.Items
{
    public class HealthPotion : PotionItem
    {
        public HealthPotion(PotionData itemData) : base(itemData)
        {
        }

        public override void Use()
        {
	        Debug.Log("Use from" + GetType());
			ModifyPlayerStats("HitPoints");
			PlayerManager.Instance.PlayerObject.GetComponent<PlayerInventory>().Remove(PotionData);
        }
	}
}