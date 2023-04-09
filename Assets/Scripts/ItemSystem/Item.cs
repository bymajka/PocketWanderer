using UnityEngine;
using InventorySystem;

namespace ItemSystem
{
    public class Item : MonoBehaviour, ICollectible
    {
        public static event HandleItemCollected OnItemCollected;
        public delegate void HandleItemCollected(ItemData itemData);

        private ItemData _itemData;
        
        public void Collect()
        {
            Destroy(gameObject);
            OnItemCollected?.Invoke(_itemData);
        }
    }
}