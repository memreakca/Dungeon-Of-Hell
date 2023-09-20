using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    [SerializeField] private bool isStackable;

    public int id => GetInstanceID();

    [SerializeField] int MaxStackSize;
    [SerializeField] string Name;
    [field: SerializeField][field: TextArea] string description;
    [SerializeField] Sprite ItemImage;
}
