using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
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
            itemImage.gameObject.SetActive(false);
            titletext.text = "";
            descriptiontext.text = "";
        }

        public void SetDescription(Sprite sprite, string itemName,
            string itemDescription)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            titletext.text = itemName;
            descriptiontext.text = itemDescription;
        }



    }
}