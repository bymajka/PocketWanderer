using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Chest
{
    public class ChestManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        private List<Chest> nearestChests = new List<Chest>();

        private void Awake()
        {
            playerInput.actions["Interaction"].performed += OnInteraction;
        }

        public void AddChest(Chest chest)
        {
            if (chest != null)
                nearestChests.Add(chest);
        }
        
        public void RemoveChest(Chest chest)
        {
            if (chest != null)
                nearestChests.Remove(chest);
        }

        public void OnInteraction(InputAction.CallbackContext callbackContext)
        {
            var chestToOpen = nearestChests.FirstOrDefault();
            if (chestToOpen != null)
            {
                chestToOpen.OpenChest();
            }
        }

        public void ShowChestInventory(ChestInventory chestInventory)
        {
            Debug.Log("Showing chest inventory");
        }
    }
}