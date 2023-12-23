using System;
using UnityEngine;

public class EnemyCombatCreditManager : ICombatCreditManager
{
    int _credits;
    int _ticksHeld;
    [SerializeField] int TicksPerCredit = 1;
    [SerializeField] int CreditIncrement = 1;

    public event EventHandler<CreditManagerEventArgs> CreditsUpdated;

    public int GetCredits()
    {
        return _credits;
    }

    public void TakeCombatTick()
    {
        //    _credits += Mathf.FloorToInt((++_ticksHeld%TicksPerCredit)/TicksPerCredit);
        _credits += (++_ticksHeld >= TicksPerCredit ? CreditIncrement : 0);
    }
}
