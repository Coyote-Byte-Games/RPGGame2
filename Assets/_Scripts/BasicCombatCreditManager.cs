using System;
using UnityEngine;

public class BasicCombatCreditManager : MonoBehaviour, ICombatCreditManager
{
    [SerializeField] internal int credits;

    public int Credits { get => credits; set => credits = value; }

    public event EventHandler<CreditManagerEventArgs> CreditsUpdated;


    public void TakeCombatTick()
    {
        credits++;
    }
}
