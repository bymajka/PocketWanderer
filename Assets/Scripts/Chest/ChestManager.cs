using System;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Chest
{
    public class ChestManager : MonoBehaviour
    {
        [SerializeField] private Button chestButton;
        public static event Action<Inventory> SubscribeOnItemActions;
        private List<ChestInventory> nearestChestInventories = new ();

        private void Awake()
        {
            chestButton.gameObject.SetActive(false);
        }

        public void HideButton()
        {
            chestButton.gameObject.SetActive(false);
        }

        public void ShowButton()
        {
            chestButton.gameObject.SetActive(true);
        }

        public void AddChestInventory(ChestInventory inventory)
        {
            if (inventory != null)
                nearestChestInventories.Add(inventory);
            SubscribeOnItemActions?.Invoke(inventory);
        }
        
        public void RemoveChestInventory(ChestInventory inventory)
        {
            if (inventory != null)
                nearestChestInventories.Remove(inventory);
        }

        public void ShowChestInventory() => gameObject.SetActive(!gameObject.activeSelf);
    }
}