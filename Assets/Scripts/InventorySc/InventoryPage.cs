using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
using UnityEngine.UI;

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private InventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private InventoryDesc itemDescription;

    List<InventoryItem> items = new List<InventoryItem>();

    public Sprite image;
    public int quantity;
    public string title ,description;

    private void Awake()
    {
        itemDescription.ResetDescription();      
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

    private void HandleEndDrag(InventoryItem obj)
    {
      
    }

    private void HandleShowItemActions(InventoryItem obj)
    {
        
    }

    private void HandleBeginDrag(InventoryItem obj)
    {
       
    }

    private void HandleSwap(InventoryItem obj)
    {
        
    }

    private void HandleItemSelection(InventoryItem obj)
    {
        itemDescription.SetDescription(image, title, description);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        items[2].SetData(image, quantity);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
