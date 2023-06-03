using ItemSystem.ItemsData;
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
            Debug.Log("+" + PotionData.EffectValue + "HP");
        }
    }
}