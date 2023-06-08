using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Chest
{
    public class ChestManager : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        private List<Chest> _nearestChests = new List<Chest>();

        private void Awake()
        {
            playerInput.actions["Interaction"].performed += OnInteraction;
        }

        public void AddChest(Chest chest)
        {
            if (chest != null)
                _nearestChests.Add(chest);
        }
        
        public void RemoveChest(Chest chest)
        {
            if (chest != null)
                _nearestChests.Remove(chest);
        }

        public void OnInteraction(InputAction.CallbackContext callbackContext)
        {
            var chestToOpen = _nearestChests.FirstOrDefault();
            if (chestToOpen != null)
            {
                chestToOpen.OpenChest();
            }
        }
    }
}