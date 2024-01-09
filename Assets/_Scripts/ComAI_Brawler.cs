using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class ComAI_Brawler : ComAI_Base

{
    public AttackCombatActionTileBased closeAttack;
    public MovementCombatAction movementAction;
    public int walkDistance = 1;


    internal override void Start()
    {
        base.Start();
        unitData.OnUpdate += closeAttack.TickCooldown;

    }
    internal override void Attack()
    {
        closeAttack.Use(gameObject);

    }
    internal override bool CanAttack()
    {
        return cd.IsDone() && Vector2.Distance(transform.position, target.transform.position) < attackRange;
    }

    internal override ICombatAction GetAction()
    {

        //Find if we're close enough to use our melee
        if (closeAttack.AffirmUseAndDir(gameObject, target.transform.position))
        {

            return closeAttack;
        }
        else
        {
            return GetMoveDown();
        }
    }

    private ICombatAction GetMoveDown()
    {
        movementAction.AffirmUseAndDir(gameObject, target.transform.position);
        movementAction.magnitude = walkDistance;
        return movementAction;
    }
}
