
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



    public void Update()
    {
        if (dirSupplier.directionSupplied != Vector2.zero && credits.Credits >= magnitude)
        {
            movement.DeltaPosition(rb, dirSupplier.directionFacing);
            credits.Credits -= magnitude;
        }
    }
}