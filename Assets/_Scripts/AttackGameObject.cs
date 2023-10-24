using UnityEngine;
abstract class AttackGameObject : MonoBehaviour
{
    /// <summary>
    /// The gameobject that launched the attack. Should probably be changed to a specific interface or class for characters.
    /// </summary>
    [HideInInspector] public GameObject user;
    [SerializeField] protected int damage;
    [SerializeField] protected bool isCrit;
    [SerializeField] protected float critMultiplier;
    public int GetDamage()
    {
        return (int)(damage * (isCrit ? critMultiplier : 1));
    }

}