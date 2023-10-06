using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Model
{
    public abstract class ItemSO : ScriptableObject
    {
        [SerializeField] public bool isStackable;

        public int id => GetInstanceID();
        
        [SerializeField] public int MaxStackSize;
        [SerializeField] public string Name;
        [field: SerializeField][field: TextArea] public string description;
        [SerializeField] public Sprite ItemImage;
    }
}

