using System;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryTransfer : MonoBehaviour
    {
        [SerializeField] private Inventory inventoryCurrent;
        [SerializeField] private Inventory inventoryOther;

        public static event Action<Inventory, Inventory> SubscribeInventoryTransfering;

        private void Awake()
        {
            SubscribeInventoryTransfering?.Invoke(inventoryCurrent, inventoryOther);
        }
    }
}