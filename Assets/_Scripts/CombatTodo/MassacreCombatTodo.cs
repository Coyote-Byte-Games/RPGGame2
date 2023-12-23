using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MassacreCombatTodo : CombatTodo
{
    #region Inspector Variables

    private int numberRemaining;
    #endregion
    #region Unity Objects
    public List<UnitMB> targets;
    public MasssacreCombatTodoUI UI;
    #endregion
    private bool IsFinished()
    {
        foreach (var item in targets)
        {
            if (item is not null)
            {
                return false;
            }
        }
        return true;
    }
    public override void UpdateTodo()
    {
        UI.UpdateUI(--numberRemaining);
        if (IsCommplete())
        {
            EndTodo();
        }
    }

    private void EndTodo()
    {
        OnTodoComplete();
        UI.Exterminate();
    }
    #region Lifetime methods
    public void Start()
    {

        foreach (var item in targets)
        {

            item.OnDeath += UpdateTodo;
            numberRemaining++;
        }
        UI.UpdateUI(numberRemaining);
    }

    internal override bool IsCommplete()
    {
        return numberRemaining <= 0;
    }
    #endregion

}
