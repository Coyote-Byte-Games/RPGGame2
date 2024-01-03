using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class BasicCombatCreditManager : SerializedMonoBehaviour, ICombatCreditManager
{
    public void Start()
    {
        CombatManager.instance.clockCycleEnd += TakeCombatTick;
    }
    [SerializeField] internal int credits;

    public int Credits { get => credits; set => credits = value; }

    public event EventHandler<CreditManagerEventArgs> CreditsUpdated;


    public void TakeCombatTick()
    {
        Debug.Log("sckrin!");
        Credits++;
    }
}
