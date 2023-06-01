using UnityEngine;
using UnityEngine.Serialization;

namespace ItemSystem.ItemsData
{
    public abstract class ItemData : ScriptableObject
    {
        /// <summary>
        /// The name that is displayed to the player in the game interface.
        /// </summary>
        [FormerlySerializedAs("DisplayName")]
        public string displayName;

        /// <summary>
        /// The image used to represent this item in the game interface.
        /// </summary>
        [FormerlySerializedAs("Icon")]
        public Sprite icon;

        /// <summary>
        /// The maximum number of identical items that can be held in a single inventory slot.
        /// </summary>
        [FormerlySerializedAs("StackCapacity")]
        public int stackCapacity;

        /// <summary>
        /// The rarity of the item, which affects its value and properties.
        /// </summary>
        [FormerlySerializedAs("Rarity")]
        public ItemRarity rarity;

        /// <summary>
        /// The quantity of this item, typically used to determine the amount a player will receive when collecting the item.
        /// </summary>
        [FormerlySerializedAs("Amount")]
        public float amount;

        /// <summary>
        /// The price of the item in monetary equivalent.
        /// </summary>
        [FormerlySerializedAs("Price")]
        public float price;
    }
}