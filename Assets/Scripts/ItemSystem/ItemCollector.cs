using UnityEngine;

namespace ItemSystem
{
    public class ItemCollector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            ICollectible collectible = collision.GetComponent<ICollectible>();
            if (collectible != null)
            {
                collectible.Collect();
            }
        }
    }
}