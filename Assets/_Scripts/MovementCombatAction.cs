using UnityEngine;
using UnityEditor.Callbacks;
using System;
using UnityEditorInternal;
using Unity.Burst.Intrinsics;
using static TileTelegraphVFXScript.ShapeUtil;
using Sirenix.Serialization;
using Sirenix.OdinInspector;
public class MovementCombatAction : SerializedMonoBehaviour, ICombatAction
{
    [OdinSerialize] public IEnemyMovementImplement movementImplement;
    public int magnitude = 1;
    private Rigidbody2D originRb2D;
    public Vector2Int destination;
    private int _heading;

    public MovementCombatAction(Vector2Int endPoint)
    {
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
        //First, we need to ask our Movement Implement just where the hell it wants to go
        Vector2Int thing = movementImplement.GetPracticalDirection();

        return new TileTelegraphData
        {
            color = TileTelegraphVFXScript.Palette.Yellow,
            // tileCoords = TileTelegraphVFXScript.ShapeUtil.LineFroToVertical(0, -1, -(magnitude + 1)) //new Vector2Int[]{destination * magnitude} 
            //Then, we need to get the direction
            tileCoords = new Vector2Int[] { thing }
        };
    }

    public bool AffirmUseAndDir(GameObject user, GameObject target)
    {
        //Because of SOC, I'm gonna be lazy and sub-conponent these into 
        /* 
        1) Movement driver basic, which just uses some shitty vector math
        2) Movement that uses A* to move around walls and the like   */
        // _heading = -GetRotationDegrees(movementImplement.GetPracticalDirection());
        destination = movementImplement.GetPracticalDirection();
        // Debug.Log("piss yourself" + _heading);
        return true;
    }
    // 
    public void Use(GameObject origin)
    {

        Debug.Log("YEOOOOOOOWUCH " + destination);
        origin.GetComponent<IMovement>().DeltaPosition(originRb2D ? originRb2D : originRb2D = origin.GetComponentInParent<Rigidbody2D>(), destination);
    }


    //! still smells
    public bool TryGetSupposedHeading(out int heading)
    {
        return (heading = _heading) != -1;

    }
}
