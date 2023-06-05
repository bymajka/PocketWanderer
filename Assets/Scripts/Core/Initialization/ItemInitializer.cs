using InventorySystem;
using ItemSystem.Generators;
using ItemSystem.ItemsData;
using UnityEngine;

namespace Core.Initialization
{
    public class ItemInitializer : MonoBehaviour
    {
        [SerializeField] private int _minNumberOfItems;
        [SerializeField] private int _maxNumberOfItems;
        [SerializeField] private ItemRandomList<ItemData> _itemRandomList;

        private void Start()
        {
            InitializeInventoryItems<ChestInventory>();
            InitializeInventoryItems<EnemyInventory>();
        }

        private void InitializeInventoryItems<T>() where T : Inventory
        {
            var inventories = FindObjectsOfType<T>();
            
            Debug.Log("Found + " + inventories.Length + " " + typeof(T) + " inventories.");

            foreach (var inventory in inventories)
            {
                var itemCount = Random.Range(_minNumberOfItems, _maxNumberOfItems);
                for (var i = 0; i < itemCount; i++)
                {
                    inventory.Add(_itemRandomList.GetRandom());
                }
            }
        }

    }
}