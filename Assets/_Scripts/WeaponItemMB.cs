using UnityEngine;
//todo seperate file
public class WeaponItemMB : ItemMB
{
    public override ItemType GetType()
    {
        return ItemType.WEAPON;
    }

    public override void Use()
    {
        Equip();
    }
    private void Equip()
    {
        //Get Spork
        var spork = FindObjectsByType<PlayerAttackScript>(FindObjectsSortMode.None)[0];
        UnitEquipmentScript sporkNoEquipment = spork.GetComponent<UnitEquipmentScript>(); 
        // Set sporks weapon
        sporkNoEquipment.weapon = this;
        Debug.Log(    sporkNoEquipment.weapon + " is Sporks weapon");

    }
}
