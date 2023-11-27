using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerScript : MonoBehaviour
{
    public GameObject inventoryMain;
    public GameObject[] screens;

    void Start()
    {

    }
    void Update()
    {
        //To open it
        if (Input.GetButtonDown("Inventory"))
        {
            ToggleInventory();
        }
    }
    void ToggleInventory()
    {
        inventoryMain.active = !inventoryMain.active;
    }
  public void SwitchScreenRight()
   {
    //terrible but it works well enough
    int activeScreen = 1;
    for (int i = 0; i < screens.Length; i++)
    {
        if (screens[i].active)
        {
            activeScreen = i; 
        }
    }
    screens[activeScreen].active = false;
    screens[mod(++activeScreen, screens.Length) ].active = true;

   }
    public void SwitchScreenLeft()
   {
    //terrible but it works well enough
    int activeScreen = 0;
    for (int i = 0; i < screens.Length; i++)
    {
        if (screens[i].active)
        {
            activeScreen = i; 
        }
    }
    screens[activeScreen].active = false;
    screens[ mod (--activeScreen,screens.Length)].active = true;

   }
   private int mod(int x, int m) {
    return (x%m + m)%m;
}
    enum Screens
    {
        INVENTORY,
        EQUIPMENT,
        MAP
    }
}
