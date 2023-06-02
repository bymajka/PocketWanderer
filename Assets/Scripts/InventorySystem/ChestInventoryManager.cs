using System.Collections.Generic;

namespace InventorySystem
{
    public class ChestInventoryManager : InventoryManager<ChestInventory>
    {
        private void Awake()
        {
            InventorySlots = new List<InventorySlot>(inventory.Capacity);
            CreateEmptyInventory();
            ChestInventory.OnInventoryChangedChest += UpdateSlot;
        }
        
        private void OnDestroy()
        {
            ChestInventory.OnInventoryChangedChest -= UpdateSlot;
        }
        public ChestInventoryManager(ChestInventory inventory) : base(inventory)
        {
        }
    }
}