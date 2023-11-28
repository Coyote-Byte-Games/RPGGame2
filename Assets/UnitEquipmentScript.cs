using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class UnitEquipmentScript : MonoBehaviour
{
    public WeaponItemMB weapon;
    public PinItemMB[] pins = new PinItemMB[2];
    // public UnitMB attackScript;
    internal void EquipWeapon(WeaponItemMB weaponItemMB)
    {
        weapon = weaponItemMB;
        
    }
}
