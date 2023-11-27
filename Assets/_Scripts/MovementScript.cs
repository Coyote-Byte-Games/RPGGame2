using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    #region Properties
    [SerializeField] private Animator animator;
    [SerializeField] SpriteRenderer sr;
    public bool flippy;


    private int _xDirection;

    public int XDirection
    {
        get { return _xDirection; }
        set { _xDirection = value; if (value != 0) { YDirection = 0; } }
    }
    public void MoveUnit(UnityEngine.Vector2 dirFacing, float distance, float speed)
    {
        StartCoroutine(MoveUnit_Inner(dirFacing, distance, speed));

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

    #endregion


    public Vector2 directionSupplied;
    public Rigidbody2D rb;
    //todo assign to tiles per turn
    public float walkSpeed;
    public Vector2 directionFacing;
    public void MovementMethod()
    {
        //smell but oh well
        // if (((int)directionSupplied.x != 0) || (int)directionSupplied.y != 0)
        // {
        rb.position += walkSpeed * directionFacing;
        // }

    }
    public void ForceMovement(Vector2 direction)
    {
        Vector2 start = directionFacing;
        rb.position += (direction);
        directionFacing = start;
    }
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
    public void Update()
    {
        #region Animator
        // animator.SetFloat("DirectionX", directionFacing.x);
        // animator.SetFloat("DirectionY", directionFacing.y);
        animator.SetFloat("speed", (directionSupplied).SqrMagnitude());
        #endregion
        UpdateDirectionFacing();
    }
}
