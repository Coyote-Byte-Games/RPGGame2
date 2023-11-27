using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Scripting;
public abstract class ItemMB : MonoBehaviour
{
    public string itemName;
    public int stacksHeld = 1;
    public Sprite icon;
    public InventoryItem itemBase;
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
    
    public abstract void Use();
}
