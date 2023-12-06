using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static InventoryItem;

public class EquipmentSlotScript : InventorySlotBase
{
    [SerializeField]
    public ItemType type;
    public override void AddItem(ItemMB item)
   {base.AddItem(item);
   item.transform.localScale = Vector2.one * 1.5f;
      }

    GameObject inventoryPrefab;// =Resources.Load<TextAsset>("UI/InventoryBox");


    //Main purpose: To handle the popping up of inventory dialouges based on type
    public void OpenDialouge()
    {
        //Get the items from Spork
        List<ItemMB> inventoryItems = FindObjectOfType<InventoryScript>(true).GetItems();
        //making copy
        // List<ItemMB> inventoryItems = new List<ItemMB>(inventoryItemsOG);
        Debug.Log(inventoryItems.Count());
        //Filter those into the specific tye
        
        //This isn't working when the inventory isn't full
        var itemsWeWant = from thing in inventoryItems where thing.GetType() == type select thing; 
        var itemsWeActuallyWantClone = itemsWeWant.Select((x) => x.Clone());
        //Create the popup
        inventoryPrefab = Resources.Load<GameObject>("UI/InventoryBox");
        var prefab = Instantiate(inventoryPrefab,GameObject.Find("Canvas").transform);
        prefab.transform.position = transform.position - new Vector3((transform.position.x>0 ? 500 : -500), 0,0);
        prefab.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(675, 600);
        // prefab.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(700, 600);


        //Fill the popup

        InventoryScript inventory = prefab.GetComponent<InventoryScript>();
        // inventory.slotCount = (itemsWeActuallyWant.Count());

        inventory.SetSize(itemsWeActuallyWantClone.Count());//startingSlotCount = (itemsWeActuallyWant.Count());
        foreach (ItemMB item in itemsWeActuallyWantClone)
        {
        inventory.AddItem(item);    
        //We want to close this if we select an item there
        
        //what the hell is this
            item.OnInventorySelected += () => AddItem(item);
            item.OnInventorySelected += () => Destroy(prefab);
        }
        //we want: Choosing the item will move all items to the inentory

        // When an item is selected in the popup, we want clicking it to equip it
        //This means having each inventory slot, when clicked, is used or equipped

    }
}
