using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class UnitEquipmentScript : MonoBehaviour
{
    public GuidReference guid;
    public EquipmentData GetData()
    {
        return new EquipmentData(){weaponID = this.weapon.GetID(), pin1ID = pins[0].GetID(), pin2ID = pins[1].GetID()};
    }
    public void SetFromData( EquipmentData data)
    {
        // this.weapon = data.weaponID;
        // this.pins[0] = data.pin1ID;
        // this.pins[1] = data.pin2ID;
        
    } 
    // public void Populate(WeaponItemMB weapon, PinItemMB pin1,  PinItemMB pin2)
    // {
    //     this.weapon = weapon;
    // }
    public WeaponItemMB weapon;
    

    public PinItemMB[] pins = new PinItemMB[2];
    // public UnitMB attackScript;
    internal void EquipWeapon(WeaponItemMB weaponItemMB)
    {
        weapon = weaponItemMB;
        
    }
    public UnitEquipmentScript Clone()
    {
        return new(){weapon = weapon, pins = pins};
    
    }

    internal void SetFromInstance(UnitEquipmentScript unitEquipmentScript)
    {
     
      this.weapon = unitEquipmentScript.weapon;
    }

    public struct EquipmentData {
        public int weaponID;
        public int pin1ID;
        public int pin2ID;
    }
}
