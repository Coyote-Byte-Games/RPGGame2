using UnityEngine;

/// <summary>
/// Core responsibility is to handle all UI.
/// </summary>
public abstract class CombatTodoUI : MonoBehaviour
{
    public abstract void Exterminate();

    public abstract void UpdateUI(int v);
}