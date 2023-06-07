using ItemSystem.ItemsData;
using UnityEngine;

namespace ItemSystem.Items
{
    public class ManaPotion : PotionItem
    {
        public ManaPotion(PotionData itemData) : base(itemData)
        {
        }

        protected override void Use()
        {
            Debug.Log("Use from" + GetType());
            ModifyPlayerStats("ManaPoints");
        }
    }
}