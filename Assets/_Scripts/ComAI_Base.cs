using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.PlasticSCM.Editor.UI;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//: Basically the brain of the unit, tells it where to go and how to attack
//: this AI finds the target, then starts moving towards target

public abstract class ComAI_Base : MonoBehaviour
{
    //todo fix this, not so good right below me
    AttackStage stage = 0;
    [SerializeField] internal CooldownCharacterGraphic cd;

    float _currentCooldown = 0;
    //todo yep this right above me
    //todo this too 

    public TileTelegraphVFXScript vFXScript;
    //tells us how long a tile is, etc

    private ICombatCreditManager combatCreditManager;
    public CombatGlobalInfoSO comInfo;


    GameObject tileTelegraph;
    public int attackRange = 2;
    //Entryway into unit stats, etc.
    public UnitMB unitData;
    // public Faction thisFaction;
    public GameObject target;
    private bool _isWaitingForAttack;
    //whether we're just waiting for our attack
    private bool _actionLoopInProgress = false;
    ICombatAction currentAction = null;

    //reaches target, then lanches attack

    #region Unity lifetime
    virtual internal void Start()
    {
        combatCreditManager = GetComponent<ICombatCreditManager>();
        // var rb = gameObject.GetComponent<Rigidbody2D>();
        //Perhaps an algorithm to find a certain target based off of stats and faction?
        // movementDriver.walkSpeed = unitData.statInstance.tilesPerTurn;
        CombatManager.instance.clockCycleEnd += MoveWithClock;

    }
    #endregion

    internal GameObject FindTarget()
    {
        //todo create singleton for unit management
        //todo make death have an eventhandler, then make this AI subscribe to that eventhandler so we can find a new target when that dies
        List<UnitMB> targets = UnitManager.instance.GetUnitsOutsideOfFaction().ToList();
        int random = UnityEngine.Random.Range(0, targets.Count());
        return target = targets[random].gameObject;
    }

    private void MoveWithClock()
    {

        // combatCreditManager.TakeCombatTick();
        //todo put logic here

        HandleState(stage);
    }
    public void Update()

    {
        Debug.Log("Current state: " + stage);
    }
    private void HandleState(AttackStage DONOTINCREMENTTHISONEUSETHEOTHERSTAGEVARIABLE)
    {
        //This is probably bad, but we also handle changing the state here. Oops.
        //cases:

        //1: No telegraphed attack

        //2: Waiting for credits

        //3: We have enough credits, launch attack

        switch (DONOTINCREMENTTHISONEUSETHEOTHERSTAGEVARIABLE)
        {
            case AttackStage.WAITING_FOR_DECISION:
                //What we need to do here is get the direction in which our action will work
                if (cd.IsDone())
                {
                    currentAction = GetAction();
                    tileTelegraph = vFXScript.CreateShape(currentAction.GetTelegraphData());
                    this.stage = AttackStage.WAITING_FOR_CREDITS;
                }
                break;
            case AttackStage.WAITING_FOR_CREDITS:
                //Check if we have enough credits to move on and finish this nonsense
                if (currentAction.CreditsRequired() <= combatCreditManager.Credits)
                {
                    this.stage = AttackStage.WAITING_TO_ATTACK;
                }
                break;
            case AttackStage.WAITING_TO_ATTACK:
                Destroy(tileTelegraph);
                Debug.Log("well well " + currentAction);
                currentAction.Use(gameObject);
                _currentCooldown = GetCooldown(currentAction.GetBaseCooldown());
                this.stage = AttackStage.RECOVERING_FROM_ATTACK;
                break;
            case AttackStage.RECOVERING_FROM_ATTACK:
                if ((_currentCooldown -= CombatManager.instance.ClockDuration) <= 0)
                {

                    this.stage = AttackStage.WAITING_FOR_DECISION;
                }
                break;
            default:
                throw new NotImplementedException("Combat AI found state unhandled!");
        }
    }

    /// <summary>
    ///Provides an interface to get cooldown from another objects cooldown. Use this, since there will probably be methods ini the future that change cooldown (buffs, equipment, etc) 
    /// </summary>

    private float GetCooldown(float value)
    {
        return value;
    }

    private void ScheduleAttack(ICombatAction suspectedAction)
    {
        suspectedAction.GetTelegraphData();
    }

    //this makes me want to cry.
    public bool TryAttack()
    {
        //Check if target within range
        if (CanAttack())
        {
            Attack();
            return true;
        }
        return false;
    }

    private enum AttackStage
    {
        WAITING_FOR_DECISION,
        WAITING_FOR_CREDITS,
        WAITING_TO_ATTACK,
        RECOVERING_FROM_ATTACK
    }

    internal abstract ICombatAction GetAction();
    internal abstract void Attack();
    internal abstract bool CanAttack();

}
