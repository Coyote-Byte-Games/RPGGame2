using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractionScript : MonoBehaviour
{
    [SerializeField] private Vector2 directionFacing;
    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject interactionPrompt;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] float walkSpeed;


    private int _xDirection;
    public int XDirection
    {
        get { return _xDirection; }
        set { _xDirection = value; if (value != 0) { YDirection = 0; } }
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




    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, directionFacing);
    }




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
   void FixedUpdate()
    {
        MovementMethod();
    }
      public void MovementMethod()
    {
        
        if (FindObjectOfType<DialougeManager>().DialougeActive)
        {
            //So we can't move during dialouge
            return;
        }
        
        //smell but oh well
            if (((int)Input.GetAxisRaw("Horizontal") != 0) || (int)Input.GetAxisRaw("Vertical") != 0)
            {
                rb.position += walkSpeed * directionFacing;
            }

    }
    void Update()
    {
  #region Animator
        animator.SetFloat("DirectionX", directionFacing.x);
        animator.SetFloat("DirectionY", directionFacing.y);
        #endregion

        interactionPrompt.SetActive(false);
        var collider = Physics2D.Raycast(transform.position, directionFacing, interactDistance, layerMask).collider;
        InteractableScript script;
        UpdateDirectionFacing();
        if (collider is not null)
        {
            //todo fix this logic that was good before adding prior "collider not null" block
        if (!FindObjectOfType<DialougeManager>().DialougeActive && collider.gameObject.TryGetComponent(out script))
        {
            interactionPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                script.StartDialouge();
            }
        }
    
        }
        
    }

    private void UpdateDirectionFacing()
    {

        //Raw so we don't have yucky damping and delayed change
        YDirection = (int)Input.GetAxisRaw("Vertical");
        XDirection = (int)Input.GetAxisRaw("Horizontal");
        // directionFacing.x = XDirection == 0? directionFacing.x : XDirection;
        // directionFacing.y = YDirection == 0? directionFacing.y : YDirection;

        //If both inequal zero
        if (!(XDirection == YDirection && XDirection == 0))
        {
            directionFacing.x = XDirection;
            directionFacing.y = YDirection;

        }

    }
}
