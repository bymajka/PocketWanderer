using ItemSystem.ItemsData;

namespace ItemSystem.Items
{
	public class SpeedPotion : PotionItem
	{
		public SpeedPotion(PotionData itemData) : base(itemData)
		{
		}

		public override void Use()
		{
			ModifyPlayerStats("MovementSpeed");
			base.Use();
		}
	}
}