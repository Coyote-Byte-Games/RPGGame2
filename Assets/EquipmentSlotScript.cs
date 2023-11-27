using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentSlotScript : MonoBehaviour
{
    [SerializeField]
    public ItemType type;

    GameObject inventoryPrefab;// =Resources.Load<TextAsset>("UI/InventoryBox");


    //Main purpose: To handle the popping up of inventory dialouges based on type
    public void OpenDialouge()
    {
        //Get the items from Spork
        List<ItemMB> inventoryItems = FindObjectOfType<InventoryScript>(true).GetItems();
        Debug.Log(inventoryItems.Count());
        //Filter those into the specific type
        var itemsWeActuallyWant = (from thing in inventoryItems where thing.GetType() == type select thing); 
        //Create the popup
        inventoryPrefab = Resources.Load<GameObject>("UI/InventoryBox");
        var prefab = Instantiate(inventoryPrefab,GameObject.Find("Canvas").transform);
        prefab.transform.localPosition = Vector3.zero;


        //Fill the popup

        InventoryScript inventory = prefab.GetComponent<InventoryScript>();
        // inventory.slotCount = (itemsWeActuallyWant.Count());

        inventory.SetSize(itemsWeActuallyWant.Count());//startingSlotCount = (itemsWeActuallyWant.Count());
        foreach (ItemMB item in itemsWeActuallyWant)
        {
            Debug.Log("giggity");
        inventory.AddItem(Instantiate(item));            
        }

        // When an item is selected in the popup, we want clicking it to equip it
        //This means having each inventory slot, when clicked, is used or equipped

    }
}
