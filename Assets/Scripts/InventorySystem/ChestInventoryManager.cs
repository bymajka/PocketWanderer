namespace InventorySystem
{
    public class ChestInventoryManager : InventoryManager<ChestInventory>
    {
        public ChestInventoryManager(ChestInventory inventory) : base(inventory)
        {
        }
    }
}