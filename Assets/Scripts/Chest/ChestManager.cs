using System.Collections.Generic;
using InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Chest
{
    public class ChestManager : MonoBehaviour
    {
        private List<ChestInventory> nearestChestInventories;

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