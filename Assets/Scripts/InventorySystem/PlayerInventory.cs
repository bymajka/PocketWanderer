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
			var inventoryItem = Items.FirstOrDefault(i => i.ItemData == itemData && i.StackSize < itemData.StackCapacity);

			if (inventoryItem != null)
			{
				inventoryItem.AddToStack();
				OnInventoryChangedPlayer?.Invoke(inventoryItem);
				return;
			}

			InventoryItem newItem = null;
			var type = itemData.GetType();
			if (type == typeof(PotionData))
			{
				var potion = (PotionData)itemData;
				newItem = potion.PotionEffect switch
				{
					PotionEffect.ManaRegeneration => new ManaPotion(potion),
					PotionEffect.SpeedBoost => new SpeedPotion(potion),
					PotionEffect.PowerBoost => new PowerPotion(potion),
					PotionEffect.HealthBoost => new HealthPotion(potion)
				};
			}
			else if (type == typeof(ArmorData))
			{
				var armorData = (ArmorData)itemData;
				newItem = new ArmorItem(armorData);
			}
			else if (type == typeof(WeaponData))
			{
				var weaponData = (WeaponData)itemData;
				newItem = new WeaponItem(weaponData);
			}
			else if (itemData.GetType() == typeof(JewelryData))
			{
				var player = PlayerManager.Instance.GameObject()
					.GetComponent<PlayerEntityBehaviour>();
				player.Stats.Gold += (int)itemData.Price;
				return;
			}

			Items.Add(newItem);
			OnInventoryChangedPlayer?.Invoke(newItem);
		}
		public override void Remove(ItemData itemData)
		{
			var inventoryItem = Items
				.LastOrDefault(i => i.ItemData == itemData && i.StackSize > 0);

			if (inventoryItem == null)
				return;
			
			inventoryItem.RemoveFromStack();
			OnInventoryChangedPlayer?.Invoke(inventoryItem);

			if (inventoryItem.StackSize != 0)
				return;

			Items.Remove(inventoryItem);
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