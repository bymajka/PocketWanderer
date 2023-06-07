using ItemSystem.ItemsData;
using UnityEngine;

namespace ItemSystem.Items
{
    public class ManaPotion : PotionItem
    {
        public ManaPotion(PotionData itemData) : base(itemData)
        {
        }

        public override void Use()
        {
            Debug.Log("Use from" + GetType());
            ModifyPlayerStats("ManaPoints");
            base.Use();
        }
    }
}