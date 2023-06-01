using System;
using ItemSystem.ItemsData;
using UnityEngine;

namespace ItemSystem
{
    public class Item : MonoBehaviour, ICollectibleItem
    {
        public static event HandleItemCollected OnItemCollected;
        public delegate void HandleItemCollected(ItemData itemData);

        [SerializeField] protected ItemData _itemData;
        
        public void Collect()
        {
            Destroy(gameObject);
            OnItemCollected?.Invoke(_itemData);
        }
    }
}