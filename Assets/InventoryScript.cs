using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class InventoryScript : MonoBehaviour
{
    public GameObject[] inventoryItemPrefabs;
    private int totalSlots;
    public int startingSlotCount;
    // public InventorySlot[] slots;
    public GameObject InventorySlotPrefab;
    //Below this will be the slots instantiated
    public GameObject slotParent;
    public List<ItemMB> GetItems()
    {
        return slotParent.GetComponentsInChildren<InventorySlot>().Select(x => x.GetItem()).ToList();
    }
    public void SetSize(int capacity)
    {
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

    //     public override void OnInspectorGUI   ()
    // 	{
    // 		EditorGUI.BeginChangeCheck();
    // 		SetSize(startingSlotCount);
    // // EditorGUILayout.ObjectField()
    // 		if(EditorGUI.EndChangeCheck())
    // 			Debug.Log("string changed");
    // 	}

    //effectively need to find an index where a is free, and a -1 isn't

    // private int GetFirstSlotIndexAvailable()
    // {
    //     //Some helper variables
    //     Predicate<int> isFree = new Predicate<int>((x) => !getSlot(x).IsOccupied());
    //     Predicate<int> isPriorOccupied = new Predicate<int>((x) =>  getSlot(x - 1).IsOccupied());
    //     Predicate<int> goRight = new Predicate<int>( (x) => !getSlot(x).IsOccupied());
        
    //     int left = 0;
    //     int right = totalSlots - 1;
    //     int incrementer = 1;

    //     int subject = (left+right)/2;
    //     //In effect, if it's free, we want to go left
    //     //And if it's closed, we want to go right 
    //  while (right - subject > 1)
    //  {

    //     if (!isPriorOccupied(subject))
    //     {
    //         subject = (int)math.floor((subject - left)/2); 
    //     }

    //     else if(!isFree(subject))
    //     {
    //         subject = (int)math.floor((right - subject)/2); 

    //     }
    //     else{
    //         return subject;
    //     }

      
    //  }
    //  return -1;

    // }
   private Transform GetFirstSlotAvailable()
   {
    foreach (Transform item in slotParent.transform)
    {
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
        // foreach (var item in inventoryItemPrefabs)
        // {
        //     AddItem(Instantiate(item));
        // }
    }
    public void Update()
    {
        SetSize(startingSlotCount);
    }
    public void Start()
    {
        TestAddItmes();
    }
}