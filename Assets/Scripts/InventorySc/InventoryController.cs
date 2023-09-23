using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryPage InventoryUI;
        [SerializeField] private InventorySO inventoryData;

        public List<InventoryItemSO> initialItems = new List<InventoryItemSO>();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();

        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItemSO item in initialItems)
            {
                if (item.isEmpty)
                {
                    continue;
                }
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItemSO> inventoryState)
        {
            InventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                InventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }           
        }

        private void PrepareUI()
        {
            InventoryUI.InitializeInventoryUI(inventoryData.Size);
            InventoryUI.OnDescriptionRequested += HandleDescriptionRequested;
            InventoryUI.OnSwapItems += HandleSwapItems;
            InventoryUI.OnStartDragging += HandleDragging;
            InventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {

        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItemSO inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty) return;
            InventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
            InventoryUI.ResetCurrentItemIndex();
        }

        private void HandleDescriptionRequested(int itemIndex)
        {
            InventoryItemSO inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {
                InventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            InventoryUI.UpdateDescription(itemIndex, item.ItemImage,
                item.Name, item.description);
        }

        public void InvButton()
        {
            if (InventoryUI.isActiveAndEnabled == false)
            {
                InventoryUI.Show();
                foreach (var item in inventoryData.GetCurrentInventoryState())
                {
                    InventoryUI.UpdateData(item.Key,
                        item.Value.item.ItemImage,
                        item.Value.quantity);
                }
            }
            else InventoryUI.Hide();
        }

    }
}