using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
   private bool occupied;
   public ItemMB itemHeld;
   public ItemMB GetItem()
   {
    return GetComponentInChildren<ItemMB>();
   }
   public bool IsOccupied()
   {
      Debug.Log("Occupied? " + GetItem() is not null ? "Hell No!" : "as of recently yeah tbh");
    return GetItem() is not null;
   }
   public void RemoveItem()
   {
    Destroy(itemHeld.gameObject);
    occupied = false;
   }
   public void AddItem(ItemMB item)
   {
      if (!IsOccupied())
      {
      item.transform.SetParent(transform);
      item.transform.localPosition = Vector3.zero;

      this.itemHeld = item;
      }
   }
   public void UseItemHeld()
   {
      //If it's consumable, use it, etc..
      itemHeld.Use();
   }
}
