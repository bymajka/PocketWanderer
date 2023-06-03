using System;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem.ItemsData
{
    public abstract class ItemData : ScriptableObject
    {
        /// <summary>
        /// The name that is displayed to the player in the game interface.
        /// </summary>
        public string DisplayName;

        /// <summary>
        /// The image used to represent this item in the game interface.
        /// </summary>
        public Sprite Icon;

        /// <summary>
        /// The maximum number of identical items that can be held in a single inventory slot.
        /// </summary>
        public int StackCapacity;

        /// <summary>
        /// The rarity of the item, which affects its value and properties.
        /// </summary>
        public ItemRarity Rarity;

        /// <summary>
        /// The quantity of this item, typically used to determine the amount a player will receive when collecting the item.
        /// </summary>
        public float Amount;

        /// <summary>
        /// The price of the item in monetary equivalent.
        /// </summary>
        public float Price;

        /// <summary>
        /// Actions that has item.
        /// </summary>
        public Dictionary<string, Action> ItemActions;
    }
}