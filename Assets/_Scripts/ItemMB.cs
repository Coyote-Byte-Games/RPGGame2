using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.Scripting;
public abstract class ItemMB : MonoBehaviour
{
    public string itemName;
    public Sprite icon;
    public InventoryItem itemBase;
    public void InitItem()
    {
        this.icon = itemBase.icon;
    }
    public int GetID()
    {
        return (int)itemBase.itemID;
    }
    public void Awake()
    {

        InitItem();
        this.GetComponent<UnityEngine.UI.Image>().sprite = icon;
    }
    public abstract ItemType GetType();
    
    public abstract void Use();
}
