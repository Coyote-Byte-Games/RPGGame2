using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatTodoTracker : MonoBehaviour
{
    public GameObject todoPrefab;
    //Contains a list of Combat todos, all of which work to 
    public void Instantiate(SlaughterCombatTodo todo)
    {
        todo.HandleAddle(this);
    }

    internal CombatTodoGO CreateTodo(string defaultMessage)
    {
        //Here, we get text for the todo and pass in an object that the data can manipulate
        //todo elaborate
        var todo = Instantiate(todoPrefab);
        var script = todo.GetComponent<CombatTodoGO>();
        script.SetText(defaultMessage);
        return script;
    }
}
[Serializable]
//Is data class
public class SlaughterCombatTodo// : ICombatTodo
{
    //the list of todos 
    private CombatTodoTracker parent;

    //The actual widget responsible for this
    CombatTodoGO widget;
    public List<UnitMB> hitList;
    private int killCount = 0;

    public void HandleAddle(CombatTodoTracker parent)
    {
        this.parent = parent;
        //todo make sure this is invoked on awake
        //handles its own evens
        foreach (var item in hitList)
        {
            item.OnDeath += () => killCount++;
            item.OnDeath += () => FinishWhenComplete();
        }
        //Creating the todo, with text and setting reference
        this.widget = parent.CreateTodo($"Kill {hitList.Count} enemies.");
    }

    private void FinishWhenComplete()
    {
        if (isDone())
        {
          Debug.Log($"The todo slaughter01 is finished!");

        }
    }

    public bool isDone()
    {
        return killCount >= hitList.Count();
    }



}
// public interface ICombatTodo
// {
//     //Basically reuqires
//     // - A way to resemble the objective being fulfilled
//     //A way to let the parent  function know its been fulfilled
//     bool isDone();
//     void notfiyParent();
//     //How the parent should implement this.
//     void HandleAddle(CombatTodoTracker parent);

// }