using ItemSystem.ItemsData;

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