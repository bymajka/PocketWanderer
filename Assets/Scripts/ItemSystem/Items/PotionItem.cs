using System.Reflection;
using Entity.Behaviour;
using ItemSystem.ItemsData;
using UnityEngine;

namespace ItemSystem.Items
{
    public abstract class PotionItem : InventoryItem
    {
        protected readonly PotionData _potionData;

        protected PotionItem(PotionData itemData) : base(itemData)
        {
		}

		protected void ModifyPlayerStats(string propertyName)
		{
			GameObject player = PlayerManager.Instance.PlayerObject;

			if (player == null)
				return;

			var playerBehaviour = player.GetComponent<PlayerEntityBehaviour>();
			var playerStats = playerBehaviour.Stats;

			PropertyInfo property = playerStats.GetType().GetProperty(propertyName);
			if (property != null && 
				property.PropertyType == typeof(int))
			{
				if (_potionData.EffectValue <= 0 &&
					(int)property.GetValue(playerStats) <= 0)
				{
					return;
				}

				int currentValue = (int)property.GetValue(playerStats);
				property.SetValue(playerStats, currentValue + _potionData.EffectValue);
			}
		}
	}
}