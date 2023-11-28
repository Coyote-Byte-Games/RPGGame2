using System.Collections.Generic;
using UnityEngine;
//todo seperate file
public class WeaponItemMB : ItemMB
{
    public List<Attack> attacks;
    public override ItemMB Clone()
    {
      GameObject output = Instantiate (gameObject);// (new WeaponItemMB(){itemBase = this.itemBase, stacksHeld = this.stacksHeld, icon = this.icon, itemName = this.itemName});
        return output.GetComponent<ItemMB>();
    }

    public override ItemType GetType()
    {
        return ItemType.WEAPON;
    }

    public override void Use()
    {
        base.Use();
        Equip();
    }
    private void Equip()
    {
        //Get Spork
        var spork = FindObjectsByType<PlayerAttackScript>(FindObjectsSortMode.None)[0];
        UnitEquipmentScript sporkNoEquipment = spork.GetComponent<UnitEquipmentScript>(); 
        // Set sporks weapon
        sporkNoEquipment.EquipWeapon(this);

    }
    public List<Attack> AttacksProvided()
    {
        return attacks;
    }
}
