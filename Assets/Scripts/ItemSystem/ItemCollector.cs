using UnityEngine;

namespace ItemSystem
{
    public class ItemCollector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var collectibleItemItem = collision.GetComponent<ICollectibleItem>();
            collectibleItemItem?.Collect();
        }
    }
}