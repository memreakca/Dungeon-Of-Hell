using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    [SerializeField] private bool isStackable;

    public int id => GetInstanceID();

    [SerializeField] public int MaxStackSize;
    [SerializeField] public string Name;
    [field: SerializeField][field: TextArea] public string description;
    [SerializeField] public Sprite ItemImage;
}
