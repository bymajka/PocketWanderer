using System;
using ItemSystem.ItemsData;
using UnityEngine;

namespace ItemSystem
{
    public class Item : MonoBehaviour, ICollectibleItem
    {
        public static event HandleItemCollected OnItemCollected;
        public delegate void HandleItemCollected(ItemData itemData);

        [field: SerializeField] public ItemData ItemData { get; set; }

        public void Collect()
        {
            Destroy(gameObject);
            OnItemCollected?.Invoke(ItemData);
        }
    }
}