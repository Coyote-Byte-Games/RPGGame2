using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class ComAI_Brawler : ComAI_Base

{
    public AttackCombatAction closeAttack;



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
        if (closeAttack.ShouldUse(gameObject, target))
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
        return new MovementCombatAction
        (
            Vector2Int.down * 2
        );
    }
}

internal class MovementCombatAction : ICombatAction
{
    private int magnitude;
    private Rigidbody2D originRb2D;
    private Vector2Int destination;

    public MovementCombatAction(Vector2Int endPoint)
    {
        this.destination = endPoint;
        this.magnitude = (int)destination.magnitude;
    }
    public int CreditsRequired()
    {
        return magnitude;
    }

    public TileTelegraphData GetTelegraphData()
    {
        return new TileTelegraphData
        {
            color = TileTelegraphVFXScript.Palette.Yellow,
            tileCoords = TileTelegraphVFXScript.ShapeUtil.LineFroToVertical(0, -1, -(magnitude + 1)) //new Vector2Int[]{destination * magnitude} 

        };
    }

    public bool ShouldUse(GameObject user, GameObject origin)
    {
        //perfectly fine
        //! no its not fixme    NOW

        return true;
    }

    public void Use(GameObject origin)
    {
        origin.GetComponent<IMovement>().DeltaPosition(originRb2D ? originRb2D : originRb2D = origin.GetComponentInParent<Rigidbody2D>(), destination);
    }


}