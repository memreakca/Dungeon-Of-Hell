using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDesc : MonoBehaviour
{

    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text titletext;
    [SerializeField] private TMP_Text descriptiontext;

    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.itemImage.gameObject.SetActive(false);
        this.titletext.text = "";
        this.descriptiontext.text = "";
    }

    public void SetDescription(Sprite sprite , string itemName,
        string itemDescription)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.titletext.text = itemName;
        this.descriptiontext.text = itemDescription;
    }
    

    
}
