using UnityEngine;
using UnityEngine.Serialization;

namespace ItemSystem.ItemsData
{
    public abstract class ItemData : ScriptableObject
    {
        // The name that is displayed to the player in the game interface.
        [FormerlySerializedAs("DisplayName")]
        public string displayName;

        // The image used to represent this item in the game interface.
        [FormerlySerializedAs("Icon")]
        public Sprite icon;

        // The maximum number of identical items that can be held in a single inventory slot.
        [FormerlySerializedAs("StackCapacity")]
        public int stackCapacity;

        // The rarity of the item, which affects its value and properties.
        [FormerlySerializedAs("Rarity")]
        public ItemRarity rarity;

        // The quantity of this item, typically used to determine the amount a player will receive when collecting the item.
        [FormerlySerializedAs("Amount")]
        public float amount;

        // The price of the item in monetary equivalent.
        [FormerlySerializedAs("Price")]
        public float price;
    }
}