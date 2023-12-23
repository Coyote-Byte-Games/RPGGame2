using System;

internal interface ICombatCreditManager
{
    event EventHandler<CreditManagerEventArgs> CreditsUpdated;

    public int GetCredits();
    public void TakeCombatTick();

}
