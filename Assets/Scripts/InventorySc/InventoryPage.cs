using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private InventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private InventoryDesc itemDescription;
    [SerializeField] private MouseFollower mouseFollower;

    List<InventoryItem> items = new List<InventoryItem>();

    public event Action<int> OnDescriptionRequested,
        OnItemActionRequested,
        OnStartDragging;
    public event Action<int, int> OnSwapItems;
     

    private int currentlyDraggedItemIndex = -1;

    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
        mouseFollower.Toggle(false);
    }
    public void InitializeInventoryUI(int inventorysize)
    {
        for(int i = 0; i < inventorysize; i++)
        {
            InventoryItem uiitem = Instantiate(itemPrefab, Vector3.zero ,Quaternion.identity);
            uiitem.transform.SetParent(contentPanel);
            items.Add(uiitem);
            uiitem.OnItemClicked += HandleItemSelection;
            uiitem.OnItemBeginDrag += HandleBeginDrag;
            uiitem.OnItemDroppedOn += HandleSwap;
            uiitem.OnItemEndDrag += HandleEndDrag;
            uiitem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }
     public void UpdateData(int itemIndex,Sprite itemImage,int itemQuantity)
    {
        if(items.Count > itemIndex)
        {
            items[itemIndex].SetData(itemImage, itemQuantity);
        }
    }
    private void HandleShowItemActions(InventoryItem InventoryItemUI)
    {

    }

    private void HandleBeginDrag(InventoryItem InventoryItemUI)
    {
        int index = items.IndexOf(InventoryItemUI);
        if(index == -1)
            return;
        currentlyDraggedItemIndex = index;
        HandleItemSelection(InventoryItemUI);
        OnStartDragging?.Invoke(index);

    }
    
    public void CreateDraggedItem(Sprite sprite , int quantity)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }
    private void HandleEndDrag(InventoryItem InventoryItemUI)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(InventoryItem InventoryItemUI)
    {
        int index = items.IndexOf(InventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
       
    }

    private void HandleItemSelection(InventoryItem InventoryItemUI)
    {
        int index = items.IndexOf(InventoryItemUI);
        if (index == -1) return;
        OnDescriptionRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
        DeselectAllItems();
    }

    private void DeselectAllItems()
    {
        foreach(InventoryItem item in items)
        {
            item.Deselect();
        }
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    private void ResetDraggedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        itemDescription.SetDescription(itemImage, name, description);
        DeselectAllItems();
        items[itemIndex].Select();
    }
}
