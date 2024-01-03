using System;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
public abstract class AttackGameObject : MonoBehaviour
{
    /// <summary>
    /// The gameobject that launched the attack. Should probably be changed to a specific interface or class for characters.
    /// </summary>

    #region Fields
    //[HideInInspector] 
    [ShowInInspector] private GameObject _user;
    [SerializeField] public int damage;
    [SerializeField] public int knockBack = 0;

    [TabGroup("Crits")][SerializeField] public float critMultiplier = 2;
    [Range(0, 1)]
    [TabGroup("Crits")][SerializeField] public float miniCritChance;

    [TabGroup("Crits")][ReadOnly] public bool isCrit;

    #endregion

    public int GetDamage()
    {
        return (int)(damage * (isCrit ? critMultiplier : 1));
    }
    public void SetFactionLayer()
    {
        int layer;
        switch (_user.GetComponent<UnitMB>().statInstance.unitFaction)
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

            EffectChildren(item, (t) => t.gameObject.layer = layer, (t) => t.childCount == 0);
        }

    }
    public void SetUser(GameObject user)
    {
        this._user = user;
    }
    public GameObject GetUser()
    {
        return _user;
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
    #region Lifecycle
    public virtual void Awake()
    {
        this.isCrit = UnityEngine.Random.Range(0, 100) < miniCritChance * 100;
    }
    public virtual void Start()
    {
        SetFactionLayer();
    }

    #endregion

}