using System;
using System.Collections.Generic;
using UnityEngine;


/* 
Has the following jobs: 
- Create the UI, elemment by element */
public class CombatTodoManager : Singleton<CombatTodoManager>
{
    
    public event Action OnAllTodosDone;
    public List<CombatTodo> todos;
    public void Start()
    {
        Debug.Log("The scrinkly is working! (be happy)");
        foreach (var item in todos)
        {
            item._OnTodoComplete += ProcessCompletionStatus;
        }
    }

    private void ProcessCompletionStatus()
    {
        foreach (var item in todos)
        {
            if (!TodoComplete(item))
            {
                return;
            }
        }
        OnAllTodosDone?.Invoke();
    }
    private bool TodoComplete(CombatTodo item)
    {
        return item.IsCommplete(); 
    }
}
