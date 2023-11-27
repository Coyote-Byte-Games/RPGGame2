using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

[ExecuteInEditMode]
public class InventoryScript : MonoBehaviour
{
    public GameObject[] inventoryItemPrefabs;
    private int totalSlots;
    public int slotCount;
    // public InventorySlot[] slots;
    public GameObject InventorySlotPrefab;
    //Below this will be the slots instantiated
    public GameObject slotParent;
    public List<ItemMB> GetItems()
    {
        return slotParent.GetComponentsInChildren<InventorySlot>().Select(x => x.GetItem()).ToList();
    }
    private void MoveItem(int start, int end)
    {
        //todo this thing
    }
    //Method to remove inventory at slot
    //Method to sort entire inventory
    private void Sort()
    {
    Queue<int> empties = new Queue<int>();
    //what we want is to go through every slot and see if its occupied
    Predicate<InventorySlot> Occupied = new Predicate<InventorySlot>((x) => x.IsOccupied());
    for (int i = 0; i < totalSlots; i++)
    {
        //If it's empty, add i to the queue
        if (!Occupied(getSlot(i)))
        {
            empties.Enqueue(i);
        }
        //If full, move it to the first empty slot and dequeue
        if (empties.Count > 0)
        {
            //mmmmmmmmmmm icecream yummmy
            int indexToGoInto = empties.Dequeue();
            MoveItem(i, indexToGoInto);
        }
    }
    }
    public void SetSize(int capacity)
    {
        slotCount = capacity;
        //todo this is kinda jank but whatever im sick of working on this
        if (capacity == 0)
        {
            foreach (Transform item in slotParent.transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }
        //just makin' sure we're on the same page, here
        totalSlots = slotParent.transform.childCount;
        //the same
        if (capacity == totalSlots)
        {
            //return if we already have that number of slots (obsolete request)
            return;
        }
        //expansion
        if (capacity > totalSlots)
        {
            for (int i = 0; i < (capacity - totalSlots); i++)
            {
                var item = InventorySlotPrefab;
                //imperfect, but it works in most cases
                item.name = $"slot{slotParent.transform.childCount + 1}";
                Instantiate(item, slotParent.transform);
            }
        }
        //Shrinkage
        else
        {
            for (int i = totalSlots - 1; i > capacity - 1; i--)
            {
                DestroyImmediate(slotParent.transform.GetChild(i).gameObject);
            }
        }
        totalSlots = capacity;
    }

   private Transform GetFirstSlotAvailable()
   {
    
    foreach (Transform item in slotParent.transform)
    {
        Debug.Log("Rank 2 KEKLOL!");
        if (!item.GetComponent<InventorySlot>().IsOccupied())
        {
            return item;
        }
    }
    return null;
   }
 
    private InventorySlot getSlot(int index)
    {
        return slotParent.transform.GetChild(index).GetComponent<InventorySlot>();
    }
    private Transform getSlotTransform(int index)
    {
        return slotParent.transform.GetChild(index);
    }
    // public Transform GetFirstSlotAvailable()
    // {
    //     Debug.Log("Item index: " + GetFirstSlotIndexAvailable());
    //     return getSlotTransform(GetFirstSlotIndexAvailable());
    // }
    public void AddItem(ItemMB toAdd)
    {
        Debug.Log((slotParent is null ) + " keklol");
        //Currently, slot parent has no children
        InventorySlot slot = GetFirstSlotAvailable().GetComponent<InventorySlot>();
        slot.AddItem(toAdd);
    }
    public void AddItem(GameObject toAdd)
    {
        if (!toAdd.TryGetComponent<ItemMB>(out ItemMB item))
        {
            //If not the right type
            Debug.LogError("Tried to add non-item to inventory!");
            return;
        }
        InventorySlot slot = GetFirstSlotAvailable().GetComponent<InventorySlot>();
        slot.AddItem(item);
    }
    public int GetCountOfParticularItem(int itemID)
    {
        int output = 0;
        foreach (var item in GetItems())
        {
            if (item.GetID() == itemID)
            {
                output++;
            }
        }
        return output;
    }
    public void TestAddItmes()
    {
        foreach (var item in inventoryItemPrefabs)
        {
            AddItem(Instantiate(item));
        }
    }
    public void Update()
    {
        SetSize(slotCount);
        if (Input.GetKey(KeyCode.T))
        {
            SaveInventory();
        }
    }
    public void Start()
    {
        TestAddItmes();
    }




    //Saving the ItemMBs in a format where they can be loaded
    public void SaveInventory()
    {
        InventoryInstanceData inventoryData = new();
        //Get each item, and the count
        foreach (var item in GetItems())
        {
            inventoryData.Add(new ItemSave{itemID = item.GetID(), count=item.stacksHeld});
        }
        SaveDataHelper.SaveInventory(inventoryData);
        //Save this data somewhere   
    }
    public void WipeInventory()
    {
        
    }
    public void LoadInventory()
    {
        WipeInventory();
        var newInventory = SaveDataHelper.LoadInventory();
    }
    [Serializable]
    public struct ItemSave
    {
        public int itemID;
        public int count;
    }
    [Serializable]
    public class InventoryInstanceData : CollectionBase
    {
        public void Add(ItemSave item)
        {
            this.List.Add(item);
        }
         public ItemSave this[int i]  
        {  
            get  
            {  
                return (ItemSave)this.List[i];  
            }  
            set  
            {  
                this.List.Add(value);  
            }  
        }  
    }
}