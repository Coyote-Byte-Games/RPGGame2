using System;

internal interface ICombatCreditManager
{
    event EventHandler<CreditManagerEventArgs> CreditsUpdated;
    public int Credits {get;set;}
    public void TakeCombatTick();

}

public class CreditManagerEventArgs
{
    int newCredits;
}