using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="InventoryItem")]
public class InventoryItem : ScriptableObject
{
public string itemName;
public Sprite icon;
public ItemType type;
public bool stackable = false;
public short stackLimit = 1;
public static void LoadFromID(int id)
{

}
public enum ItemType
{
    WEAPON, 
    PIN,
    CONSUMABLE
}}
