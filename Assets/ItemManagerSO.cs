using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="My Assets", fileName = "ItemManagerSO")]
public class ItemManagerSO : ScriptableObject
{
  public List<InventoryItem> items;
public int GetItemID(InventoryItem item)
{
  return items.IndexOf(item);
}
public InventoryItem GetItemByID(int id)
{
  return items[id];
}
}
