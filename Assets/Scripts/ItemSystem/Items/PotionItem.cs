using Entity.Behaviour;
using ItemSystem.ItemsData;

namespace ItemSystem.Items
{
    public abstract class PotionItem : InventoryItem
    {
        protected PotionData PotionData;

        protected PotionItem(ItemData itemData) : base(itemData)
        {
        }

        protected void ModifyPlayerStats(string propertyName)
        {
            var player = PlayerManager.Instance.PlayerObject;
            
            if (player == null)
                return;

            var playerBehaviour = player.GetComponent<PlayerEntityBehaviour>();
            var playerStats = playerBehaviour.Stats;

            if (playerStats == null)
                return;

            var property = playerStats.GetType().GetProperty(propertyName);
            if (property == null || property.PropertyType != typeof(float))
                return;

            PotionData = ItemData as PotionData;
            if (PotionData == null)
                return;

            if (PotionData.EffectValue <= 0 && (float)property.GetValue(playerStats) <= 0)
                return;

            var currentValue = (float)property.GetValue(playerStats);

            switch (PotionData.PotionEffect)
            {
                case PotionEffect.HealthBoost:
                    if (currentValue + PotionData.EffectValue > playerStats.MaxHitPoints)
                        property.SetValue(playerStats, playerStats.MaxHitPoints);
                    else
                        property.SetValue(playerStats, currentValue + PotionData.EffectValue);
                    break;
                case PotionEffect.ManaRegeneration:
                    if (currentValue + PotionData.EffectValue > playerStats.MaxManaPool)
                        property.SetValue(playerStats, playerStats.MaxManaPool);
                    else
                        property.SetValue(playerStats, currentValue + PotionData.EffectValue);
                    break;
                case PotionEffect.SpeedBoost:
                case PotionEffect.PowerBoost:
                case PotionEffect.Invisibility:
                default:
                    property.SetValue(playerStats, currentValue + PotionData.EffectValue);
                    break;
            }
        }
    }
}
