
//! Interfaces
using UnityEngine;

public interface ICombatAction
{
    /// <returns>Data that describes the attack, including color and tile shape</returns>

    TileTelegraphData GetTelegraphData();
    int CreditsRequired();
    void Use(GameObject userGameobject);
    public bool ShouldUse(GameObject user, GameObject origin);
}
