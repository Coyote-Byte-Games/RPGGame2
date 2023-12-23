using System;
using UnityEngine;

public class ComAI_Brawler : ComAI_Base

{
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

        if (CanAttack())
        {
            //well, then, attack!
            return closeAttack;
        }
        //Well, guess you've just gotsta keep on movin'.
        //todo this is a very temporary botch
        return GetMoveDown();
    }

    private ICombatAction GetMoveDown()
    {
        throw new NotImplementedException();
    }
}
