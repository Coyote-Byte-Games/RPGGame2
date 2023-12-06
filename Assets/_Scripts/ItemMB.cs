using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Scripting;
using static InventoryItem;
public abstract class ItemMB : MonoBehaviour
{
    public string itemName;
    public string description = "Rations. Might just be good for something else, though...";
    public int stacksHeld = 1;
    public Sprite icon;
    public InventoryItem itemBase;
    public event Action OnInventorySelected;
    public void InitItem()
    {
        this.icon = itemBase.icon;
    }
  
    public int GetID()
    {
        string key = "ItemID_"+itemName;
        // return (int)itemBase.itemID;
        //We want a file that contains item IDs.
        //Read playerprefs, seeing if this item name is held
        if (!PlayerPrefs.HasKey(key))
        {
            //doesnt exist, so get the capacity
            int newSlot = PlayerPrefs.GetInt("ItemIDNext", 0);
            //Set the key
            PlayerPrefs.SetInt(key, newSlot);
            //And update the index
            PlayerPrefs.SetInt("ItemIDNext", PlayerPrefs.GetInt("ItemIDNext", 0) + 1);
        }
        return PlayerPrefs.GetInt(key);
        
    }
    public static ItemMB GetFromID(int ID)
    {

        //The item we get from playerprefs

        // ItemType output = Resources.Load<InventoryItem>("Items/{}");
        return new WeaponItemMB();
    } 
    public void Awake()
    {

        InitItem();
        this.GetComponent<UnityEngine.UI.Image>().sprite = icon;
    }
    public bool AtCapacity()
    {
        return stacksHeld >= itemBase.stackLimit;
    }
    public abstract ItemType GetType();
    
    public virtual void Use()
    {
        OnInventorySelected?.Invoke();
    }

    public abstract ItemMB Clone();
}
