using UnityEngine;

public class InventorySlotBase : MonoBehaviour
{
     private bool occupied;
   public ItemMB itemHeld;
   public ItemMB GetItem()
   {
    return GetComponentInChildren<ItemMB>();
   }
   public bool TryGetItem(out ItemMB output)
   {
     var thing = GetComponentInChildren<ItemMB>();
    output = thing;
   return thing is not null;
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
   
}