using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        [SerializeField] private List<InventoryItemSO> inventoryItems;
        [SerializeField] public int Size = 40;

        public event Action<Dictionary<int, InventoryItemSO>> OnInventoryUpdated;
        public void Initialize()
        {
            inventoryItems = new List<InventoryItemSO>();
            {
                for (int i = 0; i < Size; i++)
                {
                    inventoryItems.Add(InventoryItemSO.GetEmptyItem());
                }
            }
        }

        public void AddItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty)
                {
                    inventoryItems[i] = new InventoryItemSO
                    {
                        item = item,
                        quantity = quantity
                    };
                    return;
                }
            }

        }

        public Dictionary<int, InventoryItemSO> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItemSO> returnValue = new Dictionary<int, InventoryItemSO>();
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty)
                {
                    continue;
                }
                returnValue[i] = inventoryItems[i];
            }
            return returnValue;
        }

        public InventoryItemSO GetItemAt(int itemIndex)
        {
            return inventoryItems[itemIndex];
        }

        public void AddItem(InventoryItemSO item)
        {
            AddItem(item.item, item.quantity);
        }

        public void SwapItems(int itemIndex_1, int itemIndex_2)
        {
            InventoryItemSO item1 = inventoryItems[itemIndex_1];
            inventoryItems[itemIndex_1] = inventoryItems[itemIndex_2];
            inventoryItems[itemIndex_2] = item1;
            InformAboutChange();
            
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
        }
    }

    [Serializable]

    public struct InventoryItemSO
    {

        public int quantity;
        public ItemSO item;

        public bool isEmpty => item == null;

        public InventoryItemSO ChangeQuantity(int newQuantity)
        {
            return new InventoryItemSO
            {
                item = this.item,
                quantity = newQuantity,
            };
        }

        public static InventoryItemSO GetEmptyItem()
            => new InventoryItemSO
            {
                item = null,
                quantity = 0,
            };
    }

}
