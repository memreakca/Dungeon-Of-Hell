using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<InventoryItemSO> inventoryItems;
    [SerializeField] public int Size = 40;

    public void Initialize()
    {
        inventoryItems = new List<InventoryItemSO>();
        {
            for(int i = 0; i <Size; i++)
            {
                inventoryItems.Add(InventoryItemSO.GetEmptyItem());
            }
        }
    }

    public void AddItem(ItemSO item , int quantity)
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
            }
        }

    }

    public Dictionary<int,InventoryItemSO> GetCurrentInventoryState()
    {
        Dictionary<int,InventoryItemSO> returnValue = new Dictionary<int,InventoryItemSO>();
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
