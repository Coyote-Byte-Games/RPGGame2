// using System.Collections;
// using System.Collections.Generic;
// using System.Numerics;
// using UnityEngine;

// class DodgeAttackGO : FreeFormAttackGameObject
// {
//     private float factor = 0.025f;
//     Rigidbody2D rb;
//     private IEnumerator MovePlayer(UnityEngine.Vector2 dirFacing)
//     {
//         for (int i = 0; i < 1 / factor; i++)
//         {
//             GetUser().GetComponent<MovinDirectinFacin>().ForceMovement(rangeScalar * factor * -dirFacing);

//             yield return new WaitForSeconds(factor);
//         }
//         yield break;
//     }


//     private float velocity = 0.0005f;
//     public override void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         //todo clean this
//         var movement = GetUser().GetComponent<MovinDirectinFacin>();
//         var dirFacing = movement.directionFacing;

//         if (dirFacing.x < 0)
//         {
//             GetComponent<SpriteRenderer>().flipX = true;
//         }

//         movement.MoveUnit(dirFacing, 3, 14);



//         Destroy(gameObject, 0.33f);
//     }
// }
