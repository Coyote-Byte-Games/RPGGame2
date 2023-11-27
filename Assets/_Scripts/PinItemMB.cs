using UnityEngine;

public class PinItemMB : ItemMB
{
    public override ItemType GetType()
    {
        return ItemType.PIN;
    }

    public override void Use()
    {
        Equip();
    }
    private void Equip()
    {
      
    }
}