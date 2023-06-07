using System;
using System.Collections.Generic;
using System.Linq;
using Entity.Behaviour;
using ItemSystem;
using ItemSystem.Items;
using ItemSystem.ItemsData;
using Unity.VisualScripting;

namespace InventorySystem
{
	public class PlayerInventory : Inventory<InventoryItem>
	{
		public static event Action<InventoryItem> OnInventoryChangedPlayer;
		public override List<InventoryItem> Items { get; set; } = new();

		public override void Add(ItemData itemData)
		{
			var inventoryItem = Items.Where(i => i.ItemData == itemData && i.StackSize < itemData.StackCapacity)
				.FirstOrDefault();

			if (inventoryItem != null)
			{
				inventoryItem.AddToStack();
				OnInventoryChangedPlayer?.Invoke(inventoryItem);
			}
			else
			{
				InventoryItem newItem = null;
				var t = itemData.GetType();
				if (t == typeof(PotionData))
				{
					PotionData potion = (PotionData) itemData;
					switch (potion.PotionEffect)
					{
						case PotionEffect.ManaRegeneration:
							newItem = new ManaPotion(potion);
							break;
						case PotionEffect.HealthBoost:
							newItem = new HealthPotion(potion);
							break;
						case PotionEffect.SpeedBoost:
							newItem = new SpeedPotion(potion);
							break;
						default:
							newItem = new ManaPotion(potion);
							break;
					}
				}
				else if (t == typeof(ArmorData))
				{
					ArmorData armorData = (ArmorData) itemData;
					newItem = new ArmorItem(armorData);
				}
				else if (t == typeof(WeaponData))
				{
					WeaponData weaponData = (WeaponData) itemData;
					newItem = new WeaponItem(weaponData);
				}
				else if (itemData.GetType() == typeof(JewelryData))
				{
					var player = PlayerManager.Instance.GameObject()
						.GetComponent<PlayerEntityBehaviour>();
					player.Stats.gold += (int)itemData.Price;
				}
				Items.Add(newItem);
				OnInventoryChangedPlayer?.Invoke(newItem);
			}
		}
		public override void Remove(ItemData itemData)
		{
			var inventoryItem = Items
				.LastOrDefault(i => i.ItemData == itemData && i.StackSize > 0);

			if (inventoryItem != null)
			{
				inventoryItem.RemoveFromStack();
				OnInventoryChangedPlayer?.Invoke(inventoryItem);

				if (inventoryItem.StackSize != 0)
					return;

				Items.Remove(inventoryItem);
			}
		}

		private void OnEnable()
		{
			Item.OnItemCollected += Add;
			InventoryManager<PlayerInventory>.OnRemoveItem += Remove;
		}

		private void OnDisable()
		{
			Item.OnItemCollected -= Add;
			InventoryManager<PlayerInventory>.OnRemoveItem -= Remove;
		}
	}
}