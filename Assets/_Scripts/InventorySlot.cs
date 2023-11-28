using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : InventorySlotBase
{
   
   public bool LeaveOnSelect = false;
  public void UseItemHeld()
   {
      //If it's consumable, use it, etc..
      itemHeld.Use();
   }
}
