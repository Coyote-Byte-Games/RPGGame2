
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

class ChessMovement : SerializedMonoBehaviour
{
    public int magnitude;
    public Rigidbody2D rb;
    [OdinSerialize]
    public IMovement movement;
    public PlayerInputToMonoDir dirSupplier;
    [OdinSerialize] ICombatCreditManager credits;
    public float recovery;
    public float passedRecov;
    public float recoveryStandard = 5;
    public CooldownCharacterGraphic cdGraph;



    public void Update()
    {
        recovery -= Time.deltaTime;
        if (dirSupplier.directionSupplied != Vector2.zero && credits.Credits >= magnitude && recovery <= 0)
        {
            movement.DeltaPosition(rb, dirSupplier.directionFacing);
            credits.Credits -= magnitude;
            //! This is so terrible


            recovery = passedRecov = recoveryStandard;
            cdGraph.SetCooldown(passedRecov);
        }
    }
}

/* 
- Need to have credit interface for Sporks Attacks

1) Get Spork attacks working, giving attacks windup (that will also use the pattern)
2) Ensure Spork has a heading supplier as a standalone script
 */