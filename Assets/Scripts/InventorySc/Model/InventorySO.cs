using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
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

        public int AddItem(ItemSO item, int quantity)
        {
            if( item.isStackable == false)
            {

                for (int i = 0; i < inventoryItems.Count; i++)
                {
                    while(quantity > 0 && IsInventoryFull() == false)
                    {
                        quantity -= AddItemToFirstFreeSlot(item , 1);

                    }
                    InformAboutChange();
                    return quantity;
                    
                }
            }

            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            return quantity;

        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity)
        {
            InventoryItemSO newItem = new InventoryItemSO
            {
                item = item,
                quantity = quantity
            };

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty)
                {
                    inventoryItems[i] = newItem;
                    return quantity;
                }
            }
            return 0;
        }

        private bool IsInventoryFull()
            => inventoryItems.Where(item => item.isEmpty).Any() == false;

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].isEmpty) continue;

                if (inventoryItems[i].item.id == item.id)
                {
                    int amountPossibleToTake = inventoryItems[i].item.MaxStackSize - inventoryItems[i].quantity;

                    if(quantity > amountPossibleToTake)
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        inventoryItems[i] = inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && IsInventoryFull() == false)
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }
            return quantity;
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
