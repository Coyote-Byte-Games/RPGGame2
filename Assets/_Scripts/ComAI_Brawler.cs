using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class ComAI_Brawler : ComAI_Base

{
    public AttackCombatActionTileBased closeAttack;
    public MovementCombatAction movementAction;



    internal override void Start()
    {
        base.Start();
        unitData.OnUpdate += closeAttack.TickCooldown;
    }
    internal override void Attack()
    {
        //todo add heading support
        closeAttack.Use(gameObject);

    }
    internal override bool CanAttack()
    {
        return Vector2.Distance(transform.position, target.transform.position) < attackRange;
    }

    internal override ICombatAction GetAction()
    {

        //Find if we're close enough to use our melee
        if (closeAttack.AffirmUseAndDir(gameObject, target))
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
        return movementAction;
    }
}
