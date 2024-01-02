using UnityEngine;
using UnityEditor.Callbacks;
using System;
using UnityEditorInternal;
using Unity.Burst.Intrinsics;
using static TileTelegraphVFXScript.ShapeUtil;
public class MovementCombatAction : MonoBehaviour, ICombatAction
{
    public IEnemyMovementImplement movementImplement;
    private int magnitude;
    private Rigidbody2D originRb2D;
    private Vector2Int destination;
    private int _heading;

    public MovementCombatAction(Vector2Int endPoint)
    {
        this.destination = endPoint;
        this.magnitude = (int)destination.magnitude;
    }
    public int CreditsRequired()
    {
        return magnitude;
    }

    public float GetBaseCooldown()
    {
        return 0.5f;
    }

    public TileTelegraphData GetTelegraphData()
    {
        return new TileTelegraphData
        {
            color = TileTelegraphVFXScript.Palette.Yellow,
            tileCoords = TileTelegraphVFXScript.ShapeUtil.LineFroToVertical(0, -1, -(magnitude + 1)) //new Vector2Int[]{destination * magnitude} 

        };
    }

    public bool AffirmUseAndDir(GameObject user, GameObject origin)
    {
        //Because of SOC, I'm gonna be lazy and sub-conponent these into 
        /* 
        1) Movement driver basic, which just uses some shitty vector math
        2) Movement that uses A* to move around walls and the like   */
        _heading = -GetRotationDegrees(movementImplement.GetPracticalDirection());

        return true;
    }
    // 
    public void Use(GameObject origin)
    {
        origin.GetComponent<IMovement>().DeltaPosition(originRb2D ? originRb2D : originRb2D = origin.GetComponentInParent<Rigidbody2D>(), destination);
    }


    //! still smells
    public bool TryGetSupposedHeading(out int heading)
    {
        return (heading = _heading) != -1;

    }
}
