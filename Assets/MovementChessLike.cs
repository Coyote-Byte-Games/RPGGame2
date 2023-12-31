using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class MovementChessLike : MonoBehaviour, IMovement
{
    public CombatGlobalInfoSO combatInfo;
    //We should really probably interface all of our movement scripts, but theres too little need for it atm
    public void DeltaPosition(Rigidbody2D rb, Vector2Int changeInPosition)
    {
        //Moves the position as many units as we need
        rb.MovePosition(rb.position + combatInfo.CombatStepDistance * changeInPosition);
    }
}

public interface IMovement
{
    public void DeltaPosition(Rigidbody2D rb, Vector2Int changeInPosition);
    
}