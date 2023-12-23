using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//: Basically the brain of the unit, tells it where to go and how to attack
//: this AI finds the target, then starts moving towards target

public abstract class ComAI_Base : MonoBehaviour
{
    //todo fix this, not so good right below me
    AttackStage stage = 0;
    //todo yep this right above me
    public TileTelegraphVFXScript vFXScript;
    //tells us how long a tile is, etc
    private ICombatCreditManager combatCreditManager;
    public CombatGlobalInfoSO comInfo;
    public MovementScript movementDriver;
    public AttackCombatAction closeAttack;

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
    void Start()
    {
        combatCreditManager = GetComponent<ICombatCreditManager>();
        var rb = gameObject.GetComponent<Rigidbody2D>();
        //Perhaps an algorithm to find a certain target based off of stats and faction?
        // movementDriver.walkSpeed = unitData.statInstance.tilesPerTurn;
        CombatManager.instance.clockCycleEnd += MoveWithClock;
        unitData.OnUpdate += closeAttack.TickCooldown;
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

        combatCreditManager.TakeCombatTick();
        //todo put logic here

        HandleState(stage);
    }

    private void HandleState(AttackStage stage)
    {
        //This is probably bad, but we also handle changing the state here. Oops.
        //cases:

        //1: No telegraphed attack

        //2: Waiting for credits

        //3: We have enough credits, launch attack

        switch (stage)
        {
            case AttackStage.WAITING_FOR_DECISION:
                currentAction = GetAction();
                tileTelegraph = vFXScript.CreateShape(currentAction.GetTelegraphData());
                stage++;
                break;
            case AttackStage.WAITING_FOR_CREDITS:
                //Check if we have enough credits to move on and finish this nonsense
                if (currentAction.CreditsRequired() <= combatCreditManager.GetCredits())
                {
                    stage++;
                }
                break;
            case AttackStage.WAITING_TO_ATTACK:
                Destroy(tileTelegraph);
                Attack();
                stage = AttackStage.WAITING_FOR_DECISION;
                break;
            default:
                throw new NotImplementedException("Combat AI found state unhandled!");
        }
    }

    private void ScheduleAttack(ICombatAction suspectedAction)
    {
        suspectedAction.GetTelegraphData();
    }

    //this makes me want to cry.


    private bool ActionLoopDone()
    {
        return !_actionLoopInProgress;
    }
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
        WAITING_TO_ATTACK
    }

    internal abstract ICombatAction GetAction();
    internal abstract void Attack();
    internal abstract bool CanAttack();

}
