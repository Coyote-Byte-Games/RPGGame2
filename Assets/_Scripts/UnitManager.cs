using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

public class UnitManager : StaticInstance<UnitManager>
{
    public void AddUnit(UnitMB unit)
    {
        units.Add(unit);
    }
    //todo make this private and set the unit init within the combat manager (which also means making a combat manager)
    public List<UnitMB> units;
    public List<UnitMB> GetUnitsOutsideOfFaction()
    {
        return units;
    }
}