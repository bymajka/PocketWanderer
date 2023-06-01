using System;
using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;

        private void Start()
        {
            var parent = GetComponentInParent<InventorySlot>();
            try
            {
                var parentDictionary = parent.InventoryItem.ItemData.ItemActions;
                GetComponent<Button>().onClick.AddListener((() => AddButtons(parentDictionary)));
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public void AddButtons(Dictionary<string, Action> dictionary)
        {
            RemoveOldButtons();
            foreach (var pair in dictionary)
            {
                GameObject button = Instantiate(buttonPrefab, transform);
                button.GetComponent<Button>().onClick.AddListener(() => pair.Value());
                button.GetComponentInChildren<TMP_Text>().text = pair.Key;
            }
        }

        /*public void Togle(bool val)
        {
            if (val)
                RemoveOldButtons();
            gameObject.SetActive(val);
        }*/

        public void RemoveOldButtons()
        {
            try
            {
                var parent = GetComponentInParent<InventorySlot>().GetComponentInParent<PlayerInventoryManager>()
                    .InventorySlots;
                foreach (var inventorySlot in parent)
                {
                    foreach (Transform transformChildObjects in inventorySlot.GetComponentInChildren<ItemActionPanel>().transform)
                        Destroy(transformChildObjects.gameObject);
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}
