// using UnityEngine;
// class TreeAttackGameObjectScript : AttackGameObject
// {
//     #region Unity Properties
//     private Rigidbody2D rb;
//     private SpriteRenderer sr;
//     private Vector2 direction;

//     #endregion

//     public float speed = 200;
//     public float rotationalSpeed = 20;






//     public void Update()
//     {
//         rb.MovePosition(rb.position + direction * Time.deltaTime * speed);
//         rb.rotation += (rotationalSpeed * Time.deltaTime);
//     }
//     public void Start()
//     {
//         #region basic init
//         rotationalSpeed = Random.Range(rotationalSpeed, rotationalSpeed * 5);


//         sr = GetComponent<SpriteRenderer>();
//         rb = GetComponent<Rigidbody2D>();
//         direction = GetUser().GetComponent<MovinDirectinFacin>().directionFacing;
//         // rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

//         #endregion

//         //Setting the hitbox value of this
//         //if friendly, set to friendly attack
//         //otherwise enemy attack
//         SetFactionLayer();
//     }

// }