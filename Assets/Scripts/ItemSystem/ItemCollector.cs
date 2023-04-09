using UnityEngine;

namespace ItemSystem
{
    public class ItemCollector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            ICollectibleItem collectibleItemItem = collision.GetComponent<ICollectibleItem>();
            if (collectibleItemItem != null)
            {
                collectibleItemItem.Collect();
            }
        }
    }
}