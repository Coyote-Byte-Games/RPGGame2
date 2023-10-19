using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
[CreateAssetMenu(menuName = "My Assets/Stats")]
public class UnitStats : ScriptableObject
{


    public Faction unitFaction;

    public int hp;
    /// <summary>
    /// The time the unit takes between turns
    /// </summary>
    public float downTime;
    public int tilesPerTurn;





    //Attacks available to unit
    public List<Attack> attacks;
    //an algorithm to find what target to go for,
    //based off of all the units in the combat scene
    public Func<Collection<UnitMB>> findTarget;


}
public enum Faction
{
    Friendly,
    Enemy,
}