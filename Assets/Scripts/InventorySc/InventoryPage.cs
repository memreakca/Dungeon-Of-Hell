using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private InventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;

    List<InventoryItem> items = new List<InventoryItem>();

    public void InitializeInventoryUI(int inventorysize)
    {
        for(int i = 0; i < inventorysize; i++)
        {
            InventoryItem uiitem = Instantiate(itemPrefab, Vector3.zero ,Quaternion.identity);
            uiitem.transform.SetParent(contentPanel);
            items.Add(uiitem);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
