using System;
using Unity.VisualScripting;
using UnityEngine;
abstract class AttackGameObject : MonoBehaviour
{
    /// <summary>
    /// The gameobject that launched the attack. Should probably be changed to a specific interface or class for characters.
    /// </summary>
     public GameObject user;
    [SerializeField] public int damage;
    [SerializeField] public int knockBack = 0;
    [SerializeField] protected float rangeScalar = 1;
    [SerializeField] public bool isCrit;
    [SerializeField] public float critMultiplier;
    [SerializeField] public float miniCritChance;
    public int GetDamage()
    {
        return (int)(damage * (isCrit ? critMultiplier : 1));
    }
    public void SetFactionLayer()
    {
         int layer;
        switch (user.GetComponent<UnitMB>().statInstance.unitFaction)
        {
            case Faction.Friendly:
                layer = LayerMask.NameToLayer("Friendly Attack");
                break;
            default:
                layer = LayerMask.NameToLayer("Enemy Attack");
                break;
        }

        gameObject.layer = layer;

        foreach (Transform item in transform)
        {
            
            EffectChildren(item, (t) => t.gameObject.layer = layer, (t) => t.childCount ==0);
        }

    }
    private void EffectChildren(Transform t, Action<Transform> A, Predicate<Transform> P)
    {
        //HAVE FUN READING THIS FOOL 
        A(t);
        if (P(t))
        {
            return;
        }
        foreach (Transform item in t)
        {
        EffectChildren(item, A, P);
            
        }
    }
    public virtual void Awake()
    {
        this.isCrit = UnityEngine.Random.Range(0, 100) < miniCritChance * 100;
    }
    public virtual void Start()
    {
        SetFactionLayer();
        Vector2 direction = user.GetComponent<MovementScript>().directionFacing;
        GetComponent<Rigidbody2D>().rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;

    } 

}