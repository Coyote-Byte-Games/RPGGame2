using System;
using System.Collections;
using System.Collections.Generic;

using System.Runtime.CompilerServices;
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

   public LoadingBar healthBar;
   public Action OnUpdate;
   public CharacterTextEmitter TextEmitter;
   [SerializeField] private Stats baseStats;
   public Stats statInstance;

   public event Action OnDeath;
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
         OnDeath?.Invoke();
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
         TakeDamage(attackScript);
         //Show damage taken
      }
   }

   private void TakeDamage(AttackGameObject attack)
   {
      statInstance.hp -= attack.GetDamage();

      //Damage string
      string damageString = attack.damage.ToString() + (attack.isCrit ? $" ✕ {attack.critMultiplier}!" : "");
      TextEmitter.ShowText(damageString, attack.isCrit ? CharacterTextEmitter.TextStyle.Dire : CharacterTextEmitter.TextStyle.Neutral);
      //health bar
      if (healthBar is not null)
      {
         healthBar.SetFillAmount((float)statInstance.hp / baseStats.hp);

      }
      
      //todo sloppy, fix this
      // TakeKnockback((attack.transform.position - transform.position).normalized, attack.knockBack);

   }

    private void TakeKnockback(Vector2 direction, int knockBack)
    {
          GetComponent<MovementScript>().MoveUnit(direction, knockBack, 15);

    }

    internal int GetHP()
    {
      return statInstance.hp;
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