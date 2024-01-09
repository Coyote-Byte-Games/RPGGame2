
//! Interfaces
using UnityEngine;

public interface ICombatAction
{

    /// <summary>
    /// Responsible for returning the direciton the attack will go in, alongside heading 
    /// </summary>
    /// <returns>Data that describes the attack, including color and tile shape</returns>

    TileTelegraphData GetTelegraphData();
    int CreditsRequired();
    void Use(GameObject userGameobject);
    public bool AffirmUseAndDir(GameObject user, Vector2 origin);
    float GetBaseCooldown();
    /// <summary>
    /// Returns the time the move takes the recover from.
    /// </summary>
    /// <returns>Recovery time in seconds</returns>
    float GetRecovery();
    public bool TryGetSupposedHeading(out int heading);
}
