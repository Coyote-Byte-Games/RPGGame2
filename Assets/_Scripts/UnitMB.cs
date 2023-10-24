using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitMB : MonoBehaviour
{
   /*
   This class is responsible for handling INSTANCE DATA + basic unit behvaiour: 
   Attacks available (which are derived from the StatBase) ,
   access to the instance data Stats
   */
   //todo fix privacy of field
   public Action OnUpdate;
   [SerializeField] private Stats baseStats;
   public Stats statInstance;
   public void Awake()
   {
      statInstance = baseStats;
   }

public void Update()
{
   OnUpdate?.Invoke();
   #region am I truly alive?
      if (statInstance.hp <= 0)
      {
         GameObjectHelpers.instance.Kablooey(this);
      }
   #endregion
}
   public void OnTriggerEnter2D(Collider2D collision)
   {
      Debug.Log($"Trigger enter with {name} colliding with {collision.name}");
      //Checking layers
      int collisionLayer = collision.gameObject.layer;
      //If opposing faction and is an attack by getting component
      if
      (
        collision.gameObject.TryGetComponent<AttackGameObject>(out AttackGameObject attackScript)
        &&
        collisionLayer != LayerMask.NameToLayer($"{statInstance.unitFaction} Attack")
      )
      {
         //take damage to stat
        TakeDamage(attackScript.GetDamage());
         //Show damage taken
      }
   }

    private void TakeDamage(int takenDamage)
    {
      statInstance.hp -= takenDamage;
      Debug.Log($"{name} took {takenDamage} damage, and now has {statInstance.hp} health remaining!");
    }
}
[Serializable]
public struct Stats
{


    public Faction unitFaction;

    public int hp;
    /// <summary>
    /// The time the unit takes between turns
    /// </summary>
    public float downTime;
    public int tilesPerTurn;





    //Attacks available to unit
    public List<Attack> attacks;

}
public enum Faction
{
    Friendly,
    Enemy,
}