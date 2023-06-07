using ItemSystem.ItemsData;

namespace ItemSystem.Items
{
	public class SpeedPotion : PotionItem
	{
		public SpeedPotion(PotionData itemData) : base(itemData)
		{
		}

		protected override void Use()
		{
			ModifyPlayerStats("MovementSpeed");
		}
	}
}