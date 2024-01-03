using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovinDirectinFacin : MonoBehaviour
{
    //THIS WILL BE THE DIRECTION SUPPLIED

    PlayerInputToMonoDir supplier;

    public void MoveUnit(UnityEngine.Vector2 dirFacing, float distance, float speed)
    {
        StartCoroutine(MoveUnit_Inner(dirFacing, distance, speed));
    }

    public void MoveUnit(UnityEngine.Vector2 vector)
    {
        StartCoroutine(MoveUnit_Inner(vector, vector.magnitude, 2));

    }
    private IEnumerator MoveUnit_Inner(UnityEngine.Vector2 dirFacing, float distance, float speed)
    {
        for (int i = 0; i < distance; i++)
        {
            //0.1 is to dampen the effect
            ForceMovement(distance * speed * 0.1f * -dirFacing);

            yield return new WaitForSeconds(1 / speed);
        }
        yield break;
    }





    public Rigidbody2D rb;
    //todo assign to tiles per turn
    public float walkSpeed;
    public void MovementMethod()
    {
        rb.position += walkSpeed * supplier.directionFacing;
    }
    public void ForceMovement(Vector2 direction)
    {
        Vector2 start = supplier.directionFacing;
        rb.position += (direction);

    }
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // public void Update()
    // {
    //     #region Animator
    //     // animator.SetFloat("DirectionX", directionFacing.x);
    //     // animator.SetFloat("DirectionY", directionFacing.y);
    //     // animator.SetFloat("speed", (directionSupplied).SqrMagnitude());
    //     #endregion

    // }


}
