using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="InventoryItem")]
public class InventoryItem : ScriptableObject
{
public string itemName;
public Sprite icon;
// public ItemType type;
public bool stackable = true;
public short stackLimit = 64;
public itemID itemID;
}
public enum ItemType
{
    WEAPON, 
    PIN,
    CONSUMABLE
}
//todo make some type of policy to make these comply 
public enum itemID
{
    SCYTHE,
    RUSTYSWORD,    
    APPLE,
    SK8,

    
}