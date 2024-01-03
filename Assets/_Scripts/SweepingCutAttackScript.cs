using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

class SweepingCutAttackScript : FreeFormAttackGameObject
{

    Rigidbody2D rb;
    public float destroyTime = .3f;
    public float speed;
    private Vector2 directionFacing;
    public void Awake()
    {
        GetComponent<SpriteRenderer>().flipX = (UnityEngine.Random.Range(0, 100) > 50);
    }
    // Start is called before the first frame update

    // Update is called once per frame
    public override void Start()
    {
        var movement = GetUser().GetComponent<MovinDirectinFacin>();
        rb = GetComponent<Rigidbody2D>();
        ;// movement.directionFacing;
        rb.position = rangeScalar * directionFacing + (Vector2)GetUser().transform.position;
        base.Start();
        //todo clean this




        //Pretty much just die in a few seconds
        Destroy(transform.parent.parent.gameObject, 0.35f);

    }
    public void Update()
    {
        rb.position += directionFacing * speed * Time.deltaTime;
    }
}

public class FreeFormAttackGameObject : AttackGameObject
{
    public int rangeScalar;
}