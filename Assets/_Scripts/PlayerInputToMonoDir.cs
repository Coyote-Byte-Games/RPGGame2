using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerInputToMonoDir : MonoBehaviour
{





    #region quarentine

    //! both found in update
    //    if (((int) Input.GetAxisRaw("Horizontal") != 0) || (int) Input.GetAxisRaw("Vertical") != 0)
    // //         {
    // //             rb.position += movementdirectionFacing * movement.walkSpeed;

    // //         }


    //         GetComponent<MovinDirectinFacin>().directionSupplied.x = (int)Input.GetAxisRaw("Horizontal");        
    #endregion

    [SerializeField] private Animator animator;
    [SerializeField] SpriteRenderer sr;
    public bool flippy;
    private int _xDirection;

    public int XDirection
    {
        get { return _xDirection; }
        set { _xDirection = value; if (value != 0) { YDirection = 0; } }
    }

    [TabGroup("direction")] public Vector2 directionSupplied;
    [TabGroup("direction")] public Vector2 directionFacing;
    private int _yDirection;
    public int YDirection
    {
        get { return _yDirection; }
        set
        {
            _yDirection = value; if (value != 0)
            {
                XDirection = 0;
            }
        }
    }


    public void Update()
    {
        UpdateDirectionSupplied();
        UpdateDirectionFacing();
    }

    private void UpdateDirectionSupplied()
    {
        directionSupplied.y = (int)Input.GetAxisRaw("Vertical");
        directionSupplied.x = (int)Input.GetAxisRaw("Horizontal");
    }

    private void UpdateDirectionFacing()
    {
        animator.SetFloat("DirectionX", directionFacing.x);
        animator.SetFloat("DirectionY", directionFacing.y);
        if (directionFacing.x > 0 && flippy)
        {
            sr.flipX = true;
        }
        else if (directionFacing.x < 0 && flippy)
        {
            sr.flipX = false;

        }
        //Raw so we don't have yucky damping and delayed change
        YDirection = Mathf.RoundToInt(directionSupplied.y);
        XDirection = Mathf.RoundToInt(directionSupplied.x);
        // directionFacing.x = XDirection == 0? directionFacing.x : XDirection;
        // directionFacing.y = YDirection == 0? directionFacing.y : YDirection;

        //If either isn't zero
        if (YDirection != 0 || XDirection != 0)
        {
            directionFacing.x = XDirection;
            directionFacing.y = YDirection;

        }

    }
}