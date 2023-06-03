using System.Collections.Generic;

namespace InventorySystem
{
    public class PlayerInventoryManager : InventoryManager<PlayerInventory>
    {
        private void Awake()
        {
            InventorySlots = new List<InventorySlot>(inventory.Capacity);
            CreateEmptyInventory();
            PlayerInventory.OnInventoryChangedPlayer += UpdateSlot;
        }
        
        private void OnDestroy()
        {
            PlayerInventory.OnInventoryChangedPlayer -= UpdateSlot;
        }
        
        public PlayerInventoryManager(PlayerInventory inventory) : base(inventory)
        {
        }
    }
}