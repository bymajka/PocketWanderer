namespace InventorySystem
{
    public class PlayerInventoryManager : InventoryManager<PlayerInventory>
    {
        public PlayerInventoryManager(PlayerInventory inventory) : base(inventory)
        {
        }
    }
}