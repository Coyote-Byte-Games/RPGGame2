using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//: Basically the brain of the unit, tells it where to go and how to attack
//: this AI finds the target, then starts moving towards target

public class ComAI_Basic : ComAIBasic
{
    public MovementScript movementDriver;
    //Entryway into unit stats, etc.
    public UnitMB unitData;
    // public Faction thisFaction;
    public GameObject target;
    private int clockTicksSinceLastMove = 0;

    //reaches target, then lanches attack

    private void FindTarget()
    {
        //todo create singleton for unit management
        //todo make death have an eventhandler, then make this AI subscribe to that eventhandler so we can find a new target when that dies
        List<UnitMB> targets = UnitManager.instance.GetUnitsOutsideOfFaction().ToList();
        int random = Random.Range(0, targets.Count());
        target = targets[random].gameObject;

    }
    void Start()
    {
        var rb = gameObject.GetComponent<Rigidbody2D>();
        //Perhaps an algorithm to find a certain target based off of stats and faction?
        // movementDriver.walkSpeed = unitData.statInstance.tilesPerTurn;
        CombatManager.instance.clockCycleEnd += MoveWithClock;

    }
    private void MoveWithClock()
    {
        clockTicksSinceLastMove++;
        //If the threshold for absorbed movements is reached, move.
        if (clockTicksSinceLastMove >= unitData.statInstance.downTime)
        {

            #region direction towards target
            try
            {
            UnityEngine.Vector2 direction = (target.transform.position - transform.position).normalized;
                  movementDriver.directionSupplied = direction;
            movementDriver.MovementMethod();
            clockTicksSinceLastMove = 0;

            }
            catch (System.Exception)
            {
                
                
            }
            #endregion
          
        }


    }
    public void Update()
    {
        //We want this to move at the speed in stats every second
        //todo create the combat clock and sync with that

    }
    // private IEnumerator MoveWithClock()
    // {

    //     for (; ; )
    //     {


    //         yield return new WaitForSeconds(1 * unitData.statInstance.downTime );
    //     }
    // }

}
