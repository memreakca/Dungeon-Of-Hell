using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class InventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler,
         IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField] Image itemImage;
        [SerializeField] TMP_Text quantity;
        [SerializeField] public Image borderImage;
        

        public event Action<InventoryItem> OnItemClicked, OnItemDroppedOn,
            OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

        private bool empty = true;

        private void Awake()
        {
            ResetData();
            Deselect();
        }

        public void ResetData()
        {
            itemImage.gameObject.SetActive(false);
            empty = true;
        }
        public void Deselect()
        {
            borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = sprite;
            this.quantity.text = quantity.ToString();
            empty = false;
        }

        public void Select()
        {
            borderImage.enabled = true;
        }


        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Right)
            {
                OnItemEndDrag?.Invoke(this);
            }
            
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

            if (empty)
                return;
            if (eventData.button != PointerEventData.InputButton.Right)
            {
                OnItemBeginDrag?.Invoke(this);
            }
            
        }

        public void OnDrag(PointerEventData eventData)
        {
           
        }
    }
}