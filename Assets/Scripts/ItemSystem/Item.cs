using UnityEngine;
using InventorySystem;

namespace ItemSystem
{
    public class Item : MonoBehaviour, ICollectible
    {
        public static event HandleItemCollected OnItemCollected;
        public delegate void HandleItemCollected(ItemData itemData);

        public ItemData ItemData;
        
        public void Collect()
        {
            Destroy(gameObject);
            OnItemCollected?.Invoke(ItemData);
        }
    }
}