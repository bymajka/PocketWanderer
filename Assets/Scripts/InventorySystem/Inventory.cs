using System.Collections.Generic;
using ItemSystem.ItemsData;
using UnityEngine;

namespace InventorySystem
{
	public abstract class Inventory<T> : MonoBehaviour
	{
		public abstract List<T> Items { get; set; }
		[field: SerializeField] public int Capacity { get; private set; }
		
		public abstract void Add(ItemData itemData);

		public abstract void Remove(ItemData itemData);
	}
}