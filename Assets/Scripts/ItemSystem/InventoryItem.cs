using System;
using System.Collections.Generic;
using InventorySystem;
using ItemSystem.ItemsData;
using UnityEngine;

namespace ItemSystem
{
	public class InventoryItem
	{ 
		public int StackSize { get; set; }
		public ItemData ItemData { get; set; }

		public InventoryItem(ItemData itemData) 
		{
			ItemData = itemData;
			itemData.ItemActions = new Dictionary<string, Action>();
			InventoryTransfer.SubscribeInventoryTransfering += AddAction(nameof(Transfer), );
			AddAction(nameof(Use), Use);
			AddToStack();
		}

		public void AddToStack()
		{
			StackSize++;
		}

		internal void RemoveFromStack()
		{
			StackSize--;
		}

		public void AddAction(string actionName, Action action)
		{
			ItemData.ItemActions.Add(actionName, action);
		}

		public void Use()
		{
			Debug.Log("UsingItem");
		}

		public void Transfer(Inventory inventoryCurrent, Inventory inventoryOther)
		{
			inventoryCurrent.TransferItem(ItemData, inventoryOther);
		}
	}
}