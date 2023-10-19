using System;
using Unity;
using Unity.VisualScripting;
using UnityEngine;
public class GameStateManager : Singleton<GameStateManager>
{
    /// <summary>
    /// Event triggered before the state changes. 
    /// </summary>
    public static event Action<GameState> onBeforeStateChanged;

    /// <summary>
    /// Event triggered before the state changes. 
    /// </summary>
    public static event Action<GameState> onAfterStateChanged;
    public GameState gameState;

    public void ChangeState(GameState newState)
    {
        //security
        if (gameState == newState) return;
        //before we change state
        onBeforeStateChanged?.Invoke(newState);

        //handles every type of state change


        if (newState == GameState.Combat)
        {
            HandleCombatStateChange();
        }

        onAfterStateChanged?.Invoke(newState);


    }

    private void HandleCombatStateChange()
    {
        //todo unhardcode combat data
        //Load in the combat data (gonna hardcode for now)
        //spawn Spork
        //Spawn Other Units
    }
}
public enum GameState
{
    OverWorld,
    Combat,
}