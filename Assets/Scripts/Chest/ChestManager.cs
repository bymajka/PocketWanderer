﻿using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Chest
{
    public class ChestManager : MonoBehaviour
    {
        [SerializeField] private Button chestButton;
        private List<ChestInventory> nearestChestInventories;

        private void Awake()
        {
            chestButton.gameObject.SetActive(false);
            nearestChestInventories = new List<ChestInventory>();
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
        }
        
        public void RemoveChestInventory(ChestInventory inventory)
        {
            if (inventory != null)
                nearestChestInventories.Remove(inventory);
        }

        public void ShowChestInventory()
        {
            Debug.Log("Showing chest inventory");
        }
    }
}