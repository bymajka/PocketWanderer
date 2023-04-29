using InventorySystem;
using UnityEngine;

namespace Chest
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private ChestManager chestManager;
        [SerializeField] private ChestInventory inventory;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                chestManager.ShowButton();
                chestManager.AddChestInventory(inventory);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                chestManager.HideButton();
                chestManager.RemoveChestInventory(inventory);
                if (chestManager.gameObject.activeSelf)
                    chestManager.ShowChestInventory();
            }
        }
    }
}
