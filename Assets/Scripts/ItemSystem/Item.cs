using UnityEngine;

namespace ItemSystem
{
    public class Item : MonoBehaviour, ICollectibleItem
    {
        public static event HandleItemCollected OnItemCollected;
        public delegate void HandleItemCollected(ItemData itemData);

        [SerializeField] private ItemData _itemData;
        
        public void Collect()
        {
            Destroy(gameObject);
            OnItemCollected?.Invoke(_itemData);
        }
    }
}