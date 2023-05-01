using InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Chest
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private ChestManager chestManager;
        [SerializeField] private ChestInventory inventory;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite chestOpenedSprite;
        [SerializeField] private Sprite chestClosedSprite;
        [SerializeField] private Button chestButton;

        private void Awake()
        {
            chestButton.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                ShowButton();
                chestManager.AddChestInventory(inventory);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                HideButton();
                CloseChest();
                chestManager.RemoveChestInventory(inventory);
            }
        }

        public void OpenChest()
        {
            spriteRenderer.sprite = chestOpenedSprite;
        }

        private void CloseChest()
        {
            spriteRenderer.sprite = chestClosedSprite;
        }
        
        private void HideButton()
        {
            chestButton.gameObject.SetActive(false);
        }

        private void ShowButton()
        {
            chestButton.gameObject.SetActive(true);
        }
    }
}
