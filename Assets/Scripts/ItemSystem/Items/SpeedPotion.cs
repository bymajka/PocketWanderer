using ItemSystem.ItemsData;
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
            Debug.Log("+" + PotionData.effectValue + "MS");
        }
    }
}