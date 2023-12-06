using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   private bool occupied;
   public ItemMB itemHeld;
   public GameObject descriptivePopupPrefab;
   GameObject descriptivePopupInstance;
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
   public virtual void AddItem(ItemMB item)
   {
      if (!IsOccupied())
      {
         item.transform.SetParent(transform);
         item.transform.localPosition = Vector3.zero;
         //todo make items look better when added to the inventory
         this.itemHeld = item;
      }
   }
   public void OnMouseOver()
   {
      Debug.Log("Mouse over fired!");
   }

   public void OnPointerExit(PointerEventData eventData)
   {
      Debug.Log("left pointer");
      Destroy(descriptivePopupInstance);
   }

   public void OnPointerEnter(PointerEventData eventData)
   {
      Debug.Log("entered pointer");
      //Create dialouge if it doesn't exist
      if (IsOccupied())
      {
         descriptivePopupInstance = GameObjectHelpers.CreatePopup(transform, descriptivePopupPrefab, 700, 100);
         descriptivePopupInstance.GetComponent<ItemHoverDetail>().Fill(itemHeld);

      }
   }

}