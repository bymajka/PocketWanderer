using ItemSystem.ItemsData;

namespace ItemSystem.Items
{
    public abstract class PotionItem : InventoryItem
    {
        protected readonly PotionData PotionData;
        protected abstract void Use();

        protected PotionItem(PotionData itemData) : base(itemData)
        {
            PotionData = itemData;
            AddAction("Use", Use);
        }
    }
}