using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryPage InventoryUI;
    [SerializeField] public int inventorysize;
    private void Start()
    {
        InventoryUI.InitializeInventoryUI(inventorysize);
    }

    public void InvButton()
    {
        if (InventoryUI.isActiveAndEnabled == false)
        {
            InventoryUI.Show();
        }
        else InventoryUI.Hide();
    }

}
