using InventorySystem;
using ItemSystem;
using ItemSystem.ItemsData;
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
        [SerializeField] private float dropItemsRange;
        
        [SerializeField] private ItemData prefabItem1;
        [SerializeField] private ItemData prefabItem2;
        [SerializeField] private ItemData prefabItem3;

        private void Awake()
        {
            chestButton.gameObject.SetActive(false);
            inventory = gameObject.AddComponent<ChestInventory>();
            inventory.Add(prefabItem1);
            inventory.Add(prefabItem2);
            inventory.Add(prefabItem3);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                ShowButton();
                chestManager.AddChest(this);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                HideButton();
                chestManager.RemoveChest(this);
            }
        }

        public void OpenChest()
        {
            spriteRenderer.sprite = chestOpenedSprite;
            DropItems();
            GetComponent<Collider2D>().enabled = false;
        }

        private void DropItems()
        {
            foreach (var item in inventory.Items)
            {
                GameObject newItem = new GameObject();
                newItem.AddComponent<Item>().ItemData = item.ItemData;
                newItem.AddComponent<SpriteRenderer>().sprite = item.ItemData.Icon;
                newItem.GetComponent<SpriteRenderer>().sortingOrder = 1;
                newItem.AddComponent<CircleCollider2D>().isTrigger = true;
                newItem.transform.localScale *= 1.5f;
                newItem.transform.position = new Vector3(transform.position.x + Random.Range(-dropItemsRange, dropItemsRange), 
                    transform.position.y + Random.Range(-dropItemsRange, dropItemsRange), transform.position.z);
            }
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
